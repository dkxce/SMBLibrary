using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SMBLibrary;
using SMBLibrary.Authentication.GSSAPI;
using SMBLibrary.Authentication.NTLM;
using SMBLibrary.Server;
using SMBLibrary.Win32.Security;
using Utilities;
using System.ServiceProcess;

namespace SMBServer
{
    #if NET20
    internal class SMBServerService : ServiceBase
    {
        private static SMBServerSvc svc = null;
        private string[] onInitArguments;

        public SMBServerService(string[] args) { onInitArguments = args;  }

        protected override void OnStart(string[] args)
        {
            svc = new SMBServerSvc();
        }

        protected override void OnStop()
        {
            if (svc != null) svc.Stop();
        }
    }
    #endif
    #if NET40
    public partial class SMBServerService : ServiceBase
    {
        private static SMBServerSvc svc = null;
        private string[] onInitArguments;

        public SMBServerService(string[] args) { onInitArguments = args;  }

        protected override void OnStart(string[] args)
        {
            svc = new SMBServerSvc();
        }

        protected override void OnStop()
        {
            if (svc != null) svc.Stop();
        }
    }
    #endif

    internal class SMBServerSvc
    {
        private SMBLibrary.Server.SMBServer m_server;
        private SMBLibrary.Server.NameServer m_nameServer;
        private LogWriter m_logWriter;

        public static void Run(string[] args) 
        {
            #if NET20
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new SMBServerService(args) };
            ServiceBase.Run(ServicesToRun);
            #endif
            #if NET40
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new SMBServerService(args) };
            ServiceBase.Run(ServicesToRun);
            #endif
        }

        public SMBServerSvc() 
        {
            List<IPAddress> localIPs = NetworkInterfaceHelper.GetHostIPAddresses();
            KeyValuePairList<string, IPAddress> list = new KeyValuePairList<string, IPAddress>();
            list.Add("Any", IPAddress.Any);
            foreach (IPAddress address in localIPs) list.Add(address.ToString(), address);

            int[] ports = SettingsHelper.ReadPortSettings();
            string[] autorun = SettingsHelper.ReadAutorun();
            SMBLibrary.Server.SMBServer.NetBiosOverTCPPort = ports[1];
            SMBLibrary.Server.SMBServer.DirectTCPPort = ports[2];
            IPAddress serverAddress = IPAddress.Any;
            foreach(KeyValuePair<string,IPAddress> kvp in list) 
                if(kvp.Key == autorun[1]) 
                    serverAddress = kvp.Value;

            SMBTransportType transportType = SMBTransportType.DirectTCPTransport;
            if (ports[0] == 0) transportType = SMBTransportType.NetBiosOverTCP;
            
            NTLMAuthenticationProviderBase authenticationMechanism;
            if (ports[5] == 1)
                authenticationMechanism = new IntegratedNTLMAuthenticationProvider();
            else
            {
                UserCollection users;
                try { users = SettingsHelper.ReadUserSettings(); }
                catch { return; };
                authenticationMechanism = new IndependentNTLMAuthenticationProvider(users.GetUserPassword);
            }

            List<ShareSettings> sharesSettings;
            try { sharesSettings = SettingsHelper.ReadSharesSettings(); }
            catch (Exception) { return; }

            SMBShareCollection shares = new SMBShareCollection();
            foreach (ShareSettings shareSettings in sharesSettings)
            {
                FileSystemShare share = SMBServer.ServerUI.InitializeShare(shareSettings);
                shares.Add(share);
            }

            GSSProvider securityProvider = new GSSProvider(authenticationMechanism);
            m_server = new SMBLibrary.Server.SMBServer(shares, securityProvider);
            m_logWriter = new LogWriter();
            // The provided logging mechanism will synchronously write to the disk during server activity.
            // To maximize server performance, you can disable logging by commenting out the following line.
            m_server.LogEntryAdded += new EventHandler<LogEntry>(m_logWriter.OnLogEntryAdded);

            try
            {
                m_server.Start(serverAddress, transportType, ports[3] == 1, ports[4] == 1);
                if (transportType == SMBTransportType.NetBiosOverTCP)
                {
                    if (serverAddress.AddressFamily == AddressFamily.InterNetwork && !IPAddress.Equals(serverAddress, IPAddress.Any))
                    {
                        IPAddress subnetMask = NetworkInterfaceHelper.GetSubnetMask(serverAddress);
                        m_nameServer = new NameServer(serverAddress, subnetMask);
                        m_nameServer.Start();
                    };
                };
            }
            catch (Exception ex) { return; }
        }

        public void Stop()
        {
            m_server.Stop();
            m_logWriter.CloseLogFile();
            if (m_nameServer != null)
                m_nameServer.Stop();            
        }
    }
}

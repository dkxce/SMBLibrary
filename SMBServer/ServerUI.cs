/* Copyright (C) 2014-2018 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using SMBLibrary;
using SMBLibrary.Authentication.GSSAPI;
using SMBLibrary.Authentication.NTLM;
using SMBLibrary.Server;
using SMBLibrary.Win32;
using SMBLibrary.Win32.Security;
using Utilities;

namespace SMBServer
{
    public partial class ServerUI : Form
    {
        private SMBLibrary.Server.SMBServer m_server;
        private SMBLibrary.Server.NameServer m_nameServer;
        private LogWriter m_logWriter;

        public ServerUI()
        {
            InitializeComponent();
        }

        private void ServerUI_Load(object sender, EventArgs e)
        {            
            List<IPAddress> localIPs = NetworkInterfaceHelper.GetHostIPAddresses();
            KeyValuePairList<string, IPAddress> list = new KeyValuePairList<string, IPAddress>();
            list.Add("Any", IPAddress.Any);
            foreach (IPAddress address in localIPs)
            {
                list.Add(address.ToString(), address);
            }
            comboIPAddress.DataSource = list;
            comboIPAddress.DisplayMember = "Key";
            comboIPAddress.ValueMember = "Value";

            try
            {
                int[] ports = SettingsHelper.ReadPortSettings();
                netbiosPort.Value = ports[1];
                directPort.Value = ports[2];
                if (ports[0] == 0) rbtNetBiosOverTCP.Checked = true;
                if (ports[0] == 1) rbtDirectTCPTransport.Checked = true;
                chkSMB1.Checked = ports[3] == 1;
                chkSMB2.Checked = ports[4] == 1;
                chkIntegratedWindowsAuthentication.Checked = ports[5] == 1;
            }
            catch { };

            try
            {
                string[] autorun = SettingsHelper.ReadAutorun();
                for (int i = 0; i < list.Count; i++)
                    if (list[i].Key == autorun[1])
                        comboIPAddress.SelectedIndex = i;                
                if (autorun[2] == "1") WindowState = FormWindowState.Minimized;
                notifyIcon1.Text = $"{this.Text}: idle";
                if (autorun[0] == "1") btnStart_Click(sender, e);
            }
            catch { };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SMBLibrary.Server.SMBServer.NetBiosOverTCPPort = (int)netbiosPort.Value;
            SMBLibrary.Server.SMBServer.DirectTCPPort = (int)directPort.Value;
            IPAddress serverAddress = (IPAddress)comboIPAddress.SelectedValue;
            SMBTransportType transportType;
            if (rbtNetBiosOverTCP.Checked)
            {
                transportType = SMBTransportType.NetBiosOverTCP;
            }
            else
            {
                transportType = SMBTransportType.DirectTCPTransport;
            }

            NTLMAuthenticationProviderBase authenticationMechanism;
            if (chkIntegratedWindowsAuthentication.Checked)
            {
                authenticationMechanism = new IntegratedNTLMAuthenticationProvider();
            }
            else
            {
                UserCollection users;
                try
                {
                    users = SettingsHelper.ReadUserSettings();
                }
                catch
                {
                    MessageBox.Show("Cannot read " + SettingsHelper.SettingsFileName, "Error");
                    return;
                }

                authenticationMechanism = new IndependentNTLMAuthenticationProvider(users.GetUserPassword);
            }

            List<ShareSettings> sharesSettings;
            try
            {
                sharesSettings = SettingsHelper.ReadSharesSettings();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot read " + SettingsHelper.SettingsFileName, "Error");
                return;
            }

            SMBShareCollection shares = new SMBShareCollection();
            foreach (ShareSettings shareSettings in sharesSettings)
            {
                FileSystemShare share = InitializeShare(shareSettings);
                shares.Add(share);
            }

            GSSProvider securityProvider = new GSSProvider(authenticationMechanism);
            m_server = new SMBLibrary.Server.SMBServer(shares, securityProvider);
            m_logWriter = new LogWriter();
            // The provided logging mechanism will synchronously write to the disk during server activity.
            // To maximize server performance, you can disable logging by commenting out the following line.
            m_server.LogEntryAdded += new EventHandler<LogEntry>(m_logWriter.OnLogEntryAdded);

            string status = "running";
            try
            {
                m_server.Start(serverAddress, transportType, chkSMB1.Checked, chkSMB2.Checked);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                status = $"error: {ex.Message}";
                return;
            }

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            comboIPAddress.Enabled = false;
            rbtDirectTCPTransport.Enabled = false;
            rbtNetBiosOverTCP.Enabled = false;
            chkSMB1.Enabled = false;
            chkSMB2.Enabled = false;
            chkIntegratedWindowsAuthentication.Enabled = false;
            netbiosPort.Enabled = false;
            directPort.Enabled = false;
            notifyIcon1.Text = $"{this.Text}: {status}";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_server.Stop();
            m_logWriter.CloseLogFile();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            comboIPAddress.Enabled = true;
            rbtDirectTCPTransport.Enabled = true;
            rbtNetBiosOverTCP.Enabled = true;
            chkSMB1.Enabled = true;
            chkSMB2.Enabled = true;
            chkIntegratedWindowsAuthentication.Enabled = true;
            netbiosPort.Enabled = true;
            directPort.Enabled = true;

            if (m_nameServer != null)
            {
                m_nameServer.Stop();
            }
            notifyIcon1.Text = $"{this.Text}: stopped";
        }

        private void chkSMB1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMB1.Checked)
            {
                chkSMB2.Checked = true;
            }
        }

        private void chkSMB2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkSMB2.Checked)
            {
                chkSMB1.Checked = true;
            }
        }

        public static FileSystemShare InitializeShare(ShareSettings shareSettings)
        {
            string shareName = shareSettings.ShareName;
            string sharePath = shareSettings.SharePath;
            List<string> readAccess = shareSettings.ReadAccess;
            List<string> writeAccess = shareSettings.WriteAccess;
            FileSystemShare share = new FileSystemShare(shareName, new NTDirectoryFileSystem(sharePath));
            share.AccessRequested += delegate(object sender, AccessRequestArgs args)
            {
                bool hasReadAccess = Contains(readAccess, "Users") || Contains(readAccess, args.UserName);
                bool hasWriteAccess = Contains(writeAccess, "Users") || Contains(writeAccess, args.UserName);
                if (args.RequestedAccess == FileAccess.Read)
                {
                    args.Allow = hasReadAccess;
                }
                else if (args.RequestedAccess == FileAccess.Write)
                {
                    args.Allow = hasWriteAccess;
                }
                else // FileAccess.ReadWrite
                {
                    args.Allow = hasReadAccess && hasWriteAccess;
                }
            };
            return share;
        }

        public static bool Contains(List<string> list, string value)
        {
            return (IndexOf(list, value) >= 0);
        }

        public static int IndexOf(List<string> list, string value)
        {
            for (int index = 0; index < list.Count; index++)
            {
                if (string.Equals(list[index], value, StringComparison.OrdinalIgnoreCase))
                {
                    return index;
                }
            }
            return -1;
        }

        private void directPort_ValueChanged(object sender, EventArgs e)
        {
            rbtDirectTCPTransport.Checked = true;
        }

        private void netbiosPort_ValueChanged(object sender, EventArgs e)
        {
            rbtNetBiosOverTCP.Checked = true;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Visible && this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                return;
            };
            this.Visible = !this.Visible;            
        }
    }
}
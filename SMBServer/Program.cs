/* Copyright (C) 2014 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;

namespace SMBServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);            

            foreach (string arg in args)
            {
                if (arg == "/install" || arg == "-install")
                {
                    InstallSvc();
                    return;
                };
                if (arg == "/uninstall" || arg == "-uninstall")
                {
                    UnInstallSvc();
                    return;
                };
                if (arg == "/start" || arg == "-start")
                {
                    StartSvc();
                    return;
                };
                if (arg == "/stop" || arg == "-stop")
                {
                    StopSvc();
                    return;
                };
            };

            if (!Environment.UserInteractive)
            {
                SMBServerSvc.Run();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ServerUI());
            };
        }

        private static void InstallSvc()
        {
            try {
                string fullpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SMBServer.exe");
                ServiceInstaller.Install("SMBServerSvc", "SMB Server", fullpath);
                System.Windows.Forms.MessageBox.Show("SMBServerSvc Successfully Installed", "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private static void UnInstallSvc()
        {
            try
            {
                ServiceInstaller.Uninstall("SMBServerSvc");
                System.Windows.Forms.MessageBox.Show("SMBServerSvc Successfully Uninstalled", "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private static void StartSvc()
        {
            try
            {
                ServiceInstaller.StartService("SMBServerSvc");
                System.Windows.Forms.MessageBox.Show("SMBServerSvc Successfully Started", "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private static void StopSvc()
        {
            try
            {
                ServiceInstaller.StopService("SMBServerSvc");
                System.Windows.Forms.MessageBox.Show("SMBServerSvc Successfully Stopped", "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "SMB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                Exception ex = (Exception)e.ExceptionObject;
                HandleUnhandledException(ex);
            }
        }

        private static void HandleUnhandledException(Exception ex)
        {
            string message = String.Format("Exception: {0}: {1} Source: {2} {3}", ex.GetType(), ex.Message, ex.Source, ex.StackTrace);
            MessageBox.Show(message, "Error");
            Application.Exit();
        }
    }    
}
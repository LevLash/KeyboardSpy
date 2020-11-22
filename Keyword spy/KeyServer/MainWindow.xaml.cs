using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpListener listener;
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;
        Task receiveTask;
        CancellationTokenSource source;
        ObservableCollection<KeyClass> keys;
        ObservableCollection<PC_Class> PCs;
        public MainWindow()
        {
            InitializeComponent();
            keys = new ObservableCollection<KeyClass>();
            PCs = new ObservableCollection<PC_Class>();
            dgr_Info.ItemsSource = keys;
            dgr_Status.ItemsSource = PCs;
        }

        private async void MenuItemStartServer_Click(object sender, RoutedEventArgs e)
        {
            btn_Start.IsEnabled = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            var localEP = new IPEndPoint(ip, 50000);
            listener = new TcpListener(localEP);
            listener.Start();
            client = await listener.AcceptTcpClientAsync();
            CreateReceiveTask();
            listener.Stop();
        }

        private void CreateReceiveTask()
        {
            var ns = client.GetStream();
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            source = new CancellationTokenSource();
            writer.AutoFlush = true;
            receiveTask = Task.Run(() => { Received(source.Token); });
        }

        private void Received(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                var buffer = new byte[1024];
                try
                {
                    var amount = client.Client.Receive(buffer);
                    var s = Encoding.UTF8.GetString(buffer, 0, amount);
                    var ss = s.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var pt = (PackageType)Convert.ToInt32(ss[0]);
                    using (FileStream fstream = new FileStream($"{Environment.CurrentDirectory}\\log.txt", FileMode.Append))
                    {
                        var array = System.Text.Encoding.Default.GetBytes(s + "?");
                        fstream.Write(array, 0, array.Length);
                    }
                    switch (pt)
                    {
                        case PackageType.On:
                            PC_Class it = null;
                            foreach (var item in PCs)
                            {
                                if (item.PC == ss[1])
                                {
                                    it = item;
                                    if (item.status == "OFF")
                                    {
                                        item.status = "ON";
                                        break;
                                    }
                                }
                            }
                            if (it == null)
                            {
                                it = new PC_Class();
                                it.PC = ss[1];
                                it.status = "ON";
                                Dispatcher.Invoke(() => {
                                    PCs.Add(it);
                                });
                            }
                            break;
                        case PackageType.Pressed:
                            Dispatcher.Invoke(() => {
                                keys.Add(new KeyClass(keys.Count + 1, ss[1], ss[2], ss[3]));
                            });
                            break;
                        case PackageType.Off:
                            PC_Class its = null;
                            foreach (var item in PCs)
                            {
                                if (item.PC == ss[1])
                                {
                                    its = item;
                                    if (item.status == "ON")
                                    {
                                        item.status = "OFF";
                                        break;
                                    }
                                }
                            }
                            if (its == null)
                            {
                                its.PC = ss[1];
                                its.status = "OFF";
                                Dispatcher.Invoke(() => {
                                    PCs.Add(its);
                                });
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (SocketException) { }
            }
        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            keys.Clear();
            PCs.Clear();
        }
    }
}

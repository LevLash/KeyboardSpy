using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Show
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<KeyClass> keysList;
        string path = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            keysList = new ObservableCollection<KeyClass>();
            dgr_Info.ItemsSource = keysList;
        }

        private void MenuChooseFile_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                    ReadFile();
                }
            }
            
        }

        public void ReadFile()
        {
            string[] ss;
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string s = System.Text.Encoding.Default.GetString(array);
                ss = s.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            }
            string[] sss;
            for (int i = 0; i < ss.Length; i++)
            {
                sss = ss[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (sss[0] == "0" || sss[0] == "2")
                {
                    Dispatcher.Invoke(() =>
                    {
                        keysList.Add(new KeyClass(keysList.Count, sss[1], sss[0] == "0" ? "On" : "Off"));
                    });
                    
                }
                else if (sss[0] == "1")
                {
                    Dispatcher.Invoke(() =>
                    {
                        keysList.Add(new KeyClass(keysList.Count + 1, sss[1], sss[2], sss[3]));
                    });
                }
                
            }
        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            keysList.Clear();
        }

        private void MenuItemRefresh_Click(object sender, RoutedEventArgs e)
        {
            keysList.Clear();
            ReadFile();
        }
    }
}

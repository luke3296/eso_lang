using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace blaise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {

        private static String? currentFileName = null;

        public EditorWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pascal source file (*.pas)|*.pas";
            if(ofd.ShowDialog() == true)
            {
                codeEditor.Text = File.ReadAllText(ofd.FileName);
                currentFileName = ofd.FileName;
                this.Title = "Blaise: " + currentFileName;
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            if (currentFileName == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pascal source file (*.pas)|*.pas";
                if(sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, codeEditor.Text);
                    currentFileName = sfd.FileName;
                    this.Title = "Blaise: " + currentFileName;
                }
            } else
            {
                File.WriteAllText(currentFileName, codeEditor.Text);
            }
        }

        private void SaveFileAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Pascal source file (*.pas)|*.pas";
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, codeEditor.Text);
                currentFileName = sfd.FileName;
                this.Title = "Blaise: " + currentFileName;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Build(object sender, RoutedEventArgs e)
        {
            // no-op
        }

        private void BuildAndRun(object sender, RoutedEventArgs e)
        {
            // simulated - shows the output window and echoes something to a new command line window
            OutputWindow ow = new OutputWindow();
            ow.Show();
            System.Diagnostics.Process.Start("cmd.exe", "/K echo /b \"This is where the compiled C program would execute and show the output.\"");
        }
    }
}

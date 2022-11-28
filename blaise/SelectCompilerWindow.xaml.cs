using Microsoft.Win32;
using System.Windows;

namespace blaise
{
    /// <summary>
    /// Interaction logic for SelectCompilerWindow.xaml
    /// </summary>
    public partial class SelectCompilerWindow : Window
    {
        public SelectCompilerWindow()
        {
            InitializeComponent();
        }

        private void SelectCompilerPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Executable file (*.exe)|*.exe";
            if (ofd.ShowDialog() == true)
            {
                blaise.Properties.Settings.Default.Compiler = ofd.FileName;
                blaise.Properties.Settings.Default.Save();
            }
        }
    }
}

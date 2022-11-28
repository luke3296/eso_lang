using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

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
            //ofd.Filter = "Pascal source file (*.pas)|*.pas";
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
            if (blaise.Properties.Settings.Default.Compiler == null || !File.Exists(blaise.Properties.Settings.Default.Compiler))
            {
                // no compiler selected, cancel execution and show compiler window
                MessageBox.Show("Either no C compiler has been set, or it can't be found. Ensure a working compiler has been set up and select the path to the executable before execution.");
                SelectCompilerWindow scw = new SelectCompilerWindow();
                scw.Show();
            }
            else
            {
                // saves the open file before compilation
                SaveFile(null, null);
                
                // here, we'd run the Pascal -> C transpiler, and get the output as a string.
                // currently we're just passing the open file to the C compiler (it can't handle .pas files,
                // which is why I've disabled the open file filter)

                String outputFileName = Path.GetFileNameWithoutExtension(currentFileName);

                Process p = new Process();
                p.StartInfo.FileName = blaise.Properties.Settings.Default.Compiler;
                p.StartInfo.Arguments = $"{currentFileName} -Wall -g -o {Directory.GetCurrentDirectory()}\\{outputFileName}.exe";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.EnableRaisingEvents = true;

                String compilerOutput = "";

                p.OutputDataReceived += (sender, args) =>
                {
                    compilerOutput += args.Data;
                    Debug.Print(args.Data);
                };
                p.ErrorDataReceived += (sender, args) =>
                {
                    compilerOutput += args.Data;
                    Debug.Print(args.Data);
                };
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.CancelOutputRead();

                OutputWindow ow = new OutputWindow(compilerOutput);
                ow.Show();
            }
        }

        private void BuildAndRun(object sender, RoutedEventArgs e)
        {
            // First build the app
            Build(null, null);
        }

        /*
         * Allows the user to set the path of a valid C compiler
         * Currently we don't check to see if the provided binary is a valid C compiler,
         * but we do make sure it is an executable.
         * Chosen binary is persisted in the app's settings.
         */
        private void SelectCompiler(object sender, RoutedEventArgs e)
        {
            SelectCompilerWindow scw = new SelectCompilerWindow();
            scw.Show();
        }
    }
}

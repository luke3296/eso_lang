using Eso_Lang;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace blaise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {

        private static String? currentFileName = null;
        private static StringBuilder compilerOut;

        public EditorWindow()
        {
            InitializeComponent();
            compilerOut = new StringBuilder();
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

        private async void Build(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" called build " + codeEditor.Text);
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
                if (currentFileName == null)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Pascal source file (*.pas)|*.pas";
                    if (sfd.ShowDialog() == true)
                    {
                        File.WriteAllText(sfd.FileName, codeEditor.Text);
                        currentFileName = sfd.FileName;
                        this.Title = "Blaise: " + currentFileName;
                    } else
                    {
                        return;
                    }
                }
                else
                {
                    File.WriteAllText(currentFileName, codeEditor.Text);
                }

                // run the Pascal -> C transpiler, returns the C code as a string
                string text = codeEditor.Text.Replace(System.Environment.NewLine, " ");
                
                Lexer_Pascal lexer = new Lexer_Pascal();
                List<Token> tokens = lexer.LexPascal(text);
                Parser_Pascal parser = new Parser_Pascal(tokens);

                System.Diagnostics.Debug.WriteLine(" input str " + text);
          
                if (parser.Parse() == 1) 
                {
                    MessageBox.Show("Syntax error!");
                    return;
                }

                Pascal2C transpiler = new Pascal2C(tokens);
                String transpiledCode = transpiler.genProgram();

                MessageBox.Show(transpiledCode);

                String outputDirectory = Path.GetDirectoryName(currentFileName);
                String outputFileName = Path.GetFileNameWithoutExtension(currentFileName);

                File.WriteAllText($"{outputDirectory}\\{outputFileName}.c", transpiledCode);



              //  StartAndWaitProcess(@"some\document\path", transpiledCode);


                
                MessageBox.Show("writing to " + $"{outputDirectory}\\{outputFileName}.c \n" + " using compiler " + blaise.Properties.Settings.Default.Compiler);
                File.WriteAllText($"{outputDirectory}\\{outputFileName}.c", transpiledCode);
                
                Process p = new Process();
                p.StartInfo.FileName = blaise.Properties.Settings.Default.Compiler;
                p.StartInfo.Arguments = $"{outputDirectory}\\{outputFileName}.c -Wall -g -o {outputDirectory}\\{outputFileName}.exe";
                System.Diagnostics.Debug.WriteLine(" Process args " + $"{outputDirectory}\\{outputFileName}.c -Wall -g -o {outputDirectory}\\{outputFileName}.exe");
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
                await p.WaitForExitAsync();
                p.WaitForExit();
                p.CancelOutputRead();


                OutputWindow ow = new OutputWindow(compilerOut.ToString(), transpiledCode);
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

        async void StartAndWaitProcess(string path, string transpiledCode)
        {
            var p = new Process();
            
            p.EnableRaisingEvents = true;
            String outputDirectory = Path.GetDirectoryName(currentFileName);
            String outputFileName = Path.GetFileNameWithoutExtension(currentFileName);
            //MessageBox.Show("writing to " + $"{outputDirectory}\\{outputFileName}.c \n" + " using compiler " + blaise.Properties.Settings.Default.Compiler);

          
            p.StartInfo.FileName = "C:\\cygwin64\\bin\\gcc.exe";// blaise.Properties.Settings.Default.Compiler;
                                                                // p.StartInfo.Arguments = $"{outputDirectory}\\{outputFileName}.c -Wall -g -o {outputDirectory}\\{outputFileName}.exe";
            System.Diagnostics.Debug.WriteLine(" \nProcess args " + blaise.Properties.Settings.Default.Compiler + $" {outputDirectory}\\{outputFileName}.c -Wall -g -o {outputDirectory}\\{outputFileName}.exe");
            //p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;

            System.Text.StringBuilder compilerOutput = new System.Text.StringBuilder("");
            var lineCount = 0;
            p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    System.Diagnostics.Debug.WriteLine("gotoutput");
                    lineCount++;
                    compilerOutput.Append("\n[" + lineCount + "]: " + e.Data);
                }
            });

            p.Start();

            // Asynchronously read the standard output of the spawned process.
            // This raises OutputDataReceived events for each line of output.
            p.BeginOutputReadLine();
            await p.WaitForExitAsync();
           // await p.WaitForExit();
            System.Diagnostics.Debug.WriteLine("returnd from compilation" + compilerOutput.ToString());

           
            p.WaitForExit();
            p.Close();

            var tcs = new TaskCompletionSource<bool>();
            p.Exited += (sender, args) => { tcs.SetResult(true); p.Dispose(); };
            Task.Factory.StartNew(() => p.Start());
           // return "returen";
        }

   
    }


}

namespace EasyEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\EasyEditor";
            //if (!Directory.Exists(mainPath))
            //{
            //    Directory.CreateDirectory(mainPath);
            //}
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\EasyEditor";
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            if (!Directory.Exists(dataPath + @"\Temp"))
            {
                Directory.CreateDirectory(dataPath + @"\Temp");
            }
            if (!File.Exists(dataPath + @"\Temp\Recent Files.json"))
            {
                var createFile = File.Create(dataPath + @"\Temp\Recent Files.json");
                createFile.Close();
            }
            if (!File.Exists(dataPath + @"\Temp\Opened Files.json"))
            {
                var createFile = File.Create(dataPath + @"\Temp\Opened Files.json");
                createFile.Close();
            }
            if (!Directory.Exists(dataPath + @"\Opened Files"))
            {
                Directory.CreateDirectory(dataPath + @"\Opened Files");
            }

            // If opened from file
            if (args != null && args.Length > 0)
            {
                string fileName = args[0];
                //Check file exists
                if (File.Exists(fileName))
                {
                    ApplicationConfiguration.Initialize();
                    Form1 MainFrom = new Form1();
                    MainFrom.openFile(fileName);
                    MainFrom.restoreFiles();
                    Application.Run(MainFrom);
                }
                //The file does not exist
                else
                {
                    MessageBox.Show("The file you were opening no longer exists!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ApplicationConfiguration.Initialize();
                    Form1 MainFrom = new Form1();
                    MainFrom.restoreFiles();
                    Application.Run(MainFrom);
                }
            }
            //Not opened from file
            else
            {
                ApplicationConfiguration.Initialize();
                Form1 MainFrom = new Form1();
                MainFrom.restoreFiles();
                Application.Run(MainFrom);
            }
        }
    }
}
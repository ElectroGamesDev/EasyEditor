using Ookii.Dialogs.WinForms;
using System.Security.Cryptography;
using System.Text;

namespace EasyEditor
{
    public partial class Form1 : Form
    {
        bool? fileOpened = false;
        string? filePath = "Untitled.txt";
        string? fileName = "Untitled.txt";
        string? saveStatus = "Unsaved";
        private Rectangle originalFormSize;
        private Rectangle textBoxOriginalRectangle;
        private Rectangle menuStripOriginalRectangle;
        int fileCounter;

        public Form1()
        {
            InitializeComponent();
            if (saveStatus == "Unsaved")
            {
                Text = "Unsaved | " + filePath + " - Easy Editor";
            }
            else
            {
                Text = filePath + " - Easy Editor";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            textBoxOriginalRectangle = new Rectangle(textBox.Location.X, textBox.Location.Y, textBox.Width, textBox.Height);
            menuStripOriginalRectangle = new Rectangle(menuStrip.Location.X, menuStrip.Location.Y, menuStrip.Width, menuStrip.Height);

            numOfLines.Text = "Lines: " + textBox.LineInfos.Count;
            numOfCharacters.Text = "Length: " + textBox.TextLength;
        }

        private void toolBar_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void toolBar_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        public void resizeControl(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Width) / (float)(originalFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(originalFormSize.Height);
            int newX = (int)(r.Width * xRatio);
            int newY = (int)(r.Height * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //resizeControl(textBoxOriginalRectangle, textBox);
            //resizeControl(menuStripOriginalRectangle, menuStrip);
        }

        private void textBox_LineInserted(object sender, FastColoredTextBoxNS.LineInsertedEventArgs e)
        {
            numOfLines.Text = "Lines: " + textBox.LineInfos.Count;
        }

        private void textBox_LineRemoved(object sender, FastColoredTextBoxNS.LineRemovedEventArgs e)
        {
            numOfLines.Text = "Lines: " + textBox.LineInfos.Count;
        }

        private void textBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            saveStatus = "Unsaved";
            numOfCharacters.Text = "Length: " + textBox.TextLength;
            if (Text == (filePath + " - Easy Editor"))
            {
                Text = saveStatus + " | " + filePath + " - Easy Editor";
            }
        }

        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            MenuStrip rightClickMenu = new MenuStrip();
            rightClickMenu.Location = new Point(MousePosition.X, MousePosition.Y);
            rightClickMenu.Items.Add("F");
            Controls.Add(rightClickMenu);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.S && ModifierKeys == Keys.Control)
            //{
            //    saveFile();
            //}
        }

        private void saveFile(bool saveAs = false)
        {
            if (filePath == "Untitled.txt" || saveAs == true)
            {
                var folderBrowser = new VistaSaveFileDialog();
                folderBrowser.Filter = "Text File (*.txt)|*.txt|Python File (*.py)|*.py|PHP File (*.php)|*.php|C# File (*.cs)|*.cs|C++ File (*.cpp)|*.cpp|HTML File (*.html)|*.html|Javascript File (*.js)|*.js|Java File (*.java)|*.java|C File (*.c)|*.c|H File (*.h)|*.h|Bash Shell File (*.bs)|*.bs|Swift File (*.swift)|*.swift|Visual Basic File (*.vb)|*.vb|Cascading Style Sheet File (*.css)|*.css|All files (*.*)|*.*"; ;
                if (folderBrowser.ShowDialog() != DialogResult.OK) return;
                filePath = folderBrowser.FileName;
                fileName = Path.GetFileName(filePath);
            }
            if (!File.Exists(fileName))
            {
                var createFile = File.Create(filePath);
                createFile.Close();
            }
            File.WriteAllText(filePath, textBox.Text);
            Text = filePath + " - Easy Editor";
            saveStatus = "Saved";
        }

        public void openFile(string? path = "Untitled.txt")
        {
            filePath = path;
            if (path == "Untitled.txt")
            {
                fileName = Path.GetFileName("Untitled.txt");
                var folderBrowser = new VistaOpenFileDialog();
                if (folderBrowser.ShowDialog() != DialogResult.OK) return;
                filePath = folderBrowser.FileName;
                fileName = Path.GetFileName(filePath);
            }
            else fileName = Path.GetFileName(path); fileOpened = true; 
            string file = File.ReadAllText(filePath);
            textBox.Text = file;
            Text = filePath + " - Easy Editor";
            saveStatus = "Saved";
        }

        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "New")
            {
                filePath = "Untitled.txt";
                fileName = "Untitled.txt";
                saveStatus = "Unsaved";
                Text = saveStatus + " | " + filePath + " - Easy Editor";
                textBox.Text = "";
            }
            if (e.ClickedItem.Text == "Save")
            {
                saveFile();
            }
            if (e.ClickedItem.Text == "Save As")
            {
                saveFile(true);
            }
            if (e.ClickedItem.Text == "Open")
            {
                openFile();
            }
            if (e.ClickedItem.Text == "Exit")
            {
                Application.Exit();
            }
        }

        private void editToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Undo")
            {
                textBox.Undo();
            }
            if (e.ClickedItem.Text == "Redo")
            {
                textBox.Redo();
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Y && ModifierKeys == Keys.Control)
            //{
            //    e.Handled = true;
            //    textBox.Redo();
            //}
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            storeFiles();
        }

        private void createBaseFiles()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\EasyEditor";
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            if (!Directory.Exists(dataPath + @"\Temp"))
            {
                Directory.CreateDirectory(dataPath + @"\Temp");
            }
            if (!File.Exists(dataPath + @"\Temp\Recent Files.txt"))
            {
                var createFile = File.Create(dataPath + @"\Temp\Recent Files.txt");
                createFile.Close();
            }
            if (!File.Exists(dataPath + @"\Temp\Opened Files.txt"))
            {
                var createFile = File.Create(dataPath + @"\Temp\Opened Files.txt");
                createFile.Close();
            }
            if (!Directory.Exists(dataPath + @"\Opened Files"))
            {
                Directory.CreateDirectory(dataPath + @"\Opened Files");
            }
        }

        public static string getUniqueKey(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        public async Task storeFiles()
        {
            if (textBox.TextLength != 0 || fileOpened == true)
            {
                createBaseFiles();
                string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\EasyEditor";
                if (saveStatus == "Saved")
                {
                    List<string> linesList = new List<string>();
                    linesList.Add(filePath + "|Saved");
                    await File.WriteAllLinesAsync(dataPath + @"\Temp\Opened Files.txt", linesList);
                }
                string key = getUniqueKey(10);
                if (fileOpened == false && saveStatus == "Unsaved")
                {
                    var createFile = File.Create(dataPath + @"\Opened Files\" + key + ".txt");
                    createFile.Close();
                    File.WriteAllText(dataPath + @"\Opened Files\" + key + ".txt", textBox.Text);
                    List<string> linesList = new List<string>();
                    linesList.Add("None" + "|" + key + ".txt");
                    await File.WriteAllLinesAsync(dataPath + @"\Temp\Opened Files.txt", linesList);
                }
                else if (fileOpened == true && saveStatus == "Unsaved")
                {
                    var createFile = File.Create(dataPath + @"\Opened Files\" + key + ".txt");
                    createFile.Close();
                    File.WriteAllText(dataPath + @"\Opened Files\" + key + ".txt", textBox.Text);
                    List<string> linesList = new List<string>();
                    linesList.Add(filePath + "|" + key + ".txt");
                    await File.WriteAllLinesAsync(dataPath + @"\Temp\Opened Files.txt", linesList);
                }
            }
        }

        public async Task restoreFiles()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\EasyEditor";
            fileCounter = 0;
            if (!File.ReadAllText(dataPath + @"\Temp\Opened Files.txt").ToString().ToLower().Contains('|')) return;
            foreach (string line in File.ReadLines(dataPath + @"\Temp\Opened Files.txt"))
            {
                string[] key = line.Split('|');
                string path = key[0];
                string tempPath = dataPath + @"\Opened Files\" + key[1];

                // If the file is saved
                if (key[1] == "Saved")
                {
                    saveStatus = "Saved";
                    Text = filePath + " - Easy Editor";
                    openFile(path);
                    fileCounter++;
                    return;
                }

                if (path == "None")
                {
                    openFile(tempPath);
                    Text = "Unsaved | " + "Untitled.txt" + " - Easy Editor";
                    filePath = "Untitled.txt";
                    fileName = "Untitled.txt";
                    saveStatus = "Unsaved";
                    fileOpened = false;
                    File.Delete(tempPath);
                    fileCounter++;
                    return;
                }

                // If the file is not saved
                openFile(tempPath);
                File.Delete(tempPath);
                saveStatus = "Unsaved";
                filePath = path;
                fileName = Path.GetFileName(path);
                Text = "Unsaved | " + path + " - Easy Editor";
                fileCounter++;
            }
            string[] removeLines = { "" };
            await File.WriteAllLinesAsync(dataPath + @"\Temp\Opened Files.txt", removeLines);
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

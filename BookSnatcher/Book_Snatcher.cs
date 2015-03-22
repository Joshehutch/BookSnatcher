using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;
using WindowsInput;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;


//This program works great but its all spaghetti code....dont try to read it lol

namespace BookSnatcher
{
    public partial class Book_Snatcher : Form
    {
        public Book_Snatcher()
        {
            InitializeComponent();
            this.Width = 473;
            Start.Enabled = false;
            
            #region WelcomeText
            string welcomeText = "There are a couple of parameters you need to set before starting"
                   + NL() + "You'll see them all after you close this dialog"
                   + NL() + "The first two are the corner points of the screenshots that will be"
                   + NL() + "     taken of each page"
                   + NL() + "The next one if the location of the button you click to go to the next page"
                   + NL() + "     in the eBook reader program, or if you can just use the right button"
                   + NL() + "     on your keyboard you can just check that box"
                   + NL() + "Those 3 can be set by moving your mouse to the location you want and"
                   + NL() + "     hitting the key that is specificed to the right of them to lock"
                   + NL() + "     in the mouse coordinates"
                   + NL() + "The next one is the Title bar name for the program you're using to"
                   + NL() + "     view the eBook its not case sensitive and you dont have to type in"
                   + NL() + "     the full title name, just make sure you type enough of the name"
                   + NL() + "     so the program can be singled out"
                   + NL() + "The next one is the number of pages that you want to get"
                   + NL() + "And the last one is the folder where all the screenshots will go"
                   + NL() + "If you experience problems usually checking the Slow Computer Box will"
                   + NL() + "fix them (this adds a little delay to everything)"
                   + NL() + "After you set it up just let it run and leave everything alone";
            #endregion

            MessageBox.Show(welcomeText, "Welcome to Book Snatcher!", MessageBoxButtons.OK);

            Tracker();
        }

        private async Task PutTaskDelay(int time)
        {                
            await Task.Delay(time);
        }

        private Point upperLeft;
        private Point bottomRight;
        private Point nextButton;
        private int numberOfPages;
        private bool upLeftSet = false;
        private bool botRightSet = false;
        private bool nextSet = false;
        private bool procSet = false;
        private bool folderSet = false;

        private async void Tracker()
        {
            while (started == false)
            {
                if (upLeftSet && botRightSet && nextSet && procSet && folderSet && numberOfPages > 0)
                    Start.Enabled = true;

                await PutTaskDelay(45);

                if (Convert.ToBoolean(GetAsyncKeyState(Keys.Insert)))
                {
                    upperLeft = Cursor.Position;
                    UpperLeftLabel.Text = "Upper Left Corner: Set";
                    upLeftSet = true;
                }

                if (Convert.ToBoolean(GetAsyncKeyState(Keys.Home)))
                {
                    bottomRight = Cursor.Position;
                    bottomRightLbl.Text = "Bottom Right Corner: Set";
                    botRightSet = true;
                }

                if (Convert.ToBoolean(GetAsyncKeyState(Keys.Delete)))
                {
                    nextButton = Cursor.Position;
                    NextPageLbl.Text = "Next Page Button: Set";
                    nextSet = true;
                }
            }
        }

        private void SelectectedFolder_TextChanged(object sender, EventArgs e)
        {
            if (SelectectedFolder.Text != "Choose a folder for the screenshots to go")
            {
                if (Directory.Exists(SelectectedFolder.Text) && Directory.GetFiles(SelectectedFolder.Text, "*", SearchOption.TopDirectoryOnly).Length < 1)
                    folderSet = true;
                else if (Directory.GetFiles(SelectectedFolder.Text, "*", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    MessageBox.Show("In order to work properly Book Snatcher needs to save to an empty folder"
                           + NL() + "Please create a new folder to use");
                    SelectectedFolder.Text = "Choose a folder for the screenshots to go";
                }
                else
                    MessageBox.Show("The specified folder is invalid");
            }
            else
                folderSet = false;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browsedPath = new FolderBrowserDialog();
            DialogResult result = browsedPath.ShowDialog();
            if (result == DialogResult.OK && Directory.Exists(browsedPath.SelectedPath))
            {
                SelectectedFolder.Text = browsedPath.SelectedPath;
            }
            else if(result == DialogResult.OK && Directory.Exists(browsedPath.SelectedPath) == false)
            {
                MessageBox.Show("The specified folder is invalid");
            }
        }

        private void ProcessName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ProcessSetter();
            }
        }

        private void ProcessSet_Click(object sender, EventArgs e)
        {
            ProcessSetter();
        }

        private void ProcessSetter()
        {
            List<IntPtr> processes = WindowManagement.FindWindowsWithText(ProcessName.Text.Trim(), true);
            if (processes.Count > 0)
            {
                procSet = true;
                PrcNameLbl.Text = "Window Title Name: Set";
            }
            else
            {
                MessageBox.Show("No windows were found with \"" + ProcessName.Text.Trim() + "\" in the title");
                PrcNameLbl.Text = "Window Title Name: Not Set";
                ProcessName.Text = "Enter the window title";
                procSet = false;
            }
        }

        private void PageNumberSetter()
        {
            int num;
            if (Int32.TryParse(PageNumber.Text, out num))
            {
                numberOfPages = Convert.ToInt32(PageNumber.Text);
                PagesLbl.Text = "Number of Pages: Set";
            }
            else
            {
                numberOfPages = 0;
                MessageBox.Show("Enter a number");
                PagesLbl.Text = "Number of Pages: Not Set";
            }
        }

        private void PageNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                PageNumberSetter();
            }
        }

        private void PageNmbrSet_Click(object sender, EventArgs e)
        {
            PageNumberSetter();
        }
        private IntPtr readerhWnd = new IntPtr();
        private int i;
        private bool started = false;

        private void Start_Click(object sender, EventArgs e)
        {
            if (Directory.GetFiles(SelectectedFolder.Text, "*", SearchOption.TopDirectoryOnly).Length > 0)
            {
                MessageBox.Show("In order to work properly Book Snatcher needs to save to an empty folder"
                       + NL() + "Please create a new folder to use");
                SelectectedFolder.Text = "Choose a folder for the screenshots to go";
            }
            else if(started == false && (Directory.GetFiles(SelectectedFolder.Text, "*", SearchOption.TopDirectoryOnly).Length < 1))
            {
                started = true;
                if (WindowManagement.FindWindowsWithText(ProcessName.Text.Trim(), true).Count > 0)
                {
                    readerhWnd = WindowManagement.FindWindowsWithText(ProcessName.Text.Trim(), true)[0];
                }
                else
                {
                    MessageBox.Show("No windows with \"" + ProcessName.Text.Trim() + "\" in the title were found");
                    return;
                }
                this.Width = 548;

                i = 1;
                HowDoIScreenShotted();
                SetForegroundWindow(this.Handle);
                if (i == numberOfPages + 1)
                {
                    MessageBox.Show("Done!"
                           + NL() + numberOfPages.ToString() + " screenshots."
                           + NL() + "Saved to: " + SelectectedFolder.Text);
                    started = false;
                    Tracker();
                    this.Width = 473;
                }
            }            
        }

        private async void HowDoIScreenShotted()
        {
            while (started && i <= numberOfPages)
            {
                if(Convert.ToBoolean(GetAsyncKeyState(Keys.Escape)))
                {
                    started = false;
                    SetForegroundWindow(this.Handle);
                    Tracker();
                    this.Width = 473;
                    MessageBox.Show(i - 1 + " screenshots."
                           + NL() + "Saved to: " + SelectectedFolder.Text);
                }
                SetForegroundWindow(readerhWnd);
                Rectangle bounds = new Rectangle(upperLeft.X, upperLeft.Y, bottomRight.X - upperLeft.X, bottomRight.Y - upperLeft.Y);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(upperLeft, Point.Empty, bounds.Size);
                    }
                    
                    bitmap.Save(Path.Combine(SelectectedFolder.Text, i + ".png"), ImageFormat.Png);
                }
                while (!File.Exists(Path.Combine(SelectectedFolder.Text, i + ".png")))
                {
                    await PutTaskDelay(1);
                }
                SetForegroundWindow(readerhWnd);
                if (i < numberOfPages)
                {
                    await ScreenChanged(bounds);
                }
                CurrentCount.Text = "Current" + NL() + "Count" + NL() + i;
                i++;
            }
        }




        private void PageNumber_Enter(object sender, EventArgs e)
        {
            if (PageNumber.Text.Equals("Enter a number"))
                PageNumber.Text = null;
        }

        private void ProcessName_Enter(object sender, EventArgs e)
        {
            if (ProcessName.Text.Equals("Enter the window title"))
                ProcessName.Text = null;
        }

        private void ProcessName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProcessName.Text))
                ProcessName.Text = "Enter the window title";
            else
                ProcessSetter();
        }

        private void PageNumber_Leave(object sender, EventArgs e)
        {
            if (PageNumber.Text.Equals(""))
                PageNumber.Text = "Enter a number";
            else
            {
                PageNumberSetter();
            }
        }

        private string NL()
        {
            return Environment.NewLine;
        }

        public async void LeftClick(bool clk, int x, int y)
        {
            SetCursorPos(x, y);
            await PutTaskDelay(5);
            if (clk)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
            }
        }

        public void RightClick(int x, int y)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [StructLayout(LayoutKind.Sequential)]
        struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(Boolean? filler)
                : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }

        }

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void RightButtonNext_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(RightButtonNext.Checked))
            {
                NextPageLbl.Text = "Next Page Button: Set";
                nextSet = true;
            }
        }
        
        private static readonly Object locker = new Object();

        public static String GenerateHash(Object sourceObject)
        {
            String hashString = "";

            //Catch unuseful parameter values
            if (sourceObject == null)
            {
                throw new ArgumentNullException("Null as parameter is not allowed");
            }
            else
            {
                //We determine if the passed object is really serializable.
                try
                {
                    //Now we begin to do the real work.
                    hashString = ComputeHash(ObjectToByteArray(sourceObject));
                    return hashString;
                }
                catch (AmbiguousMatchException ame)
                {
                    throw new ApplicationException("Could not definitly decide if object is serializable. Message:" + ame.Message);
                }
            }
        }

        /// <summary>
        /// Converts an object to an array of bytes. This array is used to hash the object.
        /// </summary>
        /// <param name="objectToSerialize">Just an object</param>
        /// <returns>A byte - array representation of the object.</returns>
        /// <exception cref="SerializationException">Is thrown if something went wrong during serialization.</exception>
        private static byte[] ObjectToByteArray(Object objectToSerialize)
        {
            MemoryStream fs = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                //Here's the core functionality! One Line!
                //To be thread-safe we lock the object
                lock (locker)
                {
                    formatter.Serialize(fs, objectToSerialize);
                }
                return fs.ToArray();
            }
            catch (SerializationException se)
            {
                Console.WriteLine("Error occured during serialization. Message: " + se.Message);
                return null;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Generates the hashcode of an given byte-array. The byte-array can be an object. Then the
        /// method "hashes" this object. The hash can then be used e.g. to identify the object.
        /// </summary>
        /// <param name="objectAsBytes">bytearray representation of an object.</param>
        /// <returns>The MD5 hash of the object as a string or null if it couldn't be generated.</returns>
        private static string ComputeHash(byte[] objectAsBytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();
            }
            catch (ArgumentNullException ame)
            {
                //If something occured during serialization, this method is called with an null argument. 
                Console.WriteLine("Hash has not been generated." + ame.Message);
                return null;
            }
        }

        public Bitmap Bitmapper(Rectangle wreckedAngle)
        {
            List<Bitmap> lst = new List<Bitmap>();

            var bmpScreenshot = new Bitmap(wreckedAngle.Width, wreckedAngle.Height, PixelFormat.Format32bppRgb); //Might have trouble with hashing if its not Format32bppRgb or something

            var gfxScreenshot = Graphics.FromImage(bmpScreenshot); //who knows why editing the graphics object changes the bitmap object. it just werkz.

            gfxScreenshot.CopyFromScreen(wreckedAngle.X, wreckedAngle.Y, 0, 0, wreckedAngle.Size, CopyPixelOperation.SourceCopy); //this takes the bitmap object and makes it a screenshot. Probably.

            //bmpScreenshot.Save(@"C:\test.png", ImageFormat.Png);  
            return bmpScreenshot;
        }

        private async Task PageTurn()
        {
            SetForegroundWindow(readerhWnd);
            if (Convert.ToBoolean(RightButtonNext.Checked))
            {
                InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
                if (Convert.ToBoolean(SlowMode.Checked))
                    await PutTaskDelay(20);
            }
            else
            {
                LeftClick(true, nextButton.X, nextButton.Y);
                if (Convert.ToBoolean(SlowMode.Checked))
                    await PutTaskDelay(20);
            }
        }

        private async Task ScreenChanged(Rectangle rct)
        {
            string initial = GenerateHash(Bitmapper(rct));
            string vol = initial;
            await PageTurn();

            int counter = 0;
            while (true)
            {
                vol = GenerateHash(Bitmapper(rct));
                if (vol != initial)
                    break;
                initial = vol;

                if(counter > 30)
                {
                    await PageTurn();
                    counter = 0;
                }

                counter ++;
                await PutTaskDelay(1);
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        private void Book_Snatcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (started)
            {
                SetForegroundWindow(this.Handle);
                started = false;
                MessageBox.Show(Directory.GetFiles(SelectectedFolder.Text, "*.png", SearchOption.TopDirectoryOnly).Length + " screenshots."
                       + NL() + "Saved to: " + SelectectedFolder.Text);
            }
            Process.GetCurrentProcess().Kill();
        }

    }
}

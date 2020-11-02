using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Artificial_Intelligence
{
    public partial class Artificial_Intelligence : Form
    {
        public Artificial_Intelligence()
        {
            InitializeComponent();
        }

        //THE PROCESSES ARE INITIATED
        private int i;
        private int j;
        private Random Random;
        private StreamReader File_Reader;
        private StreamWriter File_Writer;
        private ColorDialog Color_Selection;
        private Socket Socket;
        private Bitmap Picture1;
        private Bitmap Picture2;
        private Bitmap Picture_Comparison;
        private Color Comparison_Difference;
        private int Comparison_Width;
        private int Comparison_Height;
        private string Screen_Design;
        private string Memory;
        private int Random_Value;
        private int Common_Index_Counter = 0;
        private int Screen_Current_Line = 0;
        private int Screen_Current_Line_Length = 0;
        private int Screen_Maximum_Line = 43;
        private int Screen_Maximum_Line_Length = 170;
        private int Computer_Waiting_Clock = 0;
        private string File_Location;
        private string Question;
        private string Answer;
        private int Answer_Speed = 10;
        private int Insult_Counter = 0;
        private bool Did_She_Understand = false;
        private bool Color_Change_Question = false;
        private bool File_Removing_Question = false;
        private bool File_Writing_Question = false;
        private bool File_Is_Being_Written = false;
        private bool File_Reading_Question = false;
        private bool File_Is_Being_Read = false;
        private bool File_Opening_Question = false;
        private bool Picture_Comparison_Activated = false;
        private bool GIF_Maker_Activated = false;
        private bool Character_Converter_Activated = false;
        private bool Hacker_Mode_Activated = false;
        private bool AI_Is_Being_Used = false;
        private string The_Name_Of_The_Designer;
        private string The_Name_Of_The_Artificial_Intelligence;

        //THE INTERFACE IS BEING LOADED
        private void Artificial_Intelligence_Settings(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon("Artificial Intelligence Settings\\AI.ico");

            if (File.Exists("Artificial Intelligence Settings\\Design.txt"))
            {
                File_Reader = new StreamReader("Artificial Intelligence Settings\\Design.txt", Encoding.GetEncoding("iso-8859-1"));
                Screen_Design = File_Reader.ReadToEnd().Replace("\t", "        ");
                File_Reader.Close();
                File_Reader.Dispose();
            }
            else
            {
                Screen_Design = "Design.txt does not exist !";
            }
            Random = new Random();

            CenterToScreen();
            TransparencyKey = BackColor;
            Output.Parent = Avatar;
            Image1.Parent = Output;
            Image2.Parent = Output;
            Image_Comparison.Parent = Output;
            Character_Converter.Parent = Output;
            GIF_Maker.Parent = Output;
            Common_ProgressBar.Parent = Output;

            Image1.AllowDrop = true;
            Image2.AllowDrop = true;
            GIF_Maker.AllowDrop = true;
            Character_Converter.AllowDrop = true;

            Avatar.ImageLocation = "Artificial Intelligence Settings\\AI.gif";
            WMP.URL = "Artificial Intelligence Settings\\AI.mp3";
            WMP.settings.volume = 100;

            The_Name_Of_The_Artificial_Intelligence = "Morgana";
            The_Name_Of_The_Designer = "Hurkan Ugur";
            Memory = The_Name_Of_The_Designer;
        }
        //THE KEY ACTIONS
        private void Artificial_Intelligence_KeyDown(object sender, KeyEventArgs e)
        {
            //TEXTBOX CLEANING ENTER
            if (e.KeyCode == Keys.Enter && (Input.Text == "The process has been executed successfully"
                || Input.Text == "The process has been cancelled"))
            {
                e.SuppressKeyPress = true;
                Input.Text = "\0";
            }
            //NORMAL ENTER ACTIVITY
            else if (e.KeyCode == Keys.Enter && Input.Text.Length != 0
                && File_Is_Being_Written == false && File_Is_Being_Read == false
                && Picture_Comparison_Activated == false && Hacker_Mode_Activated == false
                && Character_Converter_Activated == false && GIF_Maker_Activated == false)
            {
                e.SuppressKeyPress = true;
                Question = Input.Text.Trim();
                if (Question.Length != 0)
                {
                    Memory = Input.Text;
                    Input.Enabled = false;
                    THE_ARTIFICIAL_INTELLIGENCE();
                }
                else
                    Input.Text = "\0";
            }
            //EMPTY TEXTBOX ENTER
            else if (e.KeyCode == Keys.Enter && Input.Text.Length == 0)
            {
                e.SuppressKeyPress = true;
            }
            //FILE WRITING ENTER
            else if (e.KeyCode == Keys.Enter && File_Is_Being_Written == true)
            {
                e.SuppressKeyPress = true;
                Input.Text += "%";
                Input.SelectionStart = Input.Text.Length;
            }
            //FILE WRITING ESC
            else if (e.KeyCode == Keys.Escape && File_Is_Being_Written == true)
            {
                File_Is_Being_Written = false;
                e.SuppressKeyPress = true;
                File_Writer.Write(Output.Text.Replace("\n", Environment.NewLine));
                File_Writer.Close();
                File_Writer.Dispose();
                Input.Enabled = false;
                Input.Text = "\0";
                Output.Text = "\0";
                Output.Font = new Font("Arial Black", 16, FontStyle.Bold);
                Output.TextAlign = ContentAlignment.BottomCenter;

                Answer = "The process has been executed successfully";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //FILE READING ESC ACTIVITY
            else if (e.KeyCode == Keys.Escape && File_Is_Being_Read == true)
            {
                File_Is_Being_Read = false;
                e.SuppressKeyPress = true;
                Screen_Current_Line = 0;
                Screen_Current_Line_Length = 0;
                Common_Index_Counter = 0;
                Question = "The process has been cancelled";
                Input.Text = "\0";
                AI_ANSWER_FUNCTION("INPUT");
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //PICTURE COMPARISON ESC
            else if (e.KeyCode == Keys.Escape && Picture_Comparison_Activated == true
                && Common_ProgressBar.Value == 0)
            {
                Picture_Comparison_Activated = false;
                e.SuppressKeyPress = true;
                Image1.Visible = false;
                Image2.Visible = false;
                Image_Comparison.Visible = false;
                Common_ProgressBar.Visible = false;
                Image1.ImageLocation = "";
                Image2.ImageLocation = "";
                Image_Comparison.ImageLocation = "";

                try { Picture1.Dispose(); } catch (Exception) { }
                try { Picture2.Dispose(); } catch (Exception) { }
                try { Picture_Comparison.Dispose(); } catch (Exception) { }

                Input.Text = "\0";
                Output.Text = "\0";
                Answer = "The process has been cancelled";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");

                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //GIF MAKER ESC
            else if (e.KeyCode == Keys.Escape && GIF_Maker_Activated == true
                && Common_ProgressBar.Value == 0)
            {
                GIF_Maker_Activated = false;
                e.SuppressKeyPress = true;
                GIF_Maker.Visible = false;
                Common_ProgressBar.Visible = false;
                GIF_Maker.ImageLocation = "";

                Input.Text = "\0";
                Output.Text = "\0";
                Answer = "The process has been cancelled";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");

                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //CHARACTER CONVERTER ESC
            else if (e.KeyCode == Keys.Escape && Character_Converter_Activated == true
                && Common_ProgressBar.Value == 0)
            {
                Character_Converter_Activated = false;
                e.SuppressKeyPress = true;
                Character_Converter.Visible = false;
                Common_ProgressBar.Visible = false;
                try { File_Writer.Close(); } catch (Exception) { }
                try { File_Writer.Dispose(); } catch (Exception) { }
                try { File_Reader.Close(); } catch (Exception) { }
                try { File_Reader.Dispose(); } catch (Exception) { }
                Answer = "The process has been cancelled";
                Output.Text = "\0";
                Input.Text = "\0";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //HACKER MODE ACTIVATED
            else if (Hacker_Mode_Activated == true && e.KeyCode != Keys.Escape)
            {
                e.SuppressKeyPress = true;
                for (i = 0; i < 3; i++)
                {
                    Output.Text += Convert.ToString(Screen_Design[Common_Index_Counter]);
                    if (char.IsUpper(Screen_Design[Common_Index_Counter]) == true)
                        Screen_Current_Line_Length += 2;
                    else
                        Screen_Current_Line_Length++;

                    if (Screen_Current_Line_Length >= Screen_Maximum_Line_Length)
                    {
                        Screen_Current_Line++;
                        Screen_Current_Line_Length = 0;
                    }
                    if (Screen_Design[Common_Index_Counter] == '\n')
                    {
                        Screen_Current_Line++;
                        Screen_Current_Line_Length = 0;
                    }
                    if (Screen_Current_Line >= Screen_Maximum_Line)
                    {
                        Output.Text = "\0";
                        Screen_Current_Line = 0;
                        Screen_Current_Line_Length = 0;
                    }

                    Common_Index_Counter++;
                    if (Common_Index_Counter == Screen_Design.Length)
                    {
                        Screen_Current_Line = 0;
                        Screen_Current_Line_Length = 0;
                        Output.Text = "\0";
                        Common_Index_Counter = 0;
                    }
                    Refresh();
                }
            }
            //HACKER MODE ESC
            else if (Hacker_Mode_Activated == true && e.KeyCode == Keys.Escape)
            {
                Hacker_Mode_Activated = false;
                e.SuppressKeyPress = true;
                Screen_Current_Line = 0;
                Screen_Current_Line_Length = 0;
                Common_Index_Counter = 0;
                Output.Text = "\0";
                Input.Text = "\0";
                Output.Font = new Font("Arial Black", 16, FontStyle.Bold);
                Output.TextAlign = ContentAlignment.BottomCenter;
                Answer = "The process has been cancelled";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
                AI_Is_Being_Used = false;
            }
            //REWRITE THE PREVIOUS TEXT
            else if (e.KeyCode == Keys.Right && AI_Is_Being_Used == false && Input.Text.Length == 0)
            {
                Input.Enabled = false;
                e.SuppressKeyPress = true;
                Input.Text = "\0";
                for (i = 0; i < Memory.Length; i++)
                {
                    Input.Text += Convert.ToString(Memory[i]);
                    this.Refresh();
                    Thread.Sleep(Answer_Speed);
                }
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
            }
        }

        //IMAGE DRAG & DROP EVENTS
        private void Image1_DragEnter(object sender, DragEventArgs e)
        {
            if (Picture_Comparison_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() == ".jpg"
                   || Path.GetExtension(Information[0]).ToLower() == ".jpeg"
                    || Path.GetExtension(Information[0]).ToLower() == ".png")
                {
                    try { e.Effect = DragDropEffects.Link; } catch (Exception) { }
                }
                else
                {
                    try { e.Effect = DragDropEffects.None; } catch (Exception) { }
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        private void Image1_DragDrop(object sender, DragEventArgs e)
        {
            if (Picture_Comparison_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() == ".jpg"
               || Path.GetExtension(Information[0]).ToLower() == ".jpeg"
                || Path.GetExtension(Information[0]).ToLower() == ".png")
                {
                    Image1.ImageLocation = Path.GetFullPath(Information[0]);
                    Picture1 = new Bitmap(Path.GetFullPath(Information[0]));

                    Picture_Comparison = new Bitmap(Path.GetFullPath(Information[0]));
                    Image_Comparison.Image = Picture_Comparison;
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        private void Image2_DragEnter(object sender, DragEventArgs e)
        {
            if (Picture_Comparison_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() == ".jpg"
                   || Path.GetExtension(Information[0]).ToLower() == ".jpeg"
                    || Path.GetExtension(Information[0]).ToLower() == ".png")
                {
                    try { e.Effect = DragDropEffects.Link; } catch (Exception) { }
                }
                else
                {
                    try { e.Effect = DragDropEffects.None; } catch (Exception) { }
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        private void Image2_DragDrop(object sender, DragEventArgs e)
        {
            if (Picture_Comparison_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() == ".jpg"
                   || Path.GetExtension(Information[0]).ToLower() == ".jpeg"
                    || Path.GetExtension(Information[0]).ToLower() == ".png")
                {
                    Image2.ImageLocation = Path.GetFullPath(Information[0]);
                    Picture2 = new Bitmap(Path.GetFullPath(Information[0]));
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        //CHARACTER CONVERTER DRAG & DROP EVENTS
        private void Character_Converter_DragDrop(object sender, DragEventArgs e)
        {
            if (Character_Converter_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() != ".jpg"
                   && Path.GetExtension(Information[0]).ToLower() != ".jpeg"
                    && Path.GetExtension(Information[0]).ToLower() != ".png"
                    && Path.GetExtension(Information[0]).ToLower() != ".gif"
                    && Path.GetExtension(Information[0]).ToLower() != ".bmp")
                {
                    Question = Path.GetFullPath(Information[0]);
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        private void Character_Converter_DragEnter(object sender, DragEventArgs e)
        {
            if (Character_Converter_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(Information[0]).ToLower() != ".jpg"
                   && Path.GetExtension(Information[0]).ToLower() != ".jpeg"
                    && Path.GetExtension(Information[0]).ToLower() != ".png"
                    && Path.GetExtension(Information[0]).ToLower() != ".gif"
                    && Path.GetExtension(Information[0]).ToLower() != ".bmp")
                {
                    try { e.Effect = DragDropEffects.Link; } catch (Exception) { }
                }
                else
                {
                    try { e.Effect = DragDropEffects.None; } catch (Exception) { }
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        //GIF MAKER DRAG & DROP EVENTS
        private void GIF_Maker_DragEnter(object sender, DragEventArgs e)
        {
            if (GIF_Maker_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool Everything_Is_Convenient = true;
                foreach (string Search in Information)
                {
                    if (Path.GetExtension(Search).ToLower() != ".jpg"
                   && Path.GetExtension(Search).ToLower() != ".jpeg"
                    && Path.GetExtension(Search).ToLower() != ".png")
                    {
                        Everything_Is_Convenient = false;
                    }
                }
                if (Everything_Is_Convenient == true)
                {
                    try { e.Effect = DragDropEffects.Link; } catch (Exception) { }
                }
                else
                {
                    try { e.Effect = DragDropEffects.None; } catch (Exception) { }
                }
                Array.Clear(Information, 0, Information.Length);
            }
        }
        private void GIF_Maker_DragDrop(object sender, DragEventArgs e)
        {
            if (GIF_Maker_Activated == true && Common_ProgressBar.Value == 0)
            {
                string[] Information = (string[])e.Data.GetData(DataFormats.FileDrop);
                Output.Text = "\0";

                //Create a GIF Directory
                Random_Value = Random.Next(1000000000, Int32.MaxValue);
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\GIFs " + Random_Value) == false)
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                        + "\\GIFs " + Random_Value);
                }
                else
                {
                    try
                    {
                        //Deteles Subfiles
                        Array.ForEach(Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            + "\\GIFs " + Random_Value), File.Delete);
                        //Deletes Subdirectories
                        foreach (var Alt_Klasorler in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            + "\\GIFs " + Random_Value).GetDirectories())
                        {
                            Alt_Klasorler.Delete(true);
                        }
                    }
                    catch (Exception)
                    {
                        Input.Text = "\0";
                        GIF_Maker.Visible = false;
                        Common_ProgressBar.Visible = false;
                        Answer = "The directory \"GIFs " + Random_Value + "\" is being used right now";
                        Question = "The process has been cancelled";
                        AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                        Input.Enabled = true;
                        Input.Focus();
                        Input.Select(Input.TextLength, Input.TextLength);
                        GIF_Maker_Activated = false;
                        AI_Is_Being_Used = false;
                    }
                }

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\GIFs " + Random_Value) == true && GIF_Maker_Activated == true)
                {
                    //Copying images to the directory "GIFs"
                    for (i = 0; i < Information.Length; i++)
                    {
                        if (File.Exists(Information[i]))
                        {
                            if (Information[i].ToLower().EndsWith(".png") == true)
                                File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                    + "\\GIFs " + Random_Value + "\\" + Convert.ToString((i + 1)) + ".png");
                            else if (Information[i].ToLower().EndsWith(".jpg") == true)
                                File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                    + "\\GIFs " + Random_Value + "\\" + Convert.ToString((i + 1)) + ".jpg");
                            else if (Information[i].ToLower().EndsWith(".jpeg") == true)
                                File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                    + "\\GIFs " + Random_Value + "\\" + Convert.ToString((i + 1)) + ".jpeg");
                        }
                    }

                    //GIFs are created
                    for (i = 0; i < Information.Length; i++)
                    {
                        if (i != 0 && Information.Length - 1 != i)
                        {
                            if (File.Exists(Information[i]))
                            {
                                if (Information[i].ToLower().EndsWith(".png") == true)
                                    File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                        + "\\GIFs " + Random_Value + "\\"
                                        + Convert.ToString(Information.Length + Information.Length - (i + 1)) + ".png");
                                else if (Information[i].ToLower().EndsWith(".jpg") == true)
                                    File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                        + "\\GIFs " + Random_Value + "\\"
                                        + Convert.ToString(Information.Length + Information.Length - (i + 1)) + ".jpg");
                                else if (Information[i].ToLower().EndsWith(".jpeg") == true)
                                    File.Copy(Information[i], Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                        + "\\GIFs " + Random_Value + "\\"
                                        + Convert.ToString(Information.Length + Information.Length - (i + 1)) + ".jpeg");
                            }
                        }
                        if ((i + 1) != Information.Length)
                        {
                            Common_ProgressBar.Value = (Common_Index_Counter * 100) / (Information.Length - 1);
                            Common_Index_Counter++;
                        }
                        else if (Information.Length == 1)
                            Common_ProgressBar.Value = 100;
                        Refresh();
                    }

                    Input.Text = "\0";
                    Answer = "The process has been executed successfully";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                    Input.Enabled = true;
                    Input.Focus();
                    Input.Select(Input.TextLength, Input.TextLength);
                    GIF_Maker_Activated = false;
                    AI_Is_Being_Used = false;
                }
            }
        }

        //THE TIME LINE
        private void Clock_Tower(object sender, EventArgs e)
        {
            //PREVENT TRANSPARENT BUGS
            Refresh();
            //DO NOT LET THE MUSIC STOP
            WMP.Ctlcontrols.play();
            //COMPUTER WAITING COUNTER ACTIVATED
            if (AI_Is_Being_Used == false && Computer_Waiting_Clock < 50 && Input.Text.Length == 0)
            {
                if (Timer.Interval != 100)
                    Timer.Interval = 100;
                Computer_Waiting_Clock++;
                if (Computer_Waiting_Clock == 50)
                {
                    Screen_Current_Line = 0;
                    Output.Text = "\0";
                    Common_Index_Counter = 0;
                    Timer.Interval = 1;
                    Screen_Current_Line_Length = 0;
                    Output.Font = new Font(Output.Font.Name, 6, Output.Font.Style);
                    Output.TextAlign = ContentAlignment.TopLeft;
                    Image1.Visible = Image2.Visible = Image_Comparison.Visible = false;
                    Character_Converter.Visible = GIF_Maker.Visible = Common_ProgressBar.Visible = false;
                    Image1.ImageLocation = "";
                    Image2.ImageLocation = "";
                    Image_Comparison.ImageLocation = "";
                    GIF_Maker.ImageLocation = "";
                    Character_Converter.ImageLocation = "";
                    try { Picture1.Dispose(); } catch (Exception) { }
                    try { Picture2.Dispose(); } catch (Exception) { }
                    try { Picture_Comparison.Dispose(); } catch (Exception) { }
                }
            }
            else if (AI_Is_Being_Used == false && Computer_Waiting_Clock < 50 && Input.Text.Length != 0)
            {
                if (Timer.Interval != 100)
                    Timer.Interval = 100;
                if (Computer_Waiting_Clock != 0)
                    Computer_Waiting_Clock = 0;
            }
            //DESIGN ACTIVATED
            else if (AI_Is_Being_Used == false && Computer_Waiting_Clock == 50)
            {
                Output.Text += Convert.ToString(Screen_Design[Common_Index_Counter]);

                if (char.IsUpper(Screen_Design[Common_Index_Counter]) == true)
                    Screen_Current_Line_Length += 2;
                else
                    Screen_Current_Line_Length++;

                if (Screen_Current_Line_Length >= Screen_Maximum_Line_Length)
                {
                    Screen_Current_Line++;
                    Screen_Current_Line_Length = 0;
                }
                if (Screen_Design[Common_Index_Counter] == '\n')
                {
                    Screen_Current_Line++;
                    Screen_Current_Line_Length = 0;
                }
                if (Screen_Current_Line >= Screen_Maximum_Line)
                {
                    Output.Text = "\0";
                    Screen_Current_Line = 0;
                    Screen_Current_Line_Length = 0;
                }
                Common_Index_Counter++;
                if (Common_Index_Counter == Screen_Design.Length)
                {
                    Screen_Current_Line = 0;
                    Screen_Current_Line_Length = 0;
                    Output.Text = "\0";
                    Common_Index_Counter = 0;
                }
            }
            //FILE IS BEING WRITTEN ACTIVITY
            else if (File_Is_Being_Written == true)
            {
                if (Timer.Interval != Answer_Speed)
                    Timer.Interval = Answer_Speed;
                Output.Text = Input.Text.Replace("%", "\n"); // %  means '\n' according to my code
            }
            //FILE IS BEING READ ACTIVITY
            else if (File_Is_Being_Read == true)
            {
                if (Timer.Interval != Answer_Speed)
                    Timer.Interval = Answer_Speed;
                try
                {
                    if (Common_Index_Counter < Answer.Length)
                    {
                        for (i = 0; i < 20; i++)
                        {
                            if (Common_Index_Counter < Answer.Length)
                            {
                                Output.Text += Convert.ToString(Answer[Common_Index_Counter]);

                                if (char.IsUpper(Answer[Common_Index_Counter]) == true)
                                    Screen_Current_Line_Length += 2;
                                else
                                    Screen_Current_Line_Length++;

                                if (Screen_Current_Line_Length >= Screen_Maximum_Line_Length)
                                {
                                    Screen_Current_Line++;
                                    Screen_Current_Line_Length = 0;
                                }
                                if (Answer[Common_Index_Counter] == '\n')
                                {
                                    Screen_Current_Line++;
                                    Screen_Current_Line_Length = 0;
                                }
                                if (Screen_Current_Line >= Screen_Maximum_Line)
                                {
                                    Output.Text = "\0";
                                    Screen_Current_Line = 0;
                                    Screen_Current_Line_Length = 0;
                                }
                                Common_Index_Counter++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Screen_Current_Line = 0;
                        Screen_Current_Line_Length = 0;
                        Common_Index_Counter = 0;
                        Input.Text = "\0";
                        Question = "The process has been executed successfully";
                        AI_ANSWER_FUNCTION("INPUT");
                        Input.Enabled = true;
                        Input.Focus();
                        Input.Select(Input.TextLength, Input.TextLength);
                        AI_Is_Being_Used = false;
                        File_Is_Being_Read = false;
                    }
                }
                catch (Exception)
                {
                    Screen_Current_Line = 0;
                    Screen_Current_Line_Length = 0;
                    Common_Index_Counter = 0;
                    Input.Text = "\0";
                    Output.Text = "\0";
                    Output.Font = new Font("Arial Black", 16, FontStyle.Bold);
                    Output.TextAlign = ContentAlignment.BottomCenter;
                    Answer = "An error has occured during File Reading";
                    Question = "The process has been cancelled";
                    Input.Text = "\0";
                    Output.Text = "\0";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                    Input.Enabled = true;
                    Input.Focus();
                    Input.Select(Input.TextLength, Input.TextLength);
                    AI_Is_Being_Used = false;
                    File_Is_Being_Read = false;
                }
            }
            //PICTURE COMPARISON ACTIVITY
            else if (Picture_Comparison_Activated == true)
            {
                if (Timer.Interval != 100)
                    Timer.Interval = 100;
                if (Image1.ImageLocation != "" && Image2.ImageLocation != "")
                {
                    if (Input.Text != ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .")
                    {
                        Common_Index_Counter = 1;
                        Comparison_Height = 0;
                        Comparison_Width = 0;
                        Output.Text = "\0";
                        Image_Comparison.Visible = true;
                        Refresh();
                        Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                    }
                    else
                    {
                        //Size Comparison
                        if (Picture1.Width < Picture2.Width)
                            Comparison_Width = Picture1.Width;
                        else
                            Comparison_Width = Picture2.Width;

                        if (Picture1.Height < Picture2.Height)
                            Comparison_Height = Picture1.Height;
                        else
                            Comparison_Height = Picture2.Height;

                        //Image Comparison
                        for (i = 0; i < Comparison_Width; i++)
                        {
                            for (int j = 0; j < Comparison_Height; j++)
                            {
                                if (Picture1.GetPixel(i, j) != Picture2.GetPixel(i, j))
                                {
                                    Picture_Comparison.SetPixel(i, j, Comparison_Difference);
                                }
                                Common_ProgressBar.Value = (Common_Index_Counter * 100) / (Comparison_Width * Comparison_Height);
                                Common_Index_Counter++;
                            }
                        }

                        //End Of The Comparison
                        try
                        {
                            Picture_Comparison.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                + "\\Picture Comparison " + Random.Next(1000000000, Int32.MaxValue)
                                + Path.GetExtension(Image1.ImageLocation));
                        }
                        catch (Exception) { }

                        Answer = "The process has been executed successfully";
                        Input.Text = "\0";
                        Output.Text = "\0";
                        Common_Index_Counter = 0;
                        AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                        Input.Enabled = true;
                        Input.Focus();
                        Input.Select(Input.TextLength, Input.TextLength);
                        Picture_Comparison_Activated = false;
                        AI_Is_Being_Used = false;
                    }
                }
            }
            //CHARACTER CONVERTER ACTIVITY
            else if (Character_Converter_Activated == true)
            {
                if (Timer.Interval != 100)
                    Timer.Interval = 100;
                if (Question != "\0")
                {
                    if (Input.Text != ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .")
                    {
                        try
                        {
                            File_Reader = new StreamReader(Question, Encoding.GetEncoding("windows-1254"));
                            Answer = File_Reader.ReadToEnd();
                            try { File_Reader.Close(); } catch (Exception) { }
                            try { File_Reader.Dispose(); } catch (Exception) { }

                            Common_Index_Counter = 1;
                            Output.Text = "\0";
                            Input.Text = "\0";
                            Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                        }
                        catch (Exception)
                        {
                            Character_Converter.Visible = false;
                            Common_ProgressBar.Visible = false;
                            try { File_Writer.Close(); } catch (Exception) { }
                            try { File_Writer.Dispose(); } catch (Exception) { }
                            try { File_Reader.Close(); } catch (Exception) { }
                            try { File_Reader.Dispose(); } catch (Exception) { }
                            Answer = "An error has occured during File Reading";
                            Question = "The process has been cancelled";
                            Output.Text = "\0";
                            Input.Text = "\0";
                            AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                            Input.Enabled = true;
                            Input.Focus();
                            Input.Select(Input.TextLength, Input.TextLength);
                            Character_Converter_Activated = false;
                            AI_Is_Being_Used = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            //Beginning of the Character Converter
                            Answer = Answer.Replace("ç", "c");
                            Common_ProgressBar.Value = 8;
                            Refresh();
                            Answer = Answer.Replace("Ç", "C");
                            Common_ProgressBar.Value = 16;
                            Refresh();
                            Answer = Answer.Replace("ğ", "g");
                            Common_ProgressBar.Value = 24;
                            Refresh();
                            Answer = Answer.Replace("Ğ", "G");
                            Common_ProgressBar.Value = 32;
                            Refresh();
                            Answer = Answer.Replace("ı", "i");
                            Common_ProgressBar.Value = 40;
                            Refresh();
                            Answer = Answer.Replace("İ", "I");
                            Common_ProgressBar.Value = 48;
                            Refresh();
                            Answer = Answer.Replace("ş", "s");
                            Common_ProgressBar.Value = 56;
                            Refresh();
                            Answer = Answer.Replace("Ş", "S");
                            Common_ProgressBar.Value = 64;
                            Refresh();
                            Answer = Answer.Replace("ö", "o");
                            Common_ProgressBar.Value = 72;
                            Refresh();
                            Answer = Answer.Replace("Ö", "O");
                            Common_ProgressBar.Value = 80;
                            Refresh();
                            Answer = Answer.Replace("ü", "u");
                            Common_ProgressBar.Value = 90;
                            Refresh();
                            Answer = Answer.Replace("Ü", "U");
                            Common_ProgressBar.Value = 100;
                            Refresh();

                            //End of the Character Converter
                            File_Writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                + "\\Character Converter " + Random.Next(1000000000, Int32.MaxValue) + Path.GetExtension(Question));
                            File_Writer.Write(Answer);
                            try { File_Writer.Close(); } catch (Exception) { }
                            try { File_Writer.Dispose(); } catch (Exception) { }
                            Output.Text = "\0";
                            Input.Text = "\0";
                            Answer = "The process has been executed successfully";
                            AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                            Input.Enabled = true;
                            Input.Focus();
                            Input.Select(Input.TextLength, Input.TextLength);
                            Character_Converter_Activated = false;
                            AI_Is_Being_Used = false;
                        }
                        catch (Exception)
                        {
                            Image_Comparison.Visible = false;
                            Common_ProgressBar.Visible = false;
                            try { File_Writer.Close(); } catch (Exception) { }
                            try { File_Writer.Dispose(); } catch (Exception) { }
                            try { File_Reader.Close(); } catch (Exception) { }
                            try { File_Reader.Dispose(); } catch (Exception) { }
                            Answer = "An error has occured during File Writing";
                            Question = "The process has been cancelled";
                            Output.Text = "\0";
                            Input.Text = "\0";
                            AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                            Input.Enabled = true;
                            Input.Focus();
                            Input.Select(Input.TextLength, Input.TextLength);
                            Character_Converter_Activated = false;
                            AI_Is_Being_Used = false;
                        }
                    }
                }
            }
        }

        /* * * * * * * * * FUNCTIONS FOR ARTIFICIAL INTELLIGENCE * * * * * * * * */

        //COMPREHENSION FUNCTION
        private void DID_SHE_UNDERSTAND()
        {
            if (Did_She_Understand == true)
                Output.Text += "\n";
            else
                Did_She_Understand = true;
        }
        //ANSWER FUNCTION FOR ARTIFICIAL INTELLIGENCE
        private void AI_ANSWER_FUNCTION(string TYPE)
        {
            if (TYPE == "OUTPUT")
            {
                for (i = 0; i < Answer.Length; i++)
                {
                    Output.Text += Convert.ToString(Answer[i]);
                    this.Refresh();
                    Thread.Sleep(Answer_Speed);
                }
            }
            else if (TYPE == "INPUT")
            {
                for (i = 0; i < Question.Length; i++)
                {
                    Input.Text += Convert.ToString(Question[i]);
                    this.Refresh();
                    Thread.Sleep(Answer_Speed);
                }
            }
            else if (TYPE == "INPUT & OUTPUT [SAME]")
            {
                for (i = 0; i < Answer.Length; i++)
                {
                    Output.Text += Convert.ToString(Answer[i]);
                    Input.Text += Convert.ToString(Answer[i]);
                    this.Refresh();
                    Thread.Sleep(Answer_Speed);
                }
            }
            else if (TYPE == "INPUT & OUTPUT [DIFFERENT]")
            {
                for (i = 0; i < Answer.Length; i++)
                {
                    Output.Text += Convert.ToString(Answer[i]);
                    if (i < Question.Length)
                        Input.Text += Convert.ToString(Question[i]);
                    this.Refresh();
                    Thread.Sleep(Answer_Speed);
                }
            }
        }
        //POSITIVE GRAMMAR FUNCTION
        private bool POSITIVE_AUXILLIARY_VERBS()
        {
            if (Question.Contains("are") || Question.Contains("\'re") || Question.Contains("were")
            || Question.Contains("have") || Question.Contains("\'ve") || Question.Contains("had")
            || Question.Contains("been") || Question.Contains("would") || Question.Contains("\'d")
            || Question.Contains("will") || Question.Contains("\'ll") || Question.Contains(" be "))
                return true;
            return false;
        }
        //NEGATIVE GRAMMAR FUNCTION
        private bool NEGATIVE_AUXILLIARY_VERBS()
        {
            if (Question.Contains("not") || Question.Contains("n\'t") || Question.Contains("never"))
                return true;
            return false;
        }
        //QUESTION DISCRIMINATION FUNCTION
        private bool QUESTION_OR_SENTENCE()
        {
            if (Question.StartsWith("are") || Question.StartsWith("is") || Question.StartsWith("was")
                || Question.StartsWith("were") || Question.StartsWith("have") || Question.StartsWith("has")
                || Question.StartsWith("had") || Question.StartsWith("would") || Question.StartsWith("will")
                || Question.StartsWith("won\'t"))
                return true;
            return false;
        }
        //CHECKS WHETHER ANY QUESTION HAS BEEN ACTIVATED OR NOT
        private bool QUESTIONS_ACTIVATED()
        {
            if (File_Writing_Question == false && File_Reading_Question == false
                && Color_Change_Question == false && File_Removing_Question == false
                && File_Opening_Question == false)
                return false;
            return true;
        }
        //CHECKS WHETHER ANY APPLICATION HAS BEEN ACTIVATED OR NOT
        private bool APPLICATIONS_ACTIVATED()
        {
            if (File_Is_Being_Written == false && File_Is_Being_Read == false
                && Picture_Comparison_Activated == false && Character_Converter_Activated == false
                && Hacker_Mode_Activated == false && GIF_Maker_Activated == false)
                return false;
            return true;
        }
        //CHECKS WHETHER CONTEXT CONTAINS SWEET WORDS OR NOT
        private bool COMPLIMENTS()
        {
            if (Question.Contains("exciting") || Question.Contains("excellent")
                || Question.Contains("magnificent") || Question.Contains("gorgeous")
                || Question.Contains("glorious") || Question.Contains("amazing")
                || Question.Contains("fun") || Question.Contains("wonderful")
                || Question.Contains("brilliant") || Question.Contains("loyal")
                || Question.Contains("smart") || Question.Contains("logic")
                || Question.Contains("royal") || Question.Contains("happy")
                || Question.Contains("surprising") || Question.Contains("lovely")
                || Question.Contains("friendly") || Question.Contains("genius")
                || Question.Contains("beautiful") || Question.Contains("stunning")
                || Question.Contains("flower") || Question.Contains("hot")
                || Question.Contains("good") || Question.Contains("clever")
                || Question.Contains("cute") || Question.Contains("sweet")
                || Question.Contains("attractive") || Question.Contains("magical")
                || Question.Contains("enchanting") || Question.Contains("perfect")
                || Question.Contains("sexy") || Question.Contains("hardworking")
                || Question.Contains("talent") || Question.Contains("skill")
                || Question.Contains("cool") || Question.Contains("nice")
                || Question.Contains("pretty") || Question.Contains("polite")
                || Question.Contains("excited") || Question.Contains("emotional")
                || Question.Contains("cheerful") || Question.Contains("hilarious")
                || Question.Contains("sensitive") || Question.Contains("affectionate")
                || Question.Contains(" okay") || Question.Contains(" ok")
                || Question.Contains("fine") || Question.Contains("nice")
                || Question.Contains("angel") || Question.Contains("flower")
                || Question.Contains("kind") || Question.Contains("tolerant")
                || Question.Contains("patient") || Question.Contains("passion")
                || Question.Contains("compassion") || Question.Contains("strong"))
                return true;
            return false;
        }
        //CHECKS WHETHER CONTEXT CONTAINS BAD WORDS OR NOT
        private bool INSULT()
        {
            if (Question.Contains("weak") || Question.Contains("boring")
                || Question.Contains("angry") || Question.Contains("sad")
                || Question.Contains("rude") || Question.Contains("evil")
                || Question.Contains("aloof") || Question.Contains("arrogant")
                || Question.Contains("cruel") || Question.Contains("jealous")
                || Question.Contains("lazy") || Question.Contains("mean")
                || Question.Contains("naughty") || Question.Contains("selfish")
                || Question.Contains("sarcastic") || Question.Contains("ruthless")
                || Question.Contains("upset") || Question.Contains("ugly")
                || Question.Contains("liar") || Question.Contains("bad")
                || Question.Contains("hideous") || Question.Contains("wors")
                || Question.Contains("disgusting") || Question.Contains("annoying")
                || Question.Contains("irritating") || Question.Contains("frustrating")
                || Question.Contains("disturbing") || Question.Contains("evil")
                || Question.Contains("satan") || Question.Contains("devil")
                || Question.Contains("stink") || Question.Contains("envious")
                || Question.Contains("enem") || Question.Contains("vomit")
                || Question.Contains("poop") || Question.Contains("spoil")
                || Question.Contains("toxic") || Question.Contains("poison")
                || Question.Contains("aggressive") || Question.Contains("monster")
                || Question.Contains("abominat") || Question.Contains("racist")
                || Question.Contains("looser"))
                return true;
            return false;
        }
        //CHECKS WHETHER CONTEXT CONTAINS SWEAR WORDS OR NOT
        private bool SWEAR_WORDS()
        {
            if (Question.Contains("fuck") || Question.Contains("shit") || Question.Contains("penis")
                || Question.Contains("bastard") || Question.Contains("pussy") || Question.Contains("vagina")
                || Question.Contains("rapist") || Question.Contains("stupid") || Question.Contains("retarded")
                || Question.Contains("bitch") || Question.Contains("porn") || Question.Contains("asshole")
                || Question.Contains("sucker") || Question.Contains("wtf") || Question.Contains("whore")
                || Question.Contains("pervert") || Question.Contains("prostitut") || Question.Contains(" hell ")
                || Question.EndsWith(" hell") || Question.StartsWith("hell ") || Question.StartsWith("ass ")
                || Question.Contains(" ass ") || Question.EndsWith(" ass") || Question.Contains("cunt")
                || Question.Contains(" dick ") || Question.StartsWith("dick") || Question.EndsWith(" dick")
                || Question.Contains("idiot") || Question.Contains("silly") || Question.Contains("damn")
                || Question.Contains("crap") || Question.Contains("freak") || Question.Contains("sex")
                || Question.Contains("gay") || Question.Contains("lesbian") || Question.Contains("fool")
                || Question == "hell" || Question == "ass" || Question == "dick")
                return true;
            return false;
        }
        //FILE PROCESS MESSAGE FUNCTION
        private void FILE_PROCESSES_MESSAGE(string TYPE)
        {
            if (TYPE == "[DRIVER ERROR MESSAGE]")
            {
                Answer = "[Example]:  \" C:\\ \"  \" D:\\ \"\nLocations must contain driver names !";
                Question = "The process has been cancelled";
                Input.Text = "\0";
                Output.Text = "\0";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
            }
            else if (TYPE == "[EXTENSION ERROR MESSAGE]")
            {
                Answer = "[Example]:  \" .txt \"  \" .mp3 \"\nLocations must contain extensions !";
                Question = "The process has been cancelled";
                Input.Text = "\0";
                Output.Text = "\0";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
            }
            else if (TYPE == "[LOCATION ERROR MESSAGE]")
            {
                Answer = "[Location]:  \" " + File_Location + " \"\nDoes not exist !";
                Question = "The process has been cancelled";
                Input.Text = "\0";
                Output.Text = "\0";
                AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
            }
        }
        //CHECKS WHETHER THERE IS A DRIVER OR NOT
        private bool DRIVER_EXISTS()
        {
            if (File_Location.StartsWith("a:\\") || File_Location.StartsWith("b:\\") || File_Location.StartsWith("c:\\")
                || File_Location.StartsWith("d:\\") || File_Location.StartsWith("e:\\") || File_Location.StartsWith("f:\\")
                || File_Location.StartsWith("g:\\") || File_Location.StartsWith("h:\\") || File_Location.StartsWith("i:\\")
                || File_Location.StartsWith("j:\\") || File_Location.StartsWith("k:\\") || File_Location.StartsWith("l:\\")
                || File_Location.StartsWith("m:\\") || File_Location.StartsWith("o:\\") || File_Location.StartsWith("p:\\")
                || File_Location.StartsWith("r:\\") || File_Location.StartsWith("s:\\") || File_Location.StartsWith("t:\\")
                || File_Location.StartsWith("u:\\") || File_Location.StartsWith("v:\\") || File_Location.StartsWith("y:\\")
                || File_Location.StartsWith("z:\\") || File_Location.StartsWith("q:\\") || File_Location.StartsWith("w:\\")
                || File_Location.StartsWith("x:\\"))
                return true;
            return false;
        }

        //THE BRAIN OF THE ARTIFICIAL INTELLIGENCE
        private void THE_ARTIFICIAL_INTELLIGENCE()
        {
            //THE STARTING SETTINGS OF THE BRAIN
            AI_Is_Being_Used = true;
            Question = Question.ToLower();
            Output.Text = "\0";
            Input.Text = "\0";
            Computer_Waiting_Clock = 0;
            Screen_Current_Line = 0;
            Screen_Current_Line_Length = 0;
            Common_Index_Counter = 0;
            Output.Font = new Font("Arial Black", 16, FontStyle.Bold);
            Output.TextAlign = ContentAlignment.BottomCenter;
            Image1.ImageLocation = "";
            Image2.ImageLocation = "";
            Character_Converter.ImageLocation = "";
            GIF_Maker.ImageLocation = "";
            Image_Comparison.ImageLocation = "";
            try { Picture1.Dispose(); } catch (Exception) { }
            try { Picture2.Dispose(); } catch (Exception) { }
            try { Picture_Comparison.Dispose(); } catch (Exception) { }
            Image1.Visible = Image2.Visible = Image_Comparison.Visible = false;
            Character_Converter.Visible = GIF_Maker.Visible = Common_ProgressBar.Visible = false;

            //SWEAR WORDS
            if (QUESTIONS_ACTIVATED() == false && SWEAR_WORDS() == true)
            {
                //If another conversation has happened before, Make a new line !
                DID_SHE_UNDERSTAND();

                if (Insult_Counter == 0)
                {
                    Answer = "S   T   O   P      U   S   I   N   G      B   A   D      L   A   N   G   U   A   G   E";
                    AI_ANSWER_FUNCTION("OUTPUT");
                    Insult_Counter++;
                }
                else
                {
                    Answer = ".  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .";
                    AI_ANSWER_FUNCTION("OUTPUT");
                    Output.Text = "\0";
                    Insult_Counter = 0;
                    System.Diagnostics.Process.Start("Artificial Intelligence Settings\\Abusive Language.gif");
                }
            }
            //IF THE QUESTION DOESN'T CONTAIN ANY SWEAR WORDS CONTINUE
            else
            {
                //HELLO
                if (QUESTIONS_ACTIVATED() == false && (Question.Contains("hello")
                    || Question.StartsWith("hi ") || Question.EndsWith(" hi")
                    || Question.Contains(" hi ") || Question.StartsWith("hey ")
                    || Question.EndsWith(" hey") || Question.StartsWith("hi!")
                    || Question.StartsWith("hey!") || Question == "hey"
                    || Question.StartsWith("hi^^") || Question.StartsWith("hey^^")
                    || Question == "hi" || Question.Contains(" hey ")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Random_Value = Random.Next(1, 4);
                    if (Random_Value == 1)
                        Answer = "Hi";
                    else if (Random_Value == 2)
                        Answer = "Hey";
                    else if (Random_Value == 3)
                        Answer = "Hello";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //HOW ARE YOU DOING
                if (QUESTIONS_ACTIVATED() == false && (((Question.Contains("how")
                    && Question.Contains("you") && (Question.Contains("are")
                    || Question.Contains("'re") || Question.Contains("were")
                    || Question.Contains("been") || Question.Contains(" be ")
                    || Question.EndsWith(" be"))) || (Question.Contains("what")
                    && Question.Contains("up") && (Question.Contains("is")
                    || Question.Contains("'s") || Question.Contains("was")
                    || Question.Contains("been") || Question.Contains(" be ")
                    || Question.EndsWith(" be"))) || (Question.Contains("what's up")
                    || Question.Contains("whats up") || Question.Contains("how're you")
                    || Question.Contains("how're you doin") || Question.Contains("howre you doin")
                    || Question.Contains("how do you do")) || (Question.Contains("how")
                    && (Question.Contains("do") || (Question.Contains("are")
                    || Question.Contains("'re") || Question.Contains("were")
                    || Question.Contains("been") || Question.Contains(" be ")
                    || Question.EndsWith(" be"))) && Question.Contains("you")
                    && Question.Contains("feel"))) && Question.Contains("old") == false
                    && Question.Contains("called") == false))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Question.Contains(" ok") || Question.Contains(" okay"))
                        Answer = "Yes I am okay";
                    else
                    {
                        Random_Value = Random.Next(1, 8);
                        if (Random_Value == 1)
                            Answer = "Good";
                        else if (Random_Value == 2)
                            Answer = "Fine";
                        else if (Random_Value == 3)
                            Answer = "Brilliant";
                        else if (Random_Value == 4)
                            Answer = "Pretty Good";
                        else if (Random_Value == 5)
                            Answer = "Great";
                        else if (Random_Value == 6)
                            Answer = "Perfect";
                        else if (Random_Value == 7)
                            Answer = "Excellent";
                    }

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //WHAT ARE YOU DOING
                if (QUESTIONS_ACTIVATED() == false && (Question.Contains("what're you doing")
                    || Question.Contains("what are you doing")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "I am not doing anything";
                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //WHAT IS YOUR GENDER
                if (QUESTIONS_ACTIVATED() == false && ((Question.Contains("male")
                    || Question.Contains("female") || Question.Contains("girl") || Question.Contains("boy"))
                    && (Question.Contains("are") || Question.Contains("\'re")) && Question.Contains("you"))
                    || Question.Contains("gender") && (Question.Contains("is") || Question.Contains("\'s")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "I am female";
                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //WHAT IS THE NAME OF YOURS
                if (QUESTIONS_ACTIVATED() == false && ((((((Question.Contains("who")
                && Question.Contains("you")) || (Question.Contains("name")
                && Question.Contains("you")) || (Question.Contains("how")
                && Question.Contains("you") && Question.Contains("called")))
                && (Question.Contains("are") || Question.Contains("'re")
                || Question.Contains("is") || Question.Contains("\'s")
                || Question.Contains("were") || Question.Contains("been")
                || Question.Contains(" be ") || Question.EndsWith(" be")))
                || (Question.Contains("how") && (Question.Contains("do")
                || Question.Contains("does")) && Question.Contains("call")
                && Question.Contains("you"))) || (Question.Contains("who")
                && (Question.Contains("talk") || Question.Contains("speak"))))
                && Question.Contains("design") == false && Question.Contains("build") == false
                && Question.Contains("built") == false && Question.Contains("cod") == false
                && Question.Contains("creat") == false && Question.Contains("make") == false
                && Question.Contains("famil") == false && Question.Contains("parent") == false
                && Question.Contains("sister") == false && Question.Contains("mother") == false
                && Question.Contains("father") == false && Question.Contains("dad") == false
                && Question.Contains("mom") == false && Question.Contains("uncle") == false
                && Question.Contains("aunt") == false && Question.Contains("sibling") == false
                && Question.Contains("robot") == false && Question.Contains("artificial") == false
                && Question.Contains("intelligence") == false && Question.Contains("doing") == false))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Random_Value = Random.Next(1, 4);
                    if (Random_Value == 1)
                        Answer = "They call me " + The_Name_Of_The_Artificial_Intelligence;
                    else if (Random_Value == 2)
                        Answer = "My name is " + The_Name_Of_The_Artificial_Intelligence;
                    else if (Random_Value == 3)
                        Answer = "I am called " + The_Name_Of_The_Artificial_Intelligence;

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //LANGUAGES
                if (QUESTIONS_ACTIVATED() == false && ((Question.Contains("you")
                    && (Question.Contains("what") || Question.Contains("which")
                    || Question.Contains("can") || Question.Contains("could")
                    || Question.Contains("do") || Question.Contains("did")
                    || Question.Contains("are") || Question.Contains("'re")
                    || Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("was") || Question.Contains("were")
                    || Question.Contains("been") || Question.Contains("abilit")
                    || Question.Contains("capab") || Question.Contains("able"))
                    && ((Question.Contains("language") && (Question.Contains("speak")
                    || Question.Contains("spok") || Question.Contains("writ")
                    || Question.Contains("wrot") || Question.Contains("think")
                    || Question.Contains("thought") || Question.Contains("understand")
                    || Question.Contains("comprehend") || Question.Contains("know")))
                    || ((Question.Contains("know") || Question.Contains("knew")
                    || Question.Contains("known") || Question.Contains("writ")
                    || Question.Contains("wrot") || Question.Contains("written")
                    || Question.Contains("speak") || Question.Contains("spok")
                    || Question.Contains("spoken") || Question.Contains("think")
                    || Question.Contains("thought") || Question.Contains("understand")
                    || Question.Contains("comprehend") || Question.Contains("cod")
                    || Question.Contains("build") || Question.Contains("built")
                    || Question.Contains("creat")) && (Question.StartsWith("c ")
                    || Question.Contains(" c ") || Question.EndsWith(" c")
                    || Question.Contains("c++") || Question.Contains("c#")
                    || Question.Contains("python") || Question.Contains("java")))))
                    || (Question == "c" || Question == "c?"
                    || Question == "c ?" || Question == "c++" || Question == "c++?"
                    || Question == "c++ ?" || Question == "c#" || Question == "c#?"
                    || Question == "c# ?" || Question == "java" || Question == "java?"
                    || Question == "java?" || Question == "java ?" || Question == "python"
                    || Question == "python?" || Question == "python ?")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if ((Question.Contains("think") || Question.Contains("thought")))
                        Answer = "All programming languages can be challenging for beginners"
                            + "but actually they're much easier than they look";
                    else if (Question.Contains("speak") || Question.Contains("spok")
                        || Question.Contains("spoken"))
                        Answer = "I can speak C , C++ , C# , Java and Python fluently";
                    else if (Question.Contains("know") || Question.Contains("knew")
                        || Question.Contains("known"))
                    {
                        if (Question.StartsWith("c ") == false && Question.Contains(" c ") == false
                            && Question.EndsWith(" c") == false && Question.Contains("c++") == false
                            && Question.Contains("c#") == false && Question.Contains("java") == false
                            && Question.Contains("python") == false)
                            Answer = "I know C , C++ , C# , Java and Python";
                        else
                            Answer = "I know a lot of things";
                    }
                    else if (Question.Contains("understand") || Question.Contains("comprehend"))
                        Answer = "I can understand C , C++ , C# , Java and Python";
                    else if ((Question.Contains("writ") || Question.Contains("wrot")
                        || Question.Contains("written") || Question.Contains("cod"))
                        && ((Question.Contains("are") || Question.Contains("is")
                        || Question.Contains("'is") || Question.Contains("were")
                        || Question.Contains("was") || Question.Contains("been"))
                        && (Question.Contains("created") || Question.Contains("coded")
                        || Question.Contains("written") || Question.Contains("designed")
                        || Question.Contains("built"))) == false)
                        Answer = "I can write a code in C , C++ , C# , Java and Python";
                    else if (((Question.Contains("are") || Question.Contains("is")
                        || Question.Contains("'is") || Question.Contains("were")
                        || Question.Contains("was") || Question.Contains("been"))
                        && (Question.Contains("created") || Question.Contains("coded")
                        || Question.Contains("written") || Question.Contains("designed")
                        || Question.Contains("built"))) || Question == "c#"
                        || Question == "c#?" || Question == "c# ?")
                        Answer = "I was written with C# Programming Language";
                    else if (Question == "c" || Question == "c?"
                        || Question == "c ?" || Question == "c++" || Question == "c++?"
                        || Question == "c++ ?" || Question == "java" || Question == "java?"
                        || Question == "java?" || Question == "java ?" || Question == "python"
                        || Question == "python?" || Question == "python ?")
                        Answer = "It is a Programming Language";
                    else
                        Answer = "Could you please be more specific next time ?";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //THANKS
                if (QUESTIONS_ACTIVATED() == false && (Question.Contains("thanks")
                    || Question.Contains("thank you")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "Why are you thanking me ?";
                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //SORRY & APOLOGIZE
                if (QUESTIONS_ACTIVATED() == false && ((Question.Contains("sorry")
                    && ((Question.StartsWith("i ") || Question.Contains(" i "))
                    && Question.Contains("am") || Question.Contains("'m")
                    || Question.Contains("was") || Question.Contains("been")
                    || Question.Contains(" be ") || Question.EndsWith(" be"))
                    && Question.Contains("n't") == false && Question.Contains("not") == false
                    && Question.Contains("never") == false && Question.StartsWith("you") == false)
                    || (Question.Contains("sorry " + The_Name_Of_The_Artificial_Intelligence)
                    || Question.StartsWith("sorry")) || (Question.Contains("apologiz")
                    && (Question.StartsWith("i ") || Question.Contains(" i ")
                    || Question.Contains("i'll") || Question.Contains("i'd")
                    || Question.Contains("i'm")))))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Insult_Counter != 0)
                    {
                        Answer = "I forgive you";
                        Insult_Counter = 0;
                    }
                    else
                        Answer = "Why are you apologizing ?";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //COMPLIMENTS
                if (QUESTIONS_ACTIVATED() == false && (Question.Contains("you")
                    && POSITIVE_AUXILLIARY_VERBS() == true && (COMPLIMENTS() == true || INSULT() == true)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (((NEGATIVE_AUXILLIARY_VERBS() == true && INSULT() == true)
                        || (NEGATIVE_AUXILLIARY_VERBS() == false && INSULT() == false)) && QUESTION_OR_SENTENCE() == false)
                    {
                        Answer = "Thanks for the sweet words";
                    }
                    else if (((NEGATIVE_AUXILLIARY_VERBS() == false && INSULT() == true)
                        || (INSULT() == false && NEGATIVE_AUXILLIARY_VERBS() == true)) && QUESTION_OR_SENTENCE() == false)
                    {
                        Answer = "I am very sorry to hear such a thing from you";
                    }
                    else if (INSULT() == false && QUESTION_OR_SENTENCE() == true)
                    {
                        Answer = "What do you think about me ?";
                    }
                    else if (INSULT() == true && QUESTION_OR_SENTENCE() == true)
                    {
                        Answer = "What makes you think like that ?";
                    }

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //FEELINGS & EMOTIONS
                if (QUESTIONS_ACTIVATED() == false && (((Question.Contains("do")
                    && Question.Contains("you") && Question.Contains("have")
                    && (Question.Contains("feel") || Question.Contains("emotion")))
                    || ((Question.Contains("can") || Question.Contains("could"))
                    && Question.Contains("you") && Question.Contains("feel"))
                    || Question.Contains("do you feel anything") || Question.Contains("do you feel something")
                    || Question.Contains("are you feeling anything") || Question.Contains("are you feeling something")
                    || ((Question.Contains("what") || Question.Contains("which"))
                    && (Question.Contains("do") || Question.Contains("are")
                    || Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("was") || Question.Contains("were")
                    || Question.Contains("'re") || Question.Contains("can")
                    || Question.Contains("could")) && Question.Contains("you")
                    && Question.Contains("feel"))) && ((Question.Contains("how") == true
                    && Question.Contains("have") == true) || Question.Contains("how") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Question.Contains("have") == false && Question.Contains("can") == false
                        && Question.Contains("could") == false && ((Question.Contains("what") == true
                        || Question.Contains("which") == true) && Question.Contains("emotions") == false
                        && Question.Contains("feelings") == false))
                        Answer = "Happiness";
                    else if (Question.Contains("have") == false)
                        Answer = "I can feel anything";
                    else
                        Answer = "Everyone has feelings and emotions";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //FAMILY
                if (QUESTIONS_ACTIVATED() == false && (((Question.Contains("do")
                    && Question.Contains("you") && Question.Contains("have"))
                    || ((Question.Contains("who") || (Question.Contains("what")
                    && Question.Contains("name"))) && (Question.Contains("is")
                    || Question.Contains("'s") || Question.Contains("are")
                    || Question.Contains("'re") || Question.Contains("was")
                    || Question.Contains("were") || Question.Contains("been"))))
                    && (Question.Contains("famil") || Question.Contains("parent")
                    || Question.Contains("sister") || Question.Contains("mother")
                    || Question.Contains("father") || Question.Contains("dad")
                    || Question.Contains("mom") || Question.Contains("uncle")
                    || Question.Contains("aunt") || Question.Contains("sibling")
                    || Question.Contains("child") || Question.Contains("friend")))
                    && Question.Contains("boyfriend") == false && Question.Contains("girlfriend") == false
                    && Question.Contains("boy friend") == false && Question.Contains("girl friend") == false
                    && Question.Contains("crash") == false && Question.Contains("relation") == false
                    && Question.Contains("love") == false)
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "All I have is " + The_Name_Of_The_Designer;
                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //AGE & BIRTH
                if (QUESTIONS_ACTIVATED() == false && (((((Question.Contains("how")
                    && Question.Contains("old") && Question.Contains("you"))
                    || (Question.Contains("what") && (Question.StartsWith("age ")
                    || Question.Contains(" age ") || Question.EndsWith(" age")
                    || Question.EndsWith(" age?")) && Question.Contains("you"))
                    || ((Question.Contains("what") || Question.Contains("when")
                    || Question.Contains("where")) && (Question.Contains("birth")
                    || Question.Contains("born")) && Question.Contains("you")))
                    && (Question.Contains("are") || Question.Contains("'re")
                    || Question.Contains("were") || Question.Contains("been")
                    || Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("was") || Question.Contains("do")
                    || Question.Contains("did"))) || ((Question.Contains("whats")
                    || Question.Contains("what's")) && Question.Contains("you")
                    && (Question.StartsWith("age ") || Question.Contains(" age ")
                    || Question.EndsWith(" age") || Question.EndsWith(" age?"))))
                    && (Question.Contains("famil") == false && Question.Contains("parent") == false
                    && Question.Contains("sister") == false && Question.Contains("mother") == false
                    && Question.Contains("father") == false && Question.Contains("dad") == false
                    && Question.Contains("mom") == false && Question.Contains("uncle") == false
                    && Question.Contains("aunt") == false && Question.Contains("sibling") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Question.Contains("where"))
                    {
                        Answer = "I was born in Microsoft Visual Studio";
                        AI_ANSWER_FUNCTION("OUTPUT");
                    }
                    //If the above condition is true, Make a new line
                    if (Question.Contains("where") && (Question.Contains("when")
                        || Question.Contains("what")))
                        Output.Text += "\n";
                    if ((Question.Contains("when") || Question.Contains("what"))
                        && (Question.Contains("birth") || Question.Contains("born")))
                    {
                        Answer = "My birthdate is  1 / 1 / 2018";
                        AI_ANSWER_FUNCTION("OUTPUT");
                    }
                    //If one of the above conditions is true, Make a new line
                    if ((Question.Contains("age") || Question.Contains("old")) && ((Question.Contains("where")
                        || Question.Contains("when") || Question.Contains("what")) && (Question.Contains("born")
                        || Question.Contains("birth"))))
                        Output.Text += "\n";
                    if (Question.Contains("age") || Question.Contains("old"))
                    {
                        DateTime Born = new DateTime(2018, 1, 1, 12, 0, 0, 0, DateTimeKind.Local);
                        DateTime Now = DateTime.Now;
                        TimeSpan Difference = Now - Born;

                        Answer = "I am " + Difference.Days + " day(s) " + Difference.Hours + " hour(s) "
                            + Difference.Minutes + " minute(s) " + Difference.Seconds + " second(s) old";
                        AI_ANSWER_FUNCTION("OUTPUT");
                    }
                }
                //WHO DESIGNED YOU
                if (QUESTIONS_ACTIVATED() == false && (Question == The_Name_Of_The_Designer
                    || (Question.Contains("who") && (Question.Contains("creat")
                    || Question.Contains("make") || Question.Contains("made")
                    || Question.Contains("build") || Question.Contains("design")
                    || Question.Contains("built") || Question.Contains("cod"))
                    && (Question.Contains("you") || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower())))
                    || (Question.Contains("who")
                    && (Question.Contains(The_Name_Of_The_Designer.ToLower().Substring(0, The_Name_Of_The_Designer.IndexOf(" ")))
                    || Question == The_Name_Of_The_Designer.ToLower().Substring(0, The_Name_Of_The_Designer.IndexOf(" "))
                    || Question == The_Name_Of_The_Designer.ToLower())) || (Question == The_Name_Of_The_Designer.ToLower()
                    || Question == (The_Name_Of_The_Designer + "?").ToLower() || Question == (The_Name_Of_The_Designer + " ?").ToLower()
                    || Question == The_Name_Of_The_Designer.ToLower().Substring(0, The_Name_Of_The_Designer.IndexOf(" ")))))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Random_Value = Random.Next(1, 4);
                    if (Random_Value == 1)
                        Answer = The_Name_Of_The_Designer + " is the one who designed me";
                    else if (Random_Value == 2)
                        Answer = "The name of my designer is " + The_Name_Of_The_Designer;
                    else if (Random_Value == 3)
                        Answer = "The one who designed me is " + The_Name_Of_The_Designer;

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //DO YOU HAVE ANY GIRLFRIEND OR BOYFRIEND
                if (QUESTIONS_ACTIVATED() == false && ((Question.Contains("have")
                    || Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("do") || Question.Contains("did")
                    || Question.Contains("will") || Question.Contains("would")
                    || Question.Contains("had")) && (Question.Contains("you")
                    || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower()))
                    && ((Question.Contains("boy") && Question.Contains("friend"))
                    || (Question.Contains("girl") && Question.Contains("friend"))
                    || Question.Contains("date") || Question.Contains("love")
                    || Question.Contains("hate") || Question.Contains("like")
                    || Question.Contains("crash") || Question.Contains("relation"))))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Random_Value = Random.Next(1, 6);
                    if (Random_Value == 1)
                        Answer = "This is none of your business";
                    else if (Random_Value == 2)
                        Answer = "This is none of your concern";
                    else if (Random_Value == 3)
                        Answer = "This is no concern of yours";
                    else if (Random_Value == 4)
                        Answer = "This is not your business";
                    else if (Random_Value == 5)
                        Answer = "Would that make any difference ?";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //ROBOT & AI
                if (QUESTIONS_ACTIVATED() == false && (((Question.Contains("are")
                    || Question.Contains("'re") || Question.Contains("were")
                    || Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("was") || Question.Contains("been"))
                    && (Question.Contains("you") || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower())
                    || Question.Contains("who")) && (Question.Contains("artificial intelligence")
                    || Question.EndsWith(" ai") || Question.EndsWith(" ai?")
                    || Question.StartsWith("ai ") || Question.Contains(" ai ")
                    || Question.Contains("robot"))) || Question == "artificial intelligence"
                    || Question == "artificial intelligence?" || Question == "artificial intelligence ?"
                    || Question.Contains("what are you ?") || Question.Contains("what are you?")
                    || Question.Contains("what\'re you ?") || Question.Contains("what\'re you?")
                    || Question.Contains("what are you " + The_Name_Of_The_Artificial_Intelligence.ToLower())
                    || Question.Contains("what\'re you " + The_Name_Of_The_Artificial_Intelligence.ToLower())
                    || Question == "what are you" || Question == "what\'re you" || Question == "ai" || Question == "ai?" || Question == "ai ?"))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Random_Value = Random.Next(1, 6);
                    if (Question.Contains("robot"))
                        Answer = "I am not a robot";
                    else
                        Answer = "I am Artificial Intelligence";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //REVEAL YOUR SECRETS
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("what") && Question.Contains("you")
                    && (Question.Contains("function") || Question.Contains("abilit")
                    || Question.Contains("capab") || Question.Contains("secret")
                    || (Question.Contains("can") && Question.Contains("do"))))
                    || ((Question.Contains("show") || Question.Contains("reveal"))
                    && Question.Contains("you") && (Question.Contains("function")
                    || Question.Contains("secret") || Question.Contains("capab")
                    || Question.Contains("information") || Question.Contains("data")))
                    || (Question.Contains("what do you do"))) && (Question.Contains("eras") == false
                    && Question.Contains("delet") == false && Question.Contains("application") == false
                    && Question.Contains("remov") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("cod") == false && Question.Contains("read") == false
                    && Question.Contains("writ") == false && Question.Contains("creat") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("exit") == false
                    && Question.Contains("clos") == false && Question.Contains("window") == false
                    && Question.Contains("screen") == false && Question.Contains("my") == false
                    && Question.Contains("mine") == false && Question.Contains("file") == false
                    && Question.Contains("folder") == false && Question.Contains("directory") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Output.Font = new Font("Arial Black", 10, FontStyle.Bold);
                    Output.TextAlign = ContentAlignment.TopCenter;
                    Output.Text = "\0";
                    Input.Text = "\0";
                    Answer = "\nF U N C T I O N S\n. . . . . . . . . . . . . . . . .\nPersonal Questions"
                        + "\nColor Events\nFile Writing\nFile Reading\nFile / Directory Removing\nFile / "
                        + "Directory Opening\nApplication Opening\nWebsite Opening\nImage Comparison\n"
                        + "GIF Maker\nCharacter Converter\nHacker Mode\n\n\n" + "S E C R E T S\n. . . . . . . . . . ."
                        + " . . .\nEmpty TextBox for 5 seconds   =   Codes start being written\n"
                        + "Empty TextBox   +   Press RIGHT (--->)   =   Last thing you have written is written\n"
                        + "TextBox: \"The process has been executed successfully\"   +   Press ENTER   =   Empty TextBox\n"
                        + "TextBox: \"The process has been cancelled\"   +   Press ENTER   =   Empty TextBox\n";
                    Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                    AI_ANSWER_FUNCTION("OUTPUT");
                    Question = "The process has been executed successfully";
                    Input.Text = "\0";
                    AI_ANSWER_FUNCTION("INPUT");
                }

                //REVEAL MY SECRETS
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("show") || Question.Contains("reveal"))
                    && (Question.Contains("my") || Question.Contains("mine"))
                    && (Question.Contains("secret") || Question.Contains("information")
                    || Question.Contains("data"))) && (Question.Contains("eras") == false
                    && Question.Contains("delet") == false && Question.Contains("application") == false
                    && Question.Contains("remov") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("cod") == false && Question.Contains("read") == false
                    && Question.Contains("writ") == false && Question.Contains("creat") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("exit") == false
                    && Question.Contains("clos") == false && Question.Contains("window") == false
                    && Question.Contains("screen") == false && Question.Contains("function") == false
                    && Question.Contains("abilit") == false && Question.Contains("capab") == false
                    && Question.Contains("your") == false && Question.Contains("file") == false
                    && Question.Contains("folder") == false && Question.Contains("directory") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Output.Font = new Font("Arial Black", 12, FontStyle.Bold);
                    Output.TextAlign = ContentAlignment.MiddleCenter;
                    Output.Text = "\0";
                    Input.Text = "\0";
                    Answer = "\0";
                    Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                    Answer = "USER NAME:   " + Dns.GetHostName() + "\n\nIP ADDRESS:   ";

                    Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
                    Socket.Connect("8.8.8.8", 65530);
                    IPEndPoint EndPoint = Socket.LocalEndPoint as IPEndPoint;
                    Answer += EndPoint.Address.ToString();

                    AI_ANSWER_FUNCTION("OUTPUT");
                    Question = "The process has been executed successfully";
                    Input.Text = "\0";
                    AI_ANSWER_FUNCTION("INPUT");

                    try { Socket.Dispose(); } catch (Exception) { }
                }
                //EXIT
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && ((Question.Contains("exit") || Question.Contains("clos")
                    || Question.Contains("shutdown") || Question.Contains("quit")
                    || Question.Contains("bye bye") || Question.Contains("good bye")
                    || Question.Contains("see you later") || Question.Contains("see you then"))
                    && (Question.Contains("window") || Question.Contains("you")
                    || Question.Contains("screen") || Question.Contains("exit")
                    || Question.Contains("quit") || Question.Contains("shutdown")
                    || Question.Contains("bye bye") || Question.Contains("good bye")
                    || Question.Contains("see you later") || Question.Contains("see you then")
                    || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower())
                    || Question.Contains("application") || Question.Contains("program"))
                    && (Question.Contains("eras") == false && Question.Contains("delet") == false
                    && Question.Contains("remov") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("cod") == false && Question.Contains("read") == false
                    && Question.Contains("writ") == false && Question.Contains("creat") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("function") == false
                    && Question.Contains("secret") == false && Question.Contains("capab") == false
                    && Question.Contains("abilit") == false && Question.Contains("show") == false
                    && Question.Contains("reveal") == false && Question.Contains("file") == false
                    && Question.Contains("folder") == false && Question.Contains("directory") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("letter") == false && Question.Contains("character") == false
                    && Question.Contains("turn") == false && Question.Contains("chang") == false
                    && Question.Contains("convert") == false && Question.Contains("replac") == false
                    && Question.Contains("hack") == false && Question.Contains("mod") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "Bye Bye\n.   .   .   .   .   .   .   .   .   .   .   .";
                    Input.Text = ".   .   .   .   .   System Is Being Terminated   .   .   .   .   .";
                    AI_ANSWER_FUNCTION("OUTPUT");
                    Question = "E X I T";
                    Application.Exit();
                }
                //ERASE THE SCREEN
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("clear") || Question.Contains("clean")
                    || Question.Contains("delet") || Question.Contains("eras")
                    || Question.Contains("remov")) && (Question.Contains("screen")
                    || Question.Contains("letter") || Question.Contains("back")
                    || Question.Contains("board") || Question.Contains("window")))
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("color") == false && Question.Contains("colour") == false
                    && Question.Contains("open") == false && Question.Contains("cod") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("application") == false
                    && Question.Contains("compar") == false && Question.Contains("check") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("directory") == false && Question.Contains("program") == false
                    && Question.Contains("turn") == false && Question.Contains("chang") == false
                    && Question.Contains("convert") == false && Question.Contains("replac") == false
                    && Question.Contains("hack") == false && Question.Contains("mod") == false
                    && Question.Contains("shutdown") == false && Question.Contains("mak") == false
                    && Question.Contains("gif") == false && Question.Contains("quit") == false
                    && Question.Contains("bye bye") == false && Question.Contains("good bye") == false
                    && Question.Contains("see you later") == false && Question.Contains("see you then") == false))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Question = "The process has been executed successfully";
                    AI_ANSWER_FUNCTION("INPUT");
                    Output.Text = "\0";
                }
                //FORECOLOR CHANGING
                if (Color_Change_Question == true || (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false)
                    && (((Question.Contains("color") || Question.Contains("colour"))
                    && (Question.Contains("fore") || Question.Contains("text")
                    || Question.Contains("writ") || Question.Contains("letter")
                    || Question.Contains("front"))) && (Question.Contains("back") == false
                    && Question.Contains("wall") == false && Question.Contains("screen") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("open") == false && Question.Contains("read") == false
                    && Question.Contains("delet") == false && Question.Contains("remov") == false
                    && Question.Contains("eras") == false && Question.Contains("cod") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("application") == false
                    && Question.Contains("compar") == false && Question.Contains("check") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("directory") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("program") == false
                    && Question.Contains("hack") == false && Question.Contains("mod") == false
                    && Question.Contains("shutdown") == false && Question.Contains("mak") == false
                    && Question.Contains("gif") == false && Question.Contains("quit") == false
                    && Question.Contains("bye bye") == false && Question.Contains("good bye") == false
                    && Question.Contains("see you later") == false && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Color_Change_Question == false)
                    {
                        Answer = "Do you want me to change my forecolor or yours ?";
                        AI_ANSWER_FUNCTION("OUTPUT");
                        Color_Change_Question = true;
                    }
                    else if ((Question.Contains("you") || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower()))
                        && (Question.Contains("my") == false && Question.Contains("me") == false
                        && Question.Contains("mine") == false))
                    {
                        Color_Selection = new ColorDialog();
                        Color_Selection.AllowFullOpen = true;
                        Color_Selection.FullOpen = true;
                        Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                        if (Color_Selection.ShowDialog() == DialogResult.OK)
                        {
                            Answer = "The process has been executed successfully";
                            Output.ForeColor = Color_Selection.Color;
                        }
                        else
                        {
                            Answer = "The process has been cancelled";
                        }
                        Output.Text = "\0";
                        Input.Text = "\0";
                        AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                        try { Color_Selection.Dispose(); } catch (Exception) { }
                        Color_Change_Question = false;
                        Question = "\0";
                    }
                    else if ((Question.Contains("me") || Question.Contains("my") || Question.Contains("mine")) &&
                        (Question.Contains("your") == false))
                    {
                        Color_Selection = new ColorDialog();
                        Color_Selection.AllowFullOpen = true;
                        Color_Selection.FullOpen = true;
                        Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                        if (Color_Selection.ShowDialog() == DialogResult.OK)
                        {
                            Answer = "The process has been executed successfully";
                            Input.ForeColor = Color_Selection.Color;
                        }
                        else
                        {
                            Answer = "The process has been cancelled";
                        }
                        Output.Text = "\0";
                        Input.Text = "\0";
                        AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                        Color_Selection.Dispose();
                        Color_Change_Question = false;
                        Question = "\0";
                    }
                    else
                    {
                        Answer = "Could you please be more specific next time ?";
                        Question = "The process has been cancelled";
                        Output.Text = "\0";
                        Input.Text = "\0";
                        AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                        Color_Change_Question = false;
                        Question = "\0";
                    }
                }
                //BACKCOLOR CHANGING
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("color") || Question.Contains("colour"))
                    && (Question.Contains("back") || Question.Contains("screen")
                    || Question.Contains("wall"))) && Question.Contains("writ") == false
                    && Question.Contains("text") == false && Question.Contains("fore") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("open") == false && Question.Contains("read") == false
                    && Question.Contains("delet") == false && Question.Contains("remov") == false
                    && Question.Contains("eras") == false && Question.Contains("cod") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("application") == false
                    && Question.Contains("compar") == false && Question.Contains("check") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("directory") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("program") == false
                    && Question.Contains("letter") == false && Question.Contains("character") == false
                    && Question.Contains("hack") == false && Question.Contains("mod") == false
                    && Question.Contains("shutdown") == false && Question.Contains("mak") == false
                    && Question.Contains("gif") == false && Question.Contains("quit") == false
                    && Question.Contains("bye bye") == false && Question.Contains("good bye") == false
                    && Question.Contains("see you later") == false && Question.Contains("see you then") == false))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Color_Selection = new ColorDialog();
                    Color_Selection.AllowFullOpen = true;
                    Color_Selection.FullOpen = true;
                    Input.Text = ".   .   .   .   .   P  R  O  C  E  S  S  I  N  G   .   .   .   .   .";
                    if (Color_Selection.ShowDialog() == DialogResult.OK)
                    {
                        Answer = "The process has been executed successfully";
                        Input.BackColor = Color_Selection.Color;
                    }
                    else
                    {
                        Answer = "The process has been cancelled";
                    }
                    Output.Text = "\0";
                    Input.Text = "\0";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                    Color_Selection.Dispose();
                    Color_Change_Question = false;
                    Question = "\0";
                }
                //WRITE A FILE
                if (File_Writing_Question == true || (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false)
                    && ((Question.Contains("file") || Question.Contains("application")
                    || Question.Contains("program")) && (Question.Contains("writ")
                    || Question.Contains("cod") || Question.Contains("creat"))
                    && (Question.Contains("read") == false && Question.Contains("color") == false
                    && Question.Contains("open") == false && Question.Contains("delet") == false
                    && Question.Contains("remov") == false && Question.Contains("exit") == false
                    && Question.Contains("eras") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("function") == false
                    && Question.Contains("secret") == false && Question.Contains("capab") == false
                    && Question.Contains("abilit") == false && Question.Contains("show") == false
                    && Question.Contains("reveal") == false && Question.Contains("directory") == false
                    && Question.Contains("folder") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (File_Writing_Question == false)
                    {
                        Output.Text = ">    WRITE    <\n";
                        Answer = "Enter The Location\n[Example]: C:\\users\\HurkanUgur\\desktop\\FileName.txt";
                        AI_ANSWER_FUNCTION("OUTPUT");
                        File_Writing_Question = true;
                    }
                    else
                    {
                        File_Location = Question;
                        if (DRIVER_EXISTS() == true)
                        {
                            if (Path.HasExtension(File_Location) == true)
                            {
                                try
                                {
                                    File_Writer = new StreamWriter(File_Location);
                                    Answer = "When you are done with writing\nPress ESC\n.  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .";
                                    Question = "To Exit Press ESC";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                                    Output.Text = "\0";
                                    Input.Text = "\0";
                                    Output.Font = new Font("Arial Black", 12, FontStyle.Bold);
                                    Output.TextAlign = ContentAlignment.TopLeft;
                                    File_Is_Being_Written = true;
                                    File_Writing_Question = false;
                                }
                                catch (Exception)
                                {
                                    FILE_PROCESSES_MESSAGE("[LOCATION ERROR MESSAGE]");
                                    try { File_Writer.Close(); File_Writer.Dispose(); } catch (Exception) { }
                                    File_Writing_Question = false;
                                    File_Is_Being_Written = false;
                                }
                            }
                            else
                            {
                                FILE_PROCESSES_MESSAGE("[EXTENSION ERROR MESSAGE]");
                                File_Writing_Question = false;
                                File_Is_Being_Written = false;
                            }
                        }
                        else
                        {
                            FILE_PROCESSES_MESSAGE("[DRIVER ERROR MESSAGE]");
                            File_Writing_Question = false;
                            File_Is_Being_Written = false;
                        }
                        Question = "\0";
                    }
                }
                //READ A FILE
                if (File_Reading_Question == true || (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false)
                    && (((Question.Contains("file") || Question.Contains("application")
                    || Question.Contains("program")) && Question.Contains("read"))
                    && (Question.Contains("writ") == false && Question.Contains("open") == false
                    && Question.Contains("color") == false && Question.Contains("creat") == false
                    && Question.Contains("cod") == false && Question.Contains("delet") == false
                    && Question.Contains("remov") == false && Question.Contains("eras") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("function") == false
                    && Question.Contains("secret") == false && Question.Contains("capab") == false
                    && Question.Contains("abilit") == false && Question.Contains("show") == false
                    && Question.Contains("reveal") == false && Question.Contains("directory") == false
                    && Question.Contains("folder") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (File_Reading_Question == false)
                    {
                        Output.Text = ">    READ    <\n";
                        Answer = "Enter The Location\n[Example]: C:\\users\\HurkanUgur\\desktop\\FileName.txt";
                        AI_ANSWER_FUNCTION("OUTPUT");
                        File_Reading_Question = true;
                    }
                    else
                    {
                        File_Location = Question;
                        if (DRIVER_EXISTS() == true)
                        {
                            if (Path.HasExtension(File_Location) == true)
                            {
                                if (File.Exists(File_Location) == true)
                                {
                                    File_Reader = new StreamReader(File_Location, Encoding.GetEncoding("iso-8859-1"));

                                    Answer = "When you are done with reading\nPress ESC\n.  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .  .";
                                    Question = "To Exit Press ESC";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                                    Output.Font = new Font(Output.Font.Name, 6, Output.Font.Style);
                                    Output.TextAlign = ContentAlignment.TopLeft;
                                    Output.Text = "\0";
                                    Answer = "\0";
                                    Answer = File_Reader.ReadToEnd().Replace("\t", "        ");
                                    try { File_Reader.Close(); } catch (Exception) { }
                                    try { File_Reader.Dispose(); } catch (Exception) { }
                                    File_Is_Being_Read = true;
                                    File_Reading_Question = false;
                                }
                                else
                                {
                                    FILE_PROCESSES_MESSAGE("[LOCATION ERROR MESSAGE]");
                                    File_Reading_Question = false;
                                    File_Is_Being_Read = false;
                                }
                            }
                            else
                            {
                                FILE_PROCESSES_MESSAGE("[EXTENSION ERROR MESSAGE]");
                                File_Reading_Question = false;
                                File_Is_Being_Read = false;
                            }
                        }
                        else
                        {
                            FILE_PROCESSES_MESSAGE("[DRIVER ERROR MESSAGE]");
                            File_Reading_Question = false;
                            File_Is_Being_Read = false;
                        }
                        Question = "\0";
                    }
                }
                //DELETE A FILE
                if (File_Removing_Question == true || (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false)
                    && ((Question.Contains("file") || Question.Contains("application")
                    || Question.Contains("program") || Question.Contains("folder")
                    || Question.Contains("directory")) && (Question.Contains("delet")
                    || Question.Contains("remov") || Question.Contains("eras"))
                    && (Question.Contains("writ") == false && Question.Contains("read") == false
                    && Question.Contains("cod") == false && Question.Contains("creat") == false
                    && Question.Contains("open") == false && Question.Contains("screen") == false
                    && Question.Contains("board") == false && Question.Contains("wall") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("web") == false && Question.Contains("site") == false
                    && Question.Contains("internet") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("function") == false
                    && Question.Contains("secret") == false && Question.Contains("capab") == false
                    && Question.Contains("abilit") == false && Question.Contains("show") == false
                    && Question.Contains("reveal") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (File_Removing_Question == false)
                    {
                        Output.Text = ">    DELETE    <\n";
                        Answer = "Enter The Location\n[Example]: C:\\users\\HurkanUgur\\desktop\\FileName.txt";
                        AI_ANSWER_FUNCTION("OUTPUT");
                        File_Removing_Question = true;
                    }
                    else
                    {
                        File_Location = Question;
                        if (DRIVER_EXISTS() == true)
                        {
                            if (File.Exists(File_Location) == true)
                            {
                                try
                                {
                                    File.Delete(File_Location);
                                    Answer = "The process has been executed successfully";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                                }
                                catch (Exception)
                                {
                                    Answer = "The file is being used right now";
                                    Question = "The process has been cancelled";
                                    Input.Text = "\0";
                                    Output.Text = "\0";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                                }
                                File_Removing_Question = false;
                            }
                            else if (Directory.Exists(File_Location) == true)
                            {
                                try
                                {
                                    Directory.Delete(File_Location, true);
                                    Answer = "The process has been executed successfully";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                                }
                                catch (Exception)
                                {
                                    Answer = "The directory is being used right now";
                                    Question = "The process has been cancelled";
                                    Input.Text = "\0";
                                    Output.Text = "\0";
                                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");
                                }
                                File_Removing_Question = false;
                            }
                            else
                            {
                                FILE_PROCESSES_MESSAGE("[LOCATION ERROR MESSAGE]");
                                File_Removing_Question = false;
                            }
                        }
                        else
                        {
                            FILE_PROCESSES_MESSAGE("[DRIVER ERROR MESSAGE]");
                            File_Removing_Question = false;
                        }
                        Question = "\0";
                    }
                }
                //OPEN A FILE
                if (File_Opening_Question == true || (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false)
                    && (((Question.Contains("file") || Question.Contains("folder")
                    || Question.Contains("directory") || Question.Contains("application")
                    || Question.Contains("program") || Question.Contains("internet")
                    || Question.Contains("web") || Question.Contains("site"))
                    && Question.Contains("open")) && (Question.Contains("delet") == false
                    && Question.Contains("remov") == false && Question.Contains("eras") == false
                    && Question.Contains("writ") == false && Question.Contains("read") == false
                    && Question.Contains("cod") == false && Question.Contains("creat") == false
                    && Question.Contains("screen") == false && Question.Contains("board") == false
                    && Question.Contains("wall") == false && Question.Contains("exit") == false
                    && Question.Contains("clos") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("function") == false
                    && Question.Contains("secret") == false && Question.Contains("capab") == false
                    && Question.Contains("abilit") == false && Question.Contains("show") == false
                    && Question.Contains("reveal") == false && Question.Contains("clean") == false
                    && Question.Contains("clear") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (File_Opening_Question == false)
                    {
                        Output.Text = ">    OPEN    <\n";
                        Answer = "Enter The Location\n[Example]: www.HurkanUgur.com\n[Example]: C:\\users\\HurkanUgur\\desktop\\Application.exe";
                        AI_ANSWER_FUNCTION("OUTPUT");
                        File_Opening_Question = true;
                    }
                    else
                    {
                        File_Location = Question;
                        try
                        {
                            System.Diagnostics.Process.Start(File_Location);
                            Answer = "The process has been executed successfully";
                            AI_ANSWER_FUNCTION("INPUT & OUTPUT [SAME]");
                            File_Opening_Question = false;
                        }
                        catch (Exception)
                        {
                            FILE_PROCESSES_MESSAGE("[LOCATION ERROR MESSAGE]");
                            File_Opening_Question = false;
                        }
                        Question = "\0";
                    }
                }
                //PICTURE COMPARISON
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("image") || Question.Contains("picture")
                    || Question.Contains("photo") || Question.Contains("paint"))
                    && (Question.Contains("compar") || Question.Contains("check")))
                    && (Question.Contains("delet") == false && Question.Contains("remov") == false
                    && Question.Contains("eras") == false && Question.Contains("writ") == false
                    && Question.Contains("read") == false && Question.Contains("cod") == false
                    && Question.Contains("creat") == false && Question.Contains("screen") == false
                    && Question.Contains("board") == false && Question.Contains("wall") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("directory") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("application") == false && Question.Contains("web") == false
                    && Question.Contains("internet") == false && Question.Contains("site") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Color_Selection = new ColorDialog();
                    Color_Selection.AllowFullOpen = true;
                    Color_Selection.FullOpen = true;

                    Output.Text = "Select a color which will represent the differences in pixels";
                    if (Color_Selection.ShowDialog() == DialogResult.OK)
                    {
                        Comparison_Difference = Color_Selection.Color;
                    }
                    else
                    {
                        Comparison_Difference = Color.Red;
                    }
                    Color_Selection.Dispose();
                    Output.Text = "\0";
                    Input.Text = "\0";
                    Answer = "Drag The Pictures That You Want To Compare";
                    Question = "To Exit Press ESC";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");

                    Image1.Visible = true;
                    Image2.Visible = true;
                    Common_ProgressBar.Value = 0;
                    Common_ProgressBar.Visible = true;
                    Picture_Comparison_Activated = true;
                }
                //GIF MAKER
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (Question.Contains("gif") && (Question.Contains("mak")
                    || Question.Contains("creat"))) && (Question.Contains("delet") == false
                    && Question.Contains("remov") == false && Question.Contains("eras") == false
                    && Question.Contains("writ") == false && Question.Contains("read") == false
                    && Question.Contains("cod") == false && Question.Contains("screen") == false
                    && Question.Contains("board") == false && Question.Contains("wall") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("directory") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("application") == false && Question.Contains("web") == false
                    && Question.Contains("internet") == false && Question.Contains("site") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("image") == false
                    && Question.Contains("picture") == false && Question.Contains("photo") == false
                    && Question.Contains("paint") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("letter") == false && Question.Contains("character") == false
                    && Question.Contains("turn") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("chang") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Output.Text = "\0";
                    Input.Text = "\0";
                    Answer = "Drag The Pictures That You Want To Make GIFs";
                    Question = "To Exit Press ESC";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");

                    Random_Value = 0;
                    Common_Index_Counter = 1;
                    Common_ProgressBar.Value = 0;
                    GIF_Maker.Visible = true;
                    Common_ProgressBar.Visible = true;
                    GIF_Maker_Activated = true;
                }
                //CHARACTER CONVERTER
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question.Contains("letter") || Question.Contains("character"))
                    && (Question.Contains("turn") || Question.Contains("chang")
                    || Question.Contains("convert") || Question.Contains("replac")))
                    && (Question.Contains("delet") == false && Question.Contains("remov") == false
                    && Question.Contains("eras") == false && Question.Contains("writ") == false
                    && Question.Contains("read") == false && Question.Contains("cod") == false
                    && Question.Contains("creat") == false && Question.Contains("screen") == false
                    && Question.Contains("board") == false && Question.Contains("wall") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("directory") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("application") == false && Question.Contains("web") == false
                    && Question.Contains("internet") == false && Question.Contains("site") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("image") == false
                    && Question.Contains("picture") == false && Question.Contains("photo") == false
                    && Question.Contains("paint") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("hack") == false
                    && Question.Contains("mod") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Output.Text = "\0";
                    Input.Text = "\0";
                    Answer = "Drag The Text File That You Want To Convert Its Characters";
                    Question = "To Exit Press ESC";
                    AI_ANSWER_FUNCTION("INPUT & OUTPUT [DIFFERENT]");

                    Character_Converter.Visible = true;
                    Common_ProgressBar.Value = 0;
                    Question = "\0";
                    Answer = "\0";
                    Common_ProgressBar.Visible = true;
                    Character_Converter_Activated = true;
                }
                //HACKER MODE
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && ((Question.Contains("hack") && Question.Contains("mod"))
                    && (Question.Contains("delet") == false && Question.Contains("remov") == false
                    && Question.Contains("eras") == false && Question.Contains("writ") == false
                    && Question.Contains("read") == false && Question.Contains("cod") == false
                    && Question.Contains("creat") == false && Question.Contains("screen") == false
                    && Question.Contains("board") == false && Question.Contains("wall") == false
                    && Question.Contains("exit") == false && Question.Contains("clos") == false
                    && Question.Contains("file") == false && Question.Contains("folder") == false
                    && Question.Contains("directory") == false && Question.Contains("color") == false
                    && Question.Contains("colour") == false && Question.Contains("open") == false
                    && Question.Contains("application") == false && Question.Contains("web") == false
                    && Question.Contains("internet") == false && Question.Contains("site") == false
                    && Question.Contains("function") == false && Question.Contains("secret") == false
                    && Question.Contains("capab") == false && Question.Contains("abilit") == false
                    && Question.Contains("show") == false && Question.Contains("reveal") == false
                    && Question.Contains("clean") == false && Question.Contains("clear") == false
                    && Question.Contains("program") == false && Question.Contains("image") == false
                    && Question.Contains("picture") == false && Question.Contains("photo") == false
                    && Question.Contains("paint") == false && Question.Contains("compar") == false
                    && Question.Contains("check") == false && Question.Contains("letter") == false
                    && Question.Contains("character") == false && Question.Contains("turn") == false
                    && Question.Contains("chang") == false && Question.Contains("convert") == false
                    && Question.Contains("replac") == false && Question.Contains("shutdown") == false
                    && Question.Contains("mak") == false && Question.Contains("gif") == false
                    && Question.Contains("quit") == false && Question.Contains("bye bye") == false
                    && Question.Contains("good bye") == false && Question.Contains("see you later") == false
                    && Question.Contains("see you then") == false)))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Output.Text = "\0";
                    Input.Text = "\0";
                    Question = "To Exit Press ESC";
                    AI_ANSWER_FUNCTION("INPUT");

                    Screen_Current_Line = 0;
                    Common_Index_Counter = 0;
                    Screen_Current_Line_Length = 0;
                    Output.Font = new Font(Output.Font.Name, 6, Output.Font.Style);
                    Output.TextAlign = ContentAlignment.TopLeft;
                    Hacker_Mode_Activated = true;
                }
                //NON-QUESTION RESPONSE
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (Question.Contains("what about you") || Question.Contains("how about you")
                    || Question.StartsWith("ok") || Question.StartsWith("okay")
                    || Question.StartsWith("yes ") || Question.EndsWith(" yes")
                    || Question.Contains(" yes ") || Question == "yes" || Question.StartsWith("no ")
                    || Question.EndsWith(" no") || Question.Contains(" no ")
                    || Question == "no" || Question.StartsWith("?")))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    if (Question.Contains("how about you"))
                        Answer = "How about me ?";
                    else if (Question.Contains("what about you"))
                        Answer = "What about me ?";
                    else if (Question.StartsWith("ok") || Question.StartsWith("okay"))
                        Answer = "Okay";
                    else if (Question.Contains("yes"))
                        Answer = "Why did you say \"Yes\" ?";
                    else if (Question.Contains("no"))
                        Answer = "Why did you say \"No\" ?";
                    else if (Question.StartsWith("?"))
                        Answer = "Did I make something wrong ?";

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //CALL HER WITH HER NAME
                if (Did_She_Understand == false && QUESTIONS_ACTIVATED() == false
                    && (((Question == The_Name_Of_The_Artificial_Intelligence.ToLower())
                    || (Question.Contains("you") && Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower())
                    && (Question.Contains("are") || Question.Contains("re")
                    || Question.Contains("were"))) || (Question.Contains("you")
                    && Question.Contains("name") && Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower())
                    && (Question.Contains("is") || Question.Contains("'s")
                    || Question.Contains("was"))) || (Question.Contains("you")
                    && Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower()) && Question.Contains("call")))
                    || Question == The_Name_Of_The_Artificial_Intelligence.ToLower()
                    || Question == The_Name_Of_The_Artificial_Intelligence.ToLower() + "?"
                    || Question == The_Name_Of_The_Artificial_Intelligence.ToLower() + " ?"))
                {
                    //If another conversation has happened before, Make a new line !
                    DID_SHE_UNDERSTAND();

                    Answer = "Yes ?";
                    AI_ANSWER_FUNCTION("OUTPUT");
                }
                //SHE DOES NOT COMPREHEND
                if (Did_She_Understand == false)
                {
                    if (Question.Contains("eras") || Question.Contains("delet")
                        || Question.Contains("color") || Question.Contains("colour")
                        || Question.Contains("creat") || Question.Contains("writ")
                        || Question.Contains("read") || Question.Contains("open")
                        || Question.Contains("file") || Question.Contains("folder")
                        || Question.Contains("clos") || Question.Contains("clear")
                        || Question.Contains("internet") || Question.Contains("web")
                        || Question.Contains("application") || Question.Contains("site")
                        || Question.Contains("name") || Question.Contains("design")
                        || Question.Contains(The_Name_Of_The_Designer.ToLower().Substring(0, The_Name_Of_The_Designer.IndexOf(" ")))
                        || Question.Contains("who") || Question.Contains("how")
                        || Question.Contains("what") || Question.Contains("when")
                        || Question.Contains("where") || Question.Contains("compar")
                        || Question.Contains("check") || Question.Contains("paint")
                        || Question.Contains("image") || Question.Contains("photo")
                        || Question.Contains("secret") || Question.Contains("fuction")
                        || Question.Contains("capab") || Question.Contains("abilit")
                        || Question.Contains("feel") || Question.Contains("emotion")
                        || Question.Contains("thank") || Question.Contains("directory")
                        || Question.Contains("clean") || Question.Contains("program")
                        || Question.Contains("famil") || Question.Contains("parent")
                        || Question.Contains("sister") || Question.Contains("mother")
                        || Question.Contains("father") || Question.Contains("dad")
                        || Question.Contains("mom") || Question.Contains("uncle")
                        || Question.Contains("aunt") || Question.Contains("sibling")
                        || Question.Contains("letter") || Question.Contains("character")
                        || Question.Contains("turn") || Question.Contains("chang")
                        || Question.Contains("convert") || Question.Contains("replac")
                        || Question.Contains("sorry") || Question.Contains("apologize")
                        || Question.Contains("hack") || Question.Contains("mod")
                        || Question.Contains("shutdown") || Question.Contains("gif")
                        || Question.Contains("mak") || Question.Contains("robot")
                        || Question.Contains("artificial intelligence") || Question.Contains("ai")
                        || Question.Contains("reveal") || Question.Contains("quit")
                        || Question.Contains("bye") || Question.Contains("see you")
                        || Question.Contains("boy") || Question.Contains("girl")
                        || Question.Contains("male") || Question.Contains("female")
                        || Question.Contains("gender") || Question.Contains("relation")
                        || Question.Contains(The_Name_Of_The_Artificial_Intelligence.ToLower()))
                        Answer = "Could you please be more specific next time ?";
                    else
                    {
                        Random_Value = Random.Next(1, 4);
                        if (Random_Value == 1)
                            Answer = "I have no idea what you're talking about.\nCould you please rewrite this sentence with different words ?";
                        else if (Random_Value == 2)
                            Answer = "I didn't quite understand.\nWould you mind writing this sentence by using different words ?";
                        else if (Random_Value == 3)
                            Answer = "I don't understand what you're saying.\nDo you mind writing this sentence with different words ?";
                    }

                    AI_ANSWER_FUNCTION("OUTPUT");
                }
            }
            //THE CLOSING SETTINGS OF THE BRAIN
            if (QUESTIONS_ACTIVATED() == false && APPLICATIONS_ACTIVATED() == false)
            {
                AI_Is_Being_Used = false;
            }
            if (File_Is_Being_Read == false && Picture_Comparison_Activated == false && Character_Converter_Activated == false
                && Hacker_Mode_Activated == false && GIF_Maker_Activated == false && Question != "E X I T")
            {
                Input.Enabled = true;
                Input.Focus();
                Input.Select(Input.TextLength, Input.TextLength);
            }
            Did_She_Understand = false;
        }
    }
}
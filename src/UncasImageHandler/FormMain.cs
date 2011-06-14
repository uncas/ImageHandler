using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Uncas.Drawing.ImageResizing;

namespace UncasImageHandler
{
    public partial class FormMain : Form
    {
        #region Constructor

        public FormMain()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMain_Load);
        }

        #endregion

        #region Private fields and properties

        private IResizeImages ri = new ResizeImages();
        private const string ResizeText = "Gem";
        private const string CancelText = "Afbryd";

        #endregion

        #region Event and method cycle

        void FormMain_Load
            (object sender
            , EventArgs e)
        {
            btnSaveResizedImages.Text = ResizeText;

            #region Subscribes to event handlers
            rbChooseFiles.CheckedChanged
                += new EventHandler(rbSourceMode_CheckedChanged);
            rbChooseFolder.CheckedChanged
                += new EventHandler(rbSourceMode_CheckedChanged);
            btnChooseInputFolder.Click
                += new EventHandler(btnChooseInputFolder_Click);
            btnChooseInputFiles.Click
                += new EventHandler(btnChooseInputFiles_Click);
            btnChooseOutputFolder.Click
                += new EventHandler(btnChooseOutputFolder_Click);
            btnClear.Click
                += new EventHandler(btnClear_Click);
            btnSaveResizedImages.Click
                += new EventHandler(btnSaveResizedImages_Click);
            #endregion

            #region Sets folders on file and folder dialogs
            Environment.SpecialFolder myPictures
                = Environment.SpecialFolder.MyPictures;
            string myPicturesPath
                = Environment.GetFolderPath(myPictures);
            openFileDialog1.InitialDirectory = myPicturesPath;
            folderBrowserDialog1.RootFolder
                = Environment.SpecialFolder.Desktop;
            folderBrowserDialog1.SelectedPath = myPicturesPath;
            #endregion

            #region Input folder path
            string inputFolderPath
                = Properties.Settings.Default.InputFolderPath;
            if (inputFolderPath != null
                && inputFolderPath.Trim() != string.Empty)
            {
                lblInputFolder.Text = inputFolderPath;
            }
            else
            {
                lblInputFolder.Text = myPicturesPath;
            }
            #endregion

            #region Output folder path
            string outputFolderPath
                = Properties.Settings.Default.OutputFolderPath;
            if (outputFolderPath != null
                && outputFolderPath.Trim() != string.Empty)
            {
                lblOutputFolder.Text = outputFolderPath;
            }
            else
            {
                lblOutputFolder.Text = myPicturesPath;
            }
            #endregion

            ChangeSourceMode();

            SetMaxImageSize();

            ri.ResizeProgressChanged
                += new EventHandler<ResizeProgressEventArgs>
                    (ri_ProgressChanged);

            ri.ResizeCompleted
                += new EventHandler<ResizeCompletedEventArgs>
                    (ri_ResizeCompleted);

            ri.ResizeFailed
                += new EventHandler<ResizeFailedEventArgs>
                    (ri_ResizeFailed);
        }

        void ri_ResizeFailed
            (object sender
            , ResizeFailedEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
        }

        void ri_ResizeCompleted
            (object sender
            , ResizeCompletedEventArgs e)
        {
            if (e.Canceled)
            {
                MessageBox.Show("Arbejdet blev afbrudt");
            }
            else
            {
                lblNumberCompleted.Text = e.ProcessedImages.ToString();
                progressBar1.Value = 100;
                MessageBox.Show("Billederne blev gemt");
            }
            gbSourceMode.Enabled = true;
            gbOutputFolder.Enabled = true;
            gbMaxSize.Enabled = true;
            btnSaveResizedImages.Text = ResizeText;
            Process.Start(lblOutputFolder.Text);
        }

        void ri_ProgressChanged(object sender, ResizeProgressEventArgs e)
        {
            lblNumberCompleted.Text = e.ProcessedImages.ToString();
            progressBar1.Value = e.ProcessedImages.Percentage;
        }

        #region Handling user choices

        private void ChangeSourceMode()
        {
            btnChooseInputFiles.Enabled = rbChooseFiles.Checked;
            btnClear.Enabled = rbChooseFiles.Checked;
            listBoxFiles.Enabled = rbChooseFiles.Checked;

            btnChooseInputFolder.Enabled = rbChooseFolder.Checked;
            chbxIncludeSubFolders.Enabled = rbChooseFolder.Checked;
        }

        void rbSourceMode_CheckedChanged(object sender, EventArgs e)
        {
            ChangeSourceMode();
        }

        void btnChooseInputFolder_Click(object sender, EventArgs e)
        {
            lblInputFolder.Text
                = GetFolder(lblInputFolder.Text);
            Properties.Settings.Default.InputFolderPath
                = lblInputFolder.Text;
            Properties.Settings.Default.Save();
        }

        void btnChooseInputFiles_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            switch (dr)
            {
                default:
                case DialogResult.Abort:
                case DialogResult.Cancel:
                case DialogResult.Ignore:
                case DialogResult.No:
                case DialogResult.None:
                case DialogResult.Retry:
                    break;
                case DialogResult.OK:
                case DialogResult.Yes:
                    foreach (string fileName
                        in openFileDialog1.FileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        listBoxFiles.Items.Add(fileName);
                        folderBrowserDialog1.SelectedPath
                            = fi.DirectoryName;
                        lblOutputFolder.Text = fi.DirectoryName;
                    }
                    break;
            }
        }

        void btnChooseOutputFolder_Click(object sender, EventArgs e)
        {
            lblOutputFolder.Text
                = GetFolder(lblOutputFolder.Text);
            Properties.Settings.Default.OutputFolderPath
                = lblOutputFolder.Text;
            Properties.Settings.Default.Save();
        }

        private string GetFolder(string initialFolder)
        {
            string sOut = initialFolder;
            folderBrowserDialog1.SelectedPath = initialFolder;
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            switch (dr)
            {
                default:
                    break;
                case DialogResult.OK:
                case DialogResult.Yes:
                    sOut = folderBrowserDialog1.SelectedPath;
                    break;
            }
            return sOut;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
        }

        private void btnSaveResizedImages_Click
            (object sender
            , EventArgs e)
        {
            if (btnSaveResizedImages.Text == ResizeText)
            {
                btnSaveResizedImages.Text = CancelText;
                Properties.Settings.Default.MaxImageSize
                    = GetMaxImageSize();
                Properties.Settings.Default.Save();
                PrepareResizeWorkAsync();
            }
            else
            {
                ri.CancelResizeWork();
                btnSaveResizedImages.Text = ResizeText;
            }
        }

        private void PrepareResizeWorkAsync()
        {
            #region Initializing
            gbSourceMode.Enabled = false;
            gbOutputFolder.Enabled = false;
            gbMaxSize.Enabled = false;
            #endregion
            ri.DoResizeWorkAsync
                (lblOutputFolder.Text
                , GetMaxImageSize()
                , rbChooseFiles.Checked
                , listBoxFiles.Items
                , rbChooseFolder.Checked
                , lblInputFolder.Text
                , chbxIncludeSubFolders.Checked
                );
        }

        private void SetMaxImageSize()
        {
            int maxSize = Properties.Settings.Default.MaxImageSize;
            foreach (Control c in gbMaxSize.Controls)
            {
                RadioButton rb = (RadioButton)c;
                if (rb != null)
                {
                    rb.Checked = maxSize == Int32.Parse(rb.Text);
                }
            }
        }

        private int GetMaxImageSize()
        {
            int maxSize = 1600;
            foreach (Control c in gbMaxSize.Controls)
            {
                RadioButton rb = (RadioButton)c;
                if (rb != null && rb.Checked)
                {
                    maxSize = Int32.Parse(rb.Text);
                }
            }
            return maxSize;
        }

        #endregion

        #endregion
    }
}
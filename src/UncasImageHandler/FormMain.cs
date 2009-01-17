using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Uncas.Drawing;

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

        private ImageHandler ih = new ImageHandler();
        private const string ResizeText = "Gem";
        private const string CancelText = "Afbryd";

        #endregion

        #region Event and method cycle

        void FormMain_Load(object sender, EventArgs e)
        {
            btnSaveResizedImages.Text = ResizeText;

            #region Subscribes to event handlers
            rbChooseFiles.CheckedChanged += new EventHandler(rbSourceMode_CheckedChanged);
            rbChooseFolder.CheckedChanged += new EventHandler(rbSourceMode_CheckedChanged);
            btnChooseInputFolder.Click += new EventHandler(btnChooseInputFolder_Click);
            btnChooseInputFiles.Click += new EventHandler(btnChooseInputFiles_Click);
            btnChooseOutputFolder.Click += new EventHandler(btnChooseOutputFolder_Click);
            btnClear.Click += new EventHandler(btnClear_Click);
            btnSaveResizedImages.Click += new EventHandler(btnSaveResizedImages_Click);
            resizeWorker.DoWork += new DoWorkEventHandler(resizeWorker_DoWork);
            resizeWorker.ProgressChanged +=
                new ProgressChangedEventHandler(resizeWorker_ProgressChanged);
            resizeWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(resizeWorker_RunWorkerCompleted);
            #endregion

            #region Sets folders on file and folder dialogs
            Environment.SpecialFolder myPictures =
                Environment.SpecialFolder.MyPictures;
            string myPicturesPath = Environment.GetFolderPath(myPictures);
            openFileDialog1.InitialDirectory = myPicturesPath;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowserDialog1.SelectedPath = myPicturesPath;
            #endregion

            #region Input folder path
            string inputFolderPath = Properties.Settings.Default.InputFolderPath;
            if (inputFolderPath != null && inputFolderPath.Trim() != string.Empty)
                lblInputFolder.Text = inputFolderPath;
            else
                lblInputFolder.Text = myPicturesPath;
            #endregion

            #region Output folder path
            string outputFolderPath = Properties.Settings.Default.OutputFolderPath;
            if (outputFolderPath != null && outputFolderPath.Trim() != string.Empty)
                lblOutputFolder.Text = outputFolderPath;
            else
                lblOutputFolder.Text = myPicturesPath;
            #endregion

            ChangeSourceMode();

            SetMaxImageSize();
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
            lblInputFolder.Text = GetFolder(lblInputFolder.Text);
            Properties.Settings.Default.InputFolderPath = lblInputFolder.Text;
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
                    foreach (string fileName in openFileDialog1.FileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        listBoxFiles.Items.Add(fileName);
                        folderBrowserDialog1.SelectedPath = fi.DirectoryName;
                        lblOutputFolder.Text = fi.DirectoryName;
                    }
                    break;
            }
        }

        void btnChooseOutputFolder_Click(object sender, EventArgs e)
        {
            lblOutputFolder.Text = GetFolder(lblOutputFolder.Text);
            Properties.Settings.Default.OutputFolderPath = lblOutputFolder.Text;
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

        #endregion

        #region Resizing the chosen images

        private void btnSaveResizedImages_Click(object sender, EventArgs e)
        {
            if (btnSaveResizedImages.Text == ResizeText)
            {
                btnSaveResizedImages.Text = CancelText;
                Properties.Settings.Default.MaxImageSize = GetMaxImageSize();
                Properties.Settings.Default.Save();
                DoResizeWorkAsync();
            }
            else
            {
                resizeWorker.CancelAsync();
                btnSaveResizedImages.Text = ResizeText;
            }
        }

        #region Running in background thread

        private class ImageToResize
        {
            public string OriginalImagePath { get; set; }
            public string ResizedImagePath { get; set; }
        }

        private class SelectedImagesInfo
        {
            public int MaxImageSize { get; set; }
            public List<ImageToResize> ImagesToResize { get; set; }
        }

        private class ProcessedImagesInfo
        {
            public int TotalNumberOfImages { get; set; }
            public int ResizedNumberOfImages { get; set; }

            public override string ToString()
            {
                return string.Format("{0}/{1}", this.ResizedNumberOfImages, this.TotalNumberOfImages);
            }
        }

        private void DoResizeWorkAsync()
        {
            #region Initializing
            gbSourceMode.Enabled = false;
            gbOutputFolder.Enabled = false;
            gbMaxSize.Enabled = false;
            #endregion
            #region Getting the list of images
            string baseOutputFolder = lblOutputFolder.Text;
            List<ImageToResize> imagesToResize = new List<ImageToResize>();
            if (rbChooseFiles.Checked)
            {
                ListBox.ObjectCollection items = listBoxFiles.Items;
                imagesToResize = GetSelectedImages(baseOutputFolder, items);
            }
            else if (rbChooseFolder.Checked)
            {
                string baseInputFolder = lblInputFolder.Text;
                DirectoryInfo diBase = new DirectoryInfo(baseInputFolder);
                try
                {
                    GetImagesInSelectedFolder(ref imagesToResize,
                        diBase.Name, baseOutputFolder, diBase
                        , chbxIncludeSubFolders.Checked);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
            #endregion
            #region Resizing the images
            SelectedImagesInfo sfi = new SelectedImagesInfo
            {
                ImagesToResize = imagesToResize,
                MaxImageSize = GetMaxImageSize()
            };
            resizeWorker.RunWorkerAsync(sfi);
            #endregion
        }

        void resizeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SelectedImagesInfo sfi = (SelectedImagesInfo)e.Argument;
            int filesCompleted = 0;
            ProcessedImagesInfo pif = new ProcessedImagesInfo
            {
                TotalNumberOfImages = sfi.ImagesToResize.Count,
                ResizedNumberOfImages = filesCompleted
            };
            foreach (ImageToResize itr in sfi.ImagesToResize)
            {
                pif.ResizedNumberOfImages = filesCompleted;
                int percentage = (int)((100d * filesCompleted)
                    / (1d * sfi.ImagesToResize.Count));
                resizeWorker.ReportProgress(percentage, pif);
                if (resizeWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ResizeImage(itr.OriginalImagePath, itr.ResizedImagePath, sfi.MaxImageSize);
                filesCompleted++;
            }
            pif.ResizedNumberOfImages = filesCompleted;
            e.Result = pif;
        }

        void resizeWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProcessedImagesInfo pif = (ProcessedImagesInfo)e.UserState;
            lblNumberCompleted.Text = pif.ToString();
            progressBar1.Value = e.ProgressPercentage;
        }

        void resizeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Arbejdet blev afbrudt");
            }
            else
            {
                ProcessedImagesInfo pif = (ProcessedImagesInfo)e.Result;
                lblNumberCompleted.Text = pif.ToString();
                progressBar1.Value = 100;
                MessageBox.Show("Billederne blev gemt");
            }
            gbSourceMode.Enabled = true;
            gbOutputFolder.Enabled = true;
            gbMaxSize.Enabled = true;
            btnSaveResizedImages.Text = ResizeText;
            Process.Start(lblOutputFolder.Text);
        }

        #endregion

        #region The actual resizing methods

        private void SetMaxImageSize()
        {
            int maxSize = Properties.Settings.Default.MaxImageSize;
            foreach (Control c in gbMaxSize.Controls)
            {
                RadioButton rb = (RadioButton)c;
                if (rb != null)
                    rb.Checked = maxSize == Int32.Parse(rb.Text);
            }
        }

        private int GetMaxImageSize()
        {
            int maxSize = 1600;
            foreach (Control c in gbMaxSize.Controls)
            {
                RadioButton rb = (RadioButton)c;
                if (rb != null && rb.Checked)
                    maxSize = Int32.Parse(rb.Text);
            }
            return maxSize;
        }

        private List<ImageToResize> GetSelectedImages(string baseOutputFolder
            , ListBox.ObjectCollection items)
        {
            List<ImageToResize> imagesToResize = new List<ImageToResize>();
            foreach (string filePath in items)
            {
                FileInfo fi = new FileInfo(filePath);
                string smallerFilePath =
                    Path.Combine(baseOutputFolder, fi.Name);
                imagesToResize.Add(new ImageToResize
                {
                    OriginalImagePath = filePath,
                    ResizedImagePath = smallerFilePath
                });
            }
            return imagesToResize;
        }

        private void GetImagesInSelectedFolder(ref List<ImageToResize> imagesToResize
            , string relativeInputFolderPath
            , string baseOutputFolder, DirectoryInfo di, bool includeSubFolders)
        {
            string inputFolderPath = di.FullName;
            string outputFolderPath = Path.Combine(baseOutputFolder, relativeInputFolderPath);
            DirectoryInfo diOutput = new DirectoryInfo(outputFolderPath);
            // Getting images in this folder
            GetFilesByExtension(imagesToResize, di, outputFolderPath, diOutput, "*.jpg");
            GetFilesByExtension(imagesToResize, di, outputFolderPath, diOutput, "*.jpeg");
            GetFilesByExtension(imagesToResize, di, outputFolderPath, diOutput, "*.bmp");
            GetFilesByExtension(imagesToResize, di, outputFolderPath, diOutput, "*.png");
            GetFilesByExtension(imagesToResize, di, outputFolderPath, diOutput, "*.gif");
            if (includeSubFolders)
            {
                // Resizing images in subfolders
                foreach (DirectoryInfo diChild in di.GetDirectories())
                {
                    string childRelativePath = Path.Combine(relativeInputFolderPath, diChild.Name);
                    GetImagesInSelectedFolder(ref imagesToResize, childRelativePath, baseOutputFolder
                         , diChild, includeSubFolders);
                }
            }
        }

        private void GetFilesByExtension(List<ImageToResize> imagesToResize, DirectoryInfo di
            , string outputFolderPath, DirectoryInfo diOutput, string extension)
        {
            foreach (FileInfo fi in di.GetFiles(extension))
            {
                if (!diOutput.Exists)
                    diOutput.Create();
                string originalImagePath = fi.FullName;
                string resizedImagePath = Path.Combine(outputFolderPath, fi.Name);
                imagesToResize.Add(new ImageToResize
                {
                    OriginalImagePath = originalImagePath,
                    ResizedImagePath = resizedImagePath
                });
            }
        }

        private void ResizeImage(string originalImagePath, string resizedImagePath
            , int maxImageSize)
        {
            try
            {
                if (File.Exists(originalImagePath) && !File.Exists(resizedImagePath))
                {
                    byte[] originalBytes = File.ReadAllBytes(originalImagePath);
                    byte[] resizedBytes = ih.GetThumbnail(originalBytes, maxImageSize);
                    originalBytes = null;
                    File.WriteAllBytes(resizedImagePath, resizedBytes);
                    resizedBytes = null;
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion

        #endregion

        private void HandleException(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        #endregion
    }
}
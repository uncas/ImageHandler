namespace UncasImageHandler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnChooseInputFiles = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.rb1024 = new System.Windows.Forms.RadioButton();
            this.rb800 = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnChooseOutputFolder = new System.Windows.Forms.Button();
            this.btnSaveResizedImages = new System.Windows.Forms.Button();
            this.rb640 = new System.Windows.Forms.RadioButton();
            this.gbMaxSize = new System.Windows.Forms.GroupBox();
            this.rb1920 = new System.Windows.Forms.RadioButton();
            this.rb1600 = new System.Windows.Forms.RadioButton();
            this.rb1280 = new System.Windows.Forms.RadioButton();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbSourceMode = new System.Windows.Forms.GroupBox();
            this.chbxIncludeSubFolders = new System.Windows.Forms.CheckBox();
            this.lblInputFolder = new System.Windows.Forms.Label();
            this.btnChooseInputFolder = new System.Windows.Forms.Button();
            this.rbChooseFiles = new System.Windows.Forms.RadioButton();
            this.rbChooseFolder = new System.Windows.Forms.RadioButton();
            this.resizeWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblNumberCompleted = new System.Windows.Forms.Label();
            this.gbOutputFolder = new System.Windows.Forms.GroupBox();
            this.gbMaxSize.SuspendLayout();
            this.gbSourceMode.SuspendLayout();
            this.gbOutputFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "jpg";
            this.openFileDialog1.FileName = "Vælg fil";
            this.openFileDialog1.Filter = "jpeg |*.jpg| gif |*.gif| png |*.png| bmp |*.bmp| Alle filer |*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // btnChooseInputFiles
            // 
            this.btnChooseInputFiles.Location = new System.Drawing.Point(99, 68);
            this.btnChooseInputFiles.Name = "btnChooseInputFiles";
            this.btnChooseInputFiles.Size = new System.Drawing.Size(80, 23);
            this.btnChooseInputFiles.TabIndex = 0;
            this.btnChooseInputFiles.Text = "Gennemse...";
            this.btnChooseInputFiles.UseVisualStyleBackColor = true;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.Location = new System.Drawing.Point(6, 97);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(516, 95);
            this.listBoxFiles.TabIndex = 2;
            // 
            // rb1024
            // 
            this.rb1024.AutoSize = true;
            this.rb1024.Location = new System.Drawing.Point(174, 19);
            this.rb1024.Name = "rb1024";
            this.rb1024.Size = new System.Drawing.Size(49, 17);
            this.rb1024.TabIndex = 3;
            this.rb1024.Text = "1024";
            this.rb1024.UseVisualStyleBackColor = true;
            // 
            // rb800
            // 
            this.rb800.AutoSize = true;
            this.rb800.Location = new System.Drawing.Point(229, 19);
            this.rb800.Name = "rb800";
            this.rb800.Size = new System.Drawing.Size(43, 17);
            this.rb800.TabIndex = 4;
            this.rb800.Text = "800";
            this.rb800.UseVisualStyleBackColor = true;
            // 
            // btnChooseOutputFolder
            // 
            this.btnChooseOutputFolder.Location = new System.Drawing.Point(8, 24);
            this.btnChooseOutputFolder.Name = "btnChooseOutputFolder";
            this.btnChooseOutputFolder.Size = new System.Drawing.Size(90, 23);
            this.btnChooseOutputFolder.TabIndex = 5;
            this.btnChooseOutputFolder.Text = "Find mappe...";
            this.btnChooseOutputFolder.UseVisualStyleBackColor = true;
            // 
            // btnSaveResizedImages
            // 
            this.btnSaveResizedImages.Location = new System.Drawing.Point(392, 308);
            this.btnSaveResizedImages.Name = "btnSaveResizedImages";
            this.btnSaveResizedImages.Size = new System.Drawing.Size(55, 23);
            this.btnSaveResizedImages.TabIndex = 7;
            this.btnSaveResizedImages.Text = "Gem";
            this.btnSaveResizedImages.UseVisualStyleBackColor = true;
            // 
            // rb640
            // 
            this.rb640.AutoSize = true;
            this.rb640.Location = new System.Drawing.Point(278, 19);
            this.rb640.Name = "rb640";
            this.rb640.Size = new System.Drawing.Size(43, 17);
            this.rb640.TabIndex = 9;
            this.rb640.Text = "640";
            this.rb640.UseVisualStyleBackColor = true;
            // 
            // gbMaxSize
            // 
            this.gbMaxSize.Controls.Add(this.rb1920);
            this.gbMaxSize.Controls.Add(this.rb1600);
            this.gbMaxSize.Controls.Add(this.rb1280);
            this.gbMaxSize.Controls.Add(this.rb1024);
            this.gbMaxSize.Controls.Add(this.rb640);
            this.gbMaxSize.Controls.Add(this.rb800);
            this.gbMaxSize.Location = new System.Drawing.Point(11, 292);
            this.gbMaxSize.Name = "gbMaxSize";
            this.gbMaxSize.Size = new System.Drawing.Size(330, 47);
            this.gbMaxSize.TabIndex = 10;
            this.gbMaxSize.TabStop = false;
            this.gbMaxSize.Text = "Maksimal størrelse";
            // 
            // rb1920
            // 
            this.rb1920.AutoSize = true;
            this.rb1920.Location = new System.Drawing.Point(9, 19);
            this.rb1920.Name = "rb1920";
            this.rb1920.Size = new System.Drawing.Size(49, 17);
            this.rb1920.TabIndex = 12;
            this.rb1920.Text = "1920";
            this.rb1920.UseVisualStyleBackColor = true;
            // 
            // rb1600
            // 
            this.rb1600.AutoSize = true;
            this.rb1600.Checked = true;
            this.rb1600.Location = new System.Drawing.Point(64, 19);
            this.rb1600.Name = "rb1600";
            this.rb1600.Size = new System.Drawing.Size(49, 17);
            this.rb1600.TabIndex = 11;
            this.rb1600.TabStop = true;
            this.rb1600.Text = "1600";
            this.rb1600.UseVisualStyleBackColor = true;
            // 
            // rb1280
            // 
            this.rb1280.AutoSize = true;
            this.rb1280.Location = new System.Drawing.Point(119, 19);
            this.rb1280.Name = "rb1280";
            this.rb1280.Size = new System.Drawing.Size(49, 17);
            this.rb1280.TabIndex = 10;
            this.rb1280.Text = "1280";
            this.rb1280.UseVisualStyleBackColor = true;
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(104, 29);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(258, 13);
            this.lblOutputFolder.TabIndex = 11;
            this.lblOutputFolder.Text = "Vælg mappe til de nye billeder i den anden størrelse...";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(185, 68);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 23);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Ryd";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // gbSourceMode
            // 
            this.gbSourceMode.Controls.Add(this.chbxIncludeSubFolders);
            this.gbSourceMode.Controls.Add(this.lblInputFolder);
            this.gbSourceMode.Controls.Add(this.btnChooseInputFolder);
            this.gbSourceMode.Controls.Add(this.rbChooseFiles);
            this.gbSourceMode.Controls.Add(this.btnClear);
            this.gbSourceMode.Controls.Add(this.rbChooseFolder);
            this.gbSourceMode.Controls.Add(this.btnChooseInputFiles);
            this.gbSourceMode.Controls.Add(this.listBoxFiles);
            this.gbSourceMode.Location = new System.Drawing.Point(12, 12);
            this.gbSourceMode.Name = "gbSourceMode";
            this.gbSourceMode.Size = new System.Drawing.Size(528, 203);
            this.gbSourceMode.TabIndex = 13;
            this.gbSourceMode.TabStop = false;
            this.gbSourceMode.Text = "Vælg mappe eller individuelle filer";
            // 
            // chbxIncludeSubFolders
            // 
            this.chbxIncludeSubFolders.AutoSize = true;
            this.chbxIncludeSubFolders.Location = new System.Drawing.Point(185, 24);
            this.chbxIncludeSubFolders.Name = "chbxIncludeSubFolders";
            this.chbxIncludeSubFolders.Size = new System.Drawing.Size(146, 17);
            this.chbxIncludeSubFolders.TabIndex = 15;
            this.chbxIncludeSubFolders.Text = "Medtag alle undermapper";
            this.chbxIncludeSubFolders.UseVisualStyleBackColor = true;
            // 
            // lblInputFolder
            // 
            this.lblInputFolder.AutoSize = true;
            this.lblInputFolder.Location = new System.Drawing.Point(104, 46);
            this.lblInputFolder.Name = "lblInputFolder";
            this.lblInputFolder.Size = new System.Drawing.Size(75, 13);
            this.lblInputFolder.TabIndex = 14;
            this.lblInputFolder.Text = "Valgt mappe...";
            // 
            // btnChooseInputFolder
            // 
            this.btnChooseInputFolder.Location = new System.Drawing.Point(99, 20);
            this.btnChooseInputFolder.Name = "btnChooseInputFolder";
            this.btnChooseInputFolder.Size = new System.Drawing.Size(80, 23);
            this.btnChooseInputFolder.TabIndex = 13;
            this.btnChooseInputFolder.Text = "Find mappe...";
            this.btnChooseInputFolder.UseVisualStyleBackColor = true;
            // 
            // rbChooseFiles
            // 
            this.rbChooseFiles.AutoSize = true;
            this.rbChooseFiles.Location = new System.Drawing.Point(6, 74);
            this.rbChooseFiles.Name = "rbChooseFiles";
            this.rbChooseFiles.Size = new System.Drawing.Size(69, 17);
            this.rbChooseFiles.TabIndex = 1;
            this.rbChooseFiles.Text = "Vælg filer";
            this.rbChooseFiles.UseVisualStyleBackColor = true;
            // 
            // rbChooseFolder
            // 
            this.rbChooseFolder.AutoSize = true;
            this.rbChooseFolder.Checked = true;
            this.rbChooseFolder.Location = new System.Drawing.Point(8, 20);
            this.rbChooseFolder.Name = "rbChooseFolder";
            this.rbChooseFolder.Size = new System.Drawing.Size(85, 17);
            this.rbChooseFolder.TabIndex = 0;
            this.rbChooseFolder.TabStop = true;
            this.rbChooseFolder.Text = "Vælg mappe";
            this.rbChooseFolder.UseVisualStyleBackColor = true;
            // 
            // resizeWorker
            // 
            this.resizeWorker.WorkerReportsProgress = true;
            this.resizeWorker.WorkerSupportsCancellation = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 341);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(523, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // lblNumberCompleted
            // 
            this.lblNumberCompleted.AutoSize = true;
            this.lblNumberCompleted.Location = new System.Drawing.Point(453, 315);
            this.lblNumberCompleted.Name = "lblNumberCompleted";
            this.lblNumberCompleted.Size = new System.Drawing.Size(24, 13);
            this.lblNumberCompleted.TabIndex = 16;
            this.lblNumberCompleted.Text = "0/0";
            // 
            // gbOutputFolder
            // 
            this.gbOutputFolder.Controls.Add(this.btnChooseOutputFolder);
            this.gbOutputFolder.Controls.Add(this.lblOutputFolder);
            this.gbOutputFolder.Location = new System.Drawing.Point(12, 221);
            this.gbOutputFolder.Name = "gbOutputFolder";
            this.gbOutputFolder.Size = new System.Drawing.Size(445, 65);
            this.gbOutputFolder.TabIndex = 17;
            this.gbOutputFolder.TabStop = false;
            this.gbOutputFolder.Text = "Vælg mappe til de nye billeder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 376);
            this.Controls.Add(this.gbOutputFolder);
            this.Controls.Add(this.lblNumberCompleted);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gbSourceMode);
            this.Controls.Add(this.gbMaxSize);
            this.Controls.Add(this.btnSaveResizedImages);
            this.Name = "Form1";
            this.Text = "Lav billeder mindre";
            this.gbMaxSize.ResumeLayout(false);
            this.gbMaxSize.PerformLayout();
            this.gbSourceMode.ResumeLayout(false);
            this.gbSourceMode.PerformLayout();
            this.gbOutputFolder.ResumeLayout(false);
            this.gbOutputFolder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnChooseInputFiles;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.RadioButton rb1024;
        private System.Windows.Forms.RadioButton rb800;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnChooseOutputFolder;
        private System.Windows.Forms.Button btnSaveResizedImages;
        private System.Windows.Forms.RadioButton rb640;
        private System.Windows.Forms.GroupBox gbMaxSize;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton rb1280;
        private System.Windows.Forms.RadioButton rb1600;
        private System.Windows.Forms.RadioButton rb1920;
        private System.Windows.Forms.GroupBox gbSourceMode;
        private System.Windows.Forms.RadioButton rbChooseFiles;
        private System.Windows.Forms.RadioButton rbChooseFolder;
        private System.Windows.Forms.Button btnChooseInputFolder;
        private System.Windows.Forms.Label lblInputFolder;
        private System.Windows.Forms.CheckBox chbxIncludeSubFolders;
        private System.ComponentModel.BackgroundWorker resizeWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblNumberCompleted;
        private System.Windows.Forms.GroupBox gbOutputFolder;
    }
}


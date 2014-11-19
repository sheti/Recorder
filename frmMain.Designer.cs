namespace recorder
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.prbLeftChanel = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prbRightChanel = new System.Windows.Forms.ProgressBar();
            this.gpbAudioInput = new System.Windows.Forms.GroupBox();
            this.cmbWasapiDevices = new System.Windows.Forms.ComboBox();
            this.rbnInputSelect = new System.Windows.Forms.RadioButton();
            this.rbnInputDefault = new System.Windows.Forms.RadioButton();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.gpbFileCut = new System.Windows.Forms.GroupBox();
            this.cmbCutTimeVariant = new System.Windows.Forms.ComboBox();
            this.rbnCutClock = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCutTime = new System.Windows.Forms.NumericUpDown();
            this.rbnCutTimer = new System.Windows.Forms.RadioButton();
            this.rbnNoCut = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tmrRecordTime = new System.Windows.Forms.Timer(this.components);
            this.tmrWriteData = new System.Windows.Forms.Timer(this.components);
            this.tmrCut = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslDirPath = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsslDirPathCopyToBufer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsslDirPathOpenExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.gpbSave = new System.Windows.Forms.GroupBox();
            this.rbnSaveFiles = new System.Windows.Forms.RadioButton();
            this.rbnSaveFolderFiles = new System.Windows.Forms.RadioButton();
            this.gpbAudioInput.SuspendLayout();
            this.gpbFileCut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCutTime)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.gpbSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // prbLeftChanel
            // 
            this.prbLeftChanel.Location = new System.Drawing.Point(67, 12);
            this.prbLeftChanel.Name = "prbLeftChanel";
            this.prbLeftChanel.Size = new System.Drawing.Size(205, 23);
            this.prbLeftChanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Левый";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Правый";
            // 
            // prbRightChanel
            // 
            this.prbRightChanel.Location = new System.Drawing.Point(67, 43);
            this.prbRightChanel.Name = "prbRightChanel";
            this.prbRightChanel.Size = new System.Drawing.Size(205, 23);
            this.prbRightChanel.TabIndex = 3;
            // 
            // gpbAudioInput
            // 
            this.gpbAudioInput.Controls.Add(this.cmbWasapiDevices);
            this.gpbAudioInput.Controls.Add(this.rbnInputSelect);
            this.gpbAudioInput.Controls.Add(this.rbnInputDefault);
            this.gpbAudioInput.Location = new System.Drawing.Point(14, 93);
            this.gpbAudioInput.Name = "gpbAudioInput";
            this.gpbAudioInput.Size = new System.Drawing.Size(259, 75);
            this.gpbAudioInput.TabIndex = 4;
            this.gpbAudioInput.TabStop = false;
            this.gpbAudioInput.Text = "Источник звука";
            // 
            // cmbWasapiDevices
            // 
            this.cmbWasapiDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWasapiDevices.FormattingEnabled = true;
            this.cmbWasapiDevices.Location = new System.Drawing.Point(27, 41);
            this.cmbWasapiDevices.Name = "cmbWasapiDevices";
            this.cmbWasapiDevices.Size = new System.Drawing.Size(226, 21);
            this.cmbWasapiDevices.TabIndex = 2;
            // 
            // rbnInputSelect
            // 
            this.rbnInputSelect.AutoSize = true;
            this.rbnInputSelect.Location = new System.Drawing.Point(7, 44);
            this.rbnInputSelect.Name = "rbnInputSelect";
            this.rbnInputSelect.Size = new System.Drawing.Size(14, 13);
            this.rbnInputSelect.TabIndex = 1;
            this.rbnInputSelect.UseVisualStyleBackColor = true;
            // 
            // rbnInputDefault
            // 
            this.rbnInputDefault.AutoSize = true;
            this.rbnInputDefault.Checked = true;
            this.rbnInputDefault.Location = new System.Drawing.Point(7, 20);
            this.rbnInputDefault.Name = "rbnInputDefault";
            this.rbnInputDefault.Size = new System.Drawing.Size(159, 17);
            this.rbnInputDefault.TabIndex = 0;
            this.rbnInputDefault.TabStop = true;
            this.rbnInputDefault.Text = "Устройство по умолчанию";
            this.rbnInputDefault.UseVisualStyleBackColor = true;
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Location = new System.Drawing.Point(15, 362);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(124, 23);
            this.btnFolderSelect.TabIndex = 5;
            this.btnFolderSelect.Text = "Папка для записи...";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Enabled = false;
            this.btnRecord.Location = new System.Drawing.Point(154, 362);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(58, 23);
            this.btnRecord.TabIndex = 6;
            this.btnRecord.Text = "Запись";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(215, 362);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gpbFileCut
            // 
            this.gpbFileCut.Controls.Add(this.cmbCutTimeVariant);
            this.gpbFileCut.Controls.Add(this.rbnCutClock);
            this.gpbFileCut.Controls.Add(this.label3);
            this.gpbFileCut.Controls.Add(this.nudCutTime);
            this.gpbFileCut.Controls.Add(this.rbnCutTimer);
            this.gpbFileCut.Controls.Add(this.rbnNoCut);
            this.gpbFileCut.Location = new System.Drawing.Point(15, 174);
            this.gpbFileCut.Name = "gpbFileCut";
            this.gpbFileCut.Size = new System.Drawing.Size(258, 104);
            this.gpbFileCut.TabIndex = 8;
            this.gpbFileCut.TabStop = false;
            this.gpbFileCut.Text = "Разбивка";
            // 
            // cmbCutTimeVariant
            // 
            this.cmbCutTimeVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCutTimeVariant.FormattingEnabled = true;
            this.cmbCutTimeVariant.Items.AddRange(new object[] {
            "в начале часа",
            "в середине часа",
            "в начале суток",
            "в полдень"});
            this.cmbCutTimeVariant.Location = new System.Drawing.Point(93, 69);
            this.cmbCutTimeVariant.Name = "cmbCutTimeVariant";
            this.cmbCutTimeVariant.Size = new System.Drawing.Size(145, 21);
            this.cmbCutTimeVariant.TabIndex = 6;
            // 
            // rbnCutClock
            // 
            this.rbnCutClock.AutoSize = true;
            this.rbnCutClock.Location = new System.Drawing.Point(7, 70);
            this.rbnCutClock.Name = "rbnCutClock";
            this.rbnCutClock.Size = new System.Drawing.Size(86, 17);
            this.rbnCutClock.TabIndex = 5;
            this.rbnCutClock.TabStop = true;
            this.rbnCutClock.Text = "новый файл";
            this.rbnCutClock.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "секунд";
            // 
            // nudCutTime
            // 
            this.nudCutTime.Location = new System.Drawing.Point(70, 43);
            this.nudCutTime.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.nudCutTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCutTime.Name = "nudCutTime";
            this.nudCutTime.Size = new System.Drawing.Size(62, 20);
            this.nudCutTime.TabIndex = 2;
            this.nudCutTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // rbnCutTimer
            // 
            this.rbnCutTimer.AutoSize = true;
            this.rbnCutTimer.Location = new System.Drawing.Point(6, 43);
            this.rbnCutTimer.Name = "rbnCutTimer";
            this.rbnCutTimer.Size = new System.Drawing.Size(65, 17);
            this.rbnCutTimer.TabIndex = 1;
            this.rbnCutTimer.Text = "каждые";
            this.rbnCutTimer.UseVisualStyleBackColor = true;
            // 
            // rbnNoCut
            // 
            this.rbnNoCut.AutoSize = true;
            this.rbnNoCut.Checked = true;
            this.rbnNoCut.Location = new System.Drawing.Point(6, 20);
            this.rbnNoCut.Name = "rbnNoCut";
            this.rbnNoCut.Size = new System.Drawing.Size(124, 17);
            this.rbnNoCut.TabIndex = 0;
            this.rbnNoCut.TabStop = true;
            this.rbnNoCut.Text = "Не разбивать файл";
            this.rbnNoCut.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Время записи";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(91, 73);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 13);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "label5";
            // 
            // tmrRecordTime
            // 
            this.tmrRecordTime.Interval = 1000;
            this.tmrRecordTime.Tick += new System.EventHandler(this.tmrRecordTime_Tick);
            // 
            // tmrWriteData
            // 
            this.tmrWriteData.Interval = 1000;
            this.tmrWriteData.Tick += new System.EventHandler(this.tmrWriteData_Tick);
            // 
            // tmrCut
            // 
            this.tmrCut.Tick += new System.EventHandler(this.tmrCut_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDirPath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslDirPath
            // 
            this.tsslDirPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslDirPath.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDirPathCopyToBufer,
            this.tsslDirPathOpenExplorer});
            this.tsslDirPath.Image = ((System.Drawing.Image)(resources.GetObject("tsslDirPath.Image")));
            this.tsslDirPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsslDirPath.Name = "tsslDirPath";
            this.tsslDirPath.Size = new System.Drawing.Size(76, 20);
            this.tsslDirPath.Text = "tsslDirPath";
            this.tsslDirPath.Visible = false;
            // 
            // tsslDirPathCopyToBufer
            // 
            this.tsslDirPathCopyToBufer.Image = global::recorder.Properties.Resources._1415536258_Copy;
            this.tsslDirPathCopyToBufer.Name = "tsslDirPathCopyToBufer";
            this.tsslDirPathCopyToBufer.Size = new System.Drawing.Size(238, 22);
            this.tsslDirPathCopyToBufer.Text = "Скопировать в буфер обмена";
            this.tsslDirPathCopyToBufer.Click += new System.EventHandler(this.tsslDirPathCopyToBufer_Click);
            // 
            // tsslDirPathOpenExplorer
            // 
            this.tsslDirPathOpenExplorer.Image = global::recorder.Properties.Resources._1415536237_folder;
            this.tsslDirPathOpenExplorer.Name = "tsslDirPathOpenExplorer";
            this.tsslDirPathOpenExplorer.Size = new System.Drawing.Size(238, 22);
            this.tsslDirPathOpenExplorer.Text = "Открыть папку";
            this.tsslDirPathOpenExplorer.Click += new System.EventHandler(this.tsslDirPathOpenExplorer_Click);
            // 
            // gpbSave
            // 
            this.gpbSave.Controls.Add(this.rbnSaveFolderFiles);
            this.gpbSave.Controls.Add(this.rbnSaveFiles);
            this.gpbSave.Location = new System.Drawing.Point(15, 284);
            this.gpbSave.Name = "gpbSave";
            this.gpbSave.Size = new System.Drawing.Size(258, 72);
            this.gpbSave.TabIndex = 12;
            this.gpbSave.TabStop = false;
            this.gpbSave.Text = "Сохранение";
            // 
            // rbnSaveFiles
            // 
            this.rbnSaveFiles.AutoSize = true;
            this.rbnSaveFiles.Checked = true;
            this.rbnSaveFiles.Location = new System.Drawing.Point(7, 20);
            this.rbnSaveFiles.Name = "rbnSaveFiles";
            this.rbnSaveFiles.Size = new System.Drawing.Size(124, 17);
            this.rbnSaveFiles.TabIndex = 0;
            this.rbnSaveFiles.TabStop = true;
            this.rbnSaveFiles.Text = "Сохранять в файлы";
            this.rbnSaveFiles.UseVisualStyleBackColor = true;
            // 
            // rbnSaveFolderFiles
            // 
            this.rbnSaveFolderFiles.AutoSize = true;
            this.rbnSaveFolderFiles.Location = new System.Drawing.Point(7, 44);
            this.rbnSaveFolderFiles.Name = "rbnSaveFolderFiles";
            this.rbnSaveFolderFiles.Size = new System.Drawing.Size(164, 17);
            this.rbnSaveFolderFiles.TabIndex = 1;
            this.rbnSaveFolderFiles.TabStop = true;
            this.rbnSaveFolderFiles.Text = "Сохранять по дням в папки";
            this.rbnSaveFolderFiles.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 412);
            this.Controls.Add(this.gpbSave);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gpbFileCut);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.gpbAudioInput);
            this.Controls.Add(this.prbRightChanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prbLeftChanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Запись звука";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.gpbAudioInput.ResumeLayout(false);
            this.gpbAudioInput.PerformLayout();
            this.gpbFileCut.ResumeLayout(false);
            this.gpbFileCut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCutTime)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gpbSave.ResumeLayout(false);
            this.gpbSave.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prbLeftChanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar prbRightChanel;
        private System.Windows.Forms.GroupBox gpbAudioInput;
        private System.Windows.Forms.ComboBox cmbWasapiDevices;
        private System.Windows.Forms.RadioButton rbnInputSelect;
        private System.Windows.Forms.RadioButton rbnInputDefault;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.GroupBox gpbFileCut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCutTime;
        private System.Windows.Forms.RadioButton rbnCutTimer;
        private System.Windows.Forms.RadioButton rbnNoCut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrRecordTime;
        private System.Windows.Forms.Timer tmrWriteData;
        private System.Windows.Forms.ComboBox cmbCutTimeVariant;
        private System.Windows.Forms.RadioButton rbnCutClock;
        private System.Windows.Forms.Timer tmrCut;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tsslDirPath;
        private System.Windows.Forms.ToolStripMenuItem tsslDirPathCopyToBufer;
        private System.Windows.Forms.ToolStripMenuItem tsslDirPathOpenExplorer;
        private System.Windows.Forms.GroupBox gpbSave;
        private System.Windows.Forms.RadioButton rbnSaveFolderFiles;
        private System.Windows.Forms.RadioButton rbnSaveFiles;
    }
}


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
            this.gpbAudioInput.SuspendLayout();
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
            this.gpbAudioInput.Location = new System.Drawing.Point(13, 80);
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
            this.btnFolderSelect.Location = new System.Drawing.Point(13, 162);
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
            this.btnRecord.Location = new System.Drawing.Point(150, 162);
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
            this.btnStop.Location = new System.Drawing.Point(214, 162);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 192);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.gpbAudioInput);
            this.Controls.Add(this.prbRightChanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prbLeftChanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Запись звука";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gpbAudioInput.ResumeLayout(false);
            this.gpbAudioInput.PerformLayout();
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
    }
}


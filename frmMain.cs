using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Lame;

namespace recorder
{
    public partial class frmMain : Form
    {
        private string pathToFolderForRecodreFiles = "";
        // WaveIn - поток для записи
        WasapiCapture waveIn;
        //Класс для записи в файл
        object writer;
        MMDeviceCollection devices;
        MMDevice device, defaultDevice;
        int totalRecodrTime = 0, sectionRecordTime = 0;
        int stopStatus = 0; // 0 - не остановлен, 1 - остановлен, 2 - по таймеру
        Queue<byte> wave_data = new Queue<byte>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            if (fbdFolder.ShowDialog() == DialogResult.OK)
            {
                pathToFolderForRecodreFiles = fbdFolder.SelectedPath;
                tsslDirPath.Text = pathToFolderForRecodreFiles;
                tsslDirPath.Visible = true;
                btnRecord.Enabled = true;
            }
            else
            {
                pathToFolderForRecodreFiles = "";
                tsslDirPath.Text = "";
                tsslDirPath.Visible = false;
                btnRecord.Enabled = false;
                btnStop.Enabled = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
            devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            
            cmbWasapiDevices.Items.AddRange(devices.ToArray());

            if (cmbWasapiDevices.Items.Count > 0)
            {
                cmbWasapiDevices.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Не обнаружено ни одного устройства для записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            totalRecodrTime = 0;
            sectionRecordTime = 0;
            lblTime.Text = "";
            tsslDirPath.Text = "";
            tsslDirPath.Visible = false;
            cmbCutTimeVariant.SelectedIndex = 0;
            cmbTypeFile.SelectedIndex = 0;
            // Параметры приложения
            if (Properties.Settings.Default.p_device == 0)
            {
                rbnInputDefault.Checked = true;
            }
            else if (Properties.Settings.Default.p_device > 0 && Properties.Settings.Default.p_device <= cmbWasapiDevices.Items.Count)
            {
                rbnInputSelect.Checked = true;
                cmbWasapiDevices.SelectedIndex = Properties.Settings.Default.p_device - 1;
            }
            switch (Properties.Settings.Default.p_cut_type)
            {
                case "timer":
                    rbnCutTimer.Checked = true;
                    break;
                case "clock":
                    rbnCutClock.Checked = true;
                    break;
                default:
                    rbnNoCut.Checked = true;
                    break;
            }
            if (Properties.Settings.Default.p_cut_timer >= 5)
            {
                nudCutTime.Value = Properties.Settings.Default.p_cut_timer;
            }
            if (Properties.Settings.Default.p_cut_clock >= 0 && Properties.Settings.Default.p_cut_clock < cmbCutTimeVariant.Items.Count)
            {
                cmbCutTimeVariant.SelectedIndex = Properties.Settings.Default.p_cut_clock;
            }
            if (Directory.Exists(Properties.Settings.Default.p_dir))
            {
                pathToFolderForRecodreFiles = Properties.Settings.Default.p_dir;
                btnRecord.Enabled = true;
                tsslDirPath.Text = pathToFolderForRecodreFiles;
                tsslDirPath.Visible = true;
            }
            if (Properties.Settings.Default.p_save == 0)
            {
                rbnSaveFiles.Checked = true;
            }
            if (Properties.Settings.Default.p_save == 1)
            {
                rbnSaveFolderFiles.Checked = true;
            }
            // Аргументы коммандной строки
            String[] arguments = Environment.GetCommandLineArgs();
            bool autoStart = false;
            for(int i = 1; i < arguments.Length; i++) 
            {
                switch (arguments[i])
                {
                    case "--device":
                        if (i + 1 < arguments.Length)
                        {
                            i += 1;
                            int num = Int32.Parse(arguments[i]);
                            if (num == 0)
                            {
                                rbnInputDefault.Checked = true;
                            }
                            else
                            {
                                if (num > 0 && num <= cmbWasapiDevices.Items.Count)
                                {
                                    rbnInputSelect.Checked = true;
                                    cmbWasapiDevices.SelectedIndex = num - 1;
                                }
                            }
                        }
                        break;
                    case "--save-type-file":
                        rbnSaveFiles.Checked = true;
                        break;
                    case "--save-type-folder-file":
                        rbnSaveFolderFiles.Checked = true;
                        break;
                    case "--cut-type":
                        if (i + 1 < arguments.Length)
                        {
                            i += 1;
                            switch (arguments[i])
                            {
                                case "no":
                                    rbnNoCut.Checked = true;
                                    break;
                                case "timer":
                                    if (i + 1 < arguments.Length)
                                    {
                                        i += 1;
                                        int num = Int32.Parse(arguments[i]);
                                        if (num >= 5)
                                        {
                                            rbnCutTimer.Checked = true;
                                            nudCutTime.Value = num;
                                        }
                                    }
                                    break;
                                case "clock":
                                    if (i + 1 < arguments.Length)
                                    {
                                        i += 1;
                                        int num = Int32.Parse(arguments[i]);
                                        if (num > 0 && num <= cmbCutTimeVariant.Items.Count)
                                        {
                                            cmbCutTimeVariant.SelectedIndex = num - 1;
                                            rbnCutClock.Checked = true;
                                        }
                                    }
                                    break;
                                default:
                                    rbnNoCut.Checked = true;
                                    break;
                            }
                        }
                        break;
                    case "--dir":
                        if (i + 1 < arguments.Length)
                        {
                            i += 1;
                            if (Directory.Exists(arguments[i]))
                            {
                                pathToFolderForRecodreFiles = arguments[i];
                                btnRecord.Enabled = true;
                                tsslDirPath.Text = pathToFolderForRecodreFiles;
                                tsslDirPath.Visible = true;
                            }
                        }
                        break;
                    case "--record":
                        autoStart = true;
                        break;
                }
            }
            if (autoStart && btnRecord.Enabled)
            {
                btnRecord_Click(sender, e);
            }
        }

        private bool openRecordFile()
        {
            try
            {
                DateTime dt = DateTime.Now;
                string outputFilename = "";
                if (rbnSaveFiles.Checked)
                {
                    outputFilename = String.Format("{0}-{1:00}-{2:00}_{3:00}-{4:00}-{5:00}.", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                }
                if (rbnSaveFolderFiles.Checked)
                {
                    string directoryName = String.Format("{0}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);
                    if (!Directory.Exists(pathToFolderForRecodreFiles + "\\" + directoryName))
                    {
                        Directory.CreateDirectory(pathToFolderForRecodreFiles + "\\" + directoryName);
                    }
                    outputFilename = directoryName + "\\" + String.Format("{0:00}-{1:00}-{2:00}.",  dt.Hour, dt.Minute, dt.Second);
                }
                switch (cmbTypeFile.SelectedIndex)
                {
                    case 0:
                        outputFilename += "mp3";
                        writer = new LameMP3FileWriter(pathToFolderForRecodreFiles + "\\" + outputFilename, waveIn.WaveFormat, Properties.Settings.Default.bitrate);
                        break;
                    case 1:
                        outputFilename += "wav";
                        //Инициализируем объект WaveFileWriter
                        writer = new WaveFileWriter(pathToFolderForRecodreFiles+ "\\" + outputFilename, waveIn.WaveFormat);
                        break;
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не могу создать файл для записи. Ошибка: " + ex.Message);
                return false;
            }
            return true;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(pathToFolderForRecodreFiles))
                return;

            totalRecodrTime = 0;
            sectionRecordTime = 0;
            try
            {
                //Дефолтное устройство для записи (если оно имеется)
                if (rbnInputSelect.Checked)
                {
                    device = (MMDevice)cmbWasapiDevices.SelectedItem;
                }
                else
                {
                    device = defaultDevice;
                }
                waveIn = new WasapiCapture(device);
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += waveIn_RecordingStopped;
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                //waveIn.WaveFormat = new WaveFormat(8000, 1);

                tmrWriteData.Interval = 1000;
                if (!openRecordFile())
                    return;

                btnRecord.Enabled = false;
                btnStop.Enabled = true;
                btnFolderSelect.Enabled = false;
                gpbAudioInput.Enabled = false;
                gpbFileCut.Enabled = false;
                gpbSave.Enabled = false;
                //Начало записи
                stopStatus = 0;
                wave_data.Clear();
                waveIn.StartRecording();
                tmrRecordTime.Enabled = true;
                tmrWriteData.Enabled = true;
                startCutTimer();
            }
            catch (Exception ex)
            {
                if (writer != null)
                {
                    CloseWriter();
                }
                waveIn.Dispose();
                btnRecord.Enabled = true;
                btnStop.Enabled = false;
                btnFolderSelect.Enabled = true;
                gpbAudioInput.Enabled = true;
                tmrRecordTime.Enabled = false;
                gpbFileCut.Enabled = true;
                tmrRecordTime.Enabled = false;
                gpbSave.Enabled = true;
                lblTime.Text = "";
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseWriter()
        {
            switch (cmbTypeFile.SelectedIndex)
            {
                case 0:
                    ((LameMP3FileWriter)writer).Close();
                    ((LameMP3FileWriter)writer).Dispose();
                    break;
                case 1:
                    ((WaveFileWriter)writer).Close();
                    ((WaveFileWriter)writer).Dispose();
                    break;
            }
        }

        //Получение данных из входного буфера и обработка полученных с микрофона данных
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //writer.Write(e.Buffer, 0, e.BytesRecorded);
                for (int i = 0; i < e.BytesRecorded; i++)
                    wave_data.Enqueue(e.Buffer[i]);

                if (waveIn.WaveFormat.Channels == 2)
                {
                    prbLeftChanel.Value = (int)(Math.Round(device.AudioMeterInformation.PeakValues[0] * 100));
                    prbRightChanel.Value = (int)(Math.Round(device.AudioMeterInformation.PeakValues[1] * 100));
                }
                else
                {
                    prbLeftChanel.Value = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
                    prbRightChanel.Value = prbLeftChanel.Value;
                }
            }
        }

        //Окончание записи
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                stopStatus = 1;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            waveIn.StopRecording();
        }

        private string SecondsToMinSec(int seconds)
        {
            string buf = "";
            int hor = 0;
            int min = 0;
            hor = (int)Math.Floor(seconds / (double)(60 * 60));
            seconds -= hor * 60 * 60;
            min = (int)Math.Floor(seconds / (double)60);
            seconds -= min * 60;
            if (hor > 0)
                buf = String.Format("{0}:{1}:{2}", hor, min, seconds);
            else
                buf = String.Format("{0}:{1}", min, seconds);
            return buf;
        }

        private void tmrRecordTime_Tick(object sender, EventArgs e)
        {
            totalRecodrTime += 1;
            string time = SecondsToMinSec(totalRecodrTime);
            if (rbnNoCut.Checked == false)
            {
                sectionRecordTime += 1;
                time += " [" + SecondsToMinSec(sectionRecordTime) + "]";
            }
            lblTime.Text = time;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
            }
            if (rbnInputDefault.Checked == true)
            {
                Properties.Settings.Default.p_device = 0;
            }
            if (rbnInputSelect.Checked == true && cmbWasapiDevices.Items.Count > 0)
            {
                Properties.Settings.Default.p_device = cmbWasapiDevices.SelectedIndex + 1;
            }
            if (rbnNoCut.Checked == true)
            {
                Properties.Settings.Default.p_cut_type = "no";
            }
            if (rbnCutTimer.Checked == true)
            {
                Properties.Settings.Default.p_cut_type = "timer";
            }
            if (rbnCutClock.Checked == true)
            {
                Properties.Settings.Default.p_cut_type = "clock";
            }
            if (rbnSaveFiles.Checked)
            {
                Properties.Settings.Default.p_save = 0;
            }
            if (rbnSaveFolderFiles.Checked)
            {
                Properties.Settings.Default.p_save = 1;
            }
            Properties.Settings.Default.p_cut_timer = (int)nudCutTime.Value;
            Properties.Settings.Default.p_cut_clock = cmbCutTimeVariant.SelectedIndex;
            if (pathToFolderForRecodreFiles.Length > 0)
            {
                Properties.Settings.Default.p_dir = pathToFolderForRecodreFiles;
            }
            Properties.Settings.Default.Save();
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmAbout frm = new frmAbout();
                frm.ShowDialog();
                frm.Dispose();
            }
        }

        private void tmrWriteData_Tick(object sender, EventArgs e)
        {
            tmrWriteData.Enabled = false;
            int count = wave_data.Count;
           
            // Новый файл
            if (stopStatus == 2)
            {
                if (writer != null)
                {
                    CloseWriter();
                }

                if (!openRecordFile())
                    return;
                sectionRecordTime = 0;
                startCutTimer();
            }
            // Скидываем в файл всё что скопилось
            if(writer != null) 
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        switch (cmbTypeFile.SelectedIndex)
                        {
                            case 0:
                                ((LameMP3FileWriter)writer).WriteByte(wave_data.Dequeue());
                                break;
                            case 1:
                                ((WaveFileWriter)writer).WriteByte(wave_data.Dequeue());
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        CloseWriter();
                        writer = null;
                        stopStatus = 1;
                        MessageBox.Show("Не могу записать даныне в файл. Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            // Если запись не остановлено начинаем очередной цикл накопления данных
            if (stopStatus == 0 || stopStatus == 2)
            {
                tmrWriteData.Enabled = true;
            }
            
            if (stopStatus == 2)
            {
                stopStatus = 0;
            }
            // Полная остановка
            if (stopStatus == 1)
            {
                waveIn.Dispose();
                CloseWriter();
                prbLeftChanel.Value = 0;
                prbRightChanel.Value = 0;
                tmrRecordTime.Enabled = false;
                tmrCut.Enabled = false;
                btnStop.Enabled = false;
                btnRecord.Enabled = true;
                btnFolderSelect.Enabled = true;
                gpbAudioInput.Enabled = true;
                gpbFileCut.Enabled = true;
                gpbSave.Enabled = true;
            }
        }

        private void startCutTimer()
        {
            if (rbnCutTimer.Checked)
            {
                tmrCut.Interval = ((int)nudCutTime.Value - 1) * 1000;
                tmrCut.Enabled = true;
            }
            if (rbnCutClock.Checked)
            {
                DateTime now = DateTime.Now;
                switch (cmbCutTimeVariant.SelectedIndex)
                {
                    case 0: // В начале часа
                        tmrCut.Interval = (60 - now.Minute) * 60 * 1000 + (60 - now.Second) * 1000;
                        break;
                    case 1: // В середине часа
                        tmrCut.Interval = (30 - Math.Abs(now.Minute - 30)) * 60 * 1000 + (60 - now.Second) * 1000;
                        break;
                    case 2: // В начале суток
                        tmrCut.Interval = Math.Abs(now.Hour - 23) * 60 * 60 * 1000 + (60 - now.Minute) * 60 * 1000 + (60 - now.Second) * 1000;
                        break;
                    case 3: // В полдень
                        tmrCut.Interval = Math.Abs(now.Hour + 12 - 23) * 60 * 60 * 1000 + (60 - now.Minute) * 60 * 1000 + (60 - now.Second) * 1000;
                        break;
                }
                tmrCut.Enabled = true;
            }
        }

        private void tmrCut_Tick(object sender, EventArgs e)
        {
            stopStatus = 2;
            tmrCut.Enabled = false;
        }

        private void tsslDirPathCopyToBufer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(pathToFolderForRecodreFiles);
        }

        private void tsslDirPathOpenExplorer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(pathToFolderForRecodreFiles);
        }
    }
}

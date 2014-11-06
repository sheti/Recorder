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
        //WaveFileWriter writer;
        LameMP3FileWriter writer;
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
                btnRecord.Enabled = true;
            }
            else
            {
                pathToFolderForRecodreFiles = "";
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
            if (Properties.Settings.Default.p_cut == 0)
            {
                rbnNoCut.Checked = true;
            }
            else if (Properties.Settings.Default.p_cut > 0)
            {
                rbnCut.Checked = true;
                nudCutTime.Value = Properties.Settings.Default.p_cut;
            }
            else if (Properties.Settings.Default.p_cut < 0)
            {
                rbnCutEmpty.Checked = true;
            }
            if (Directory.Exists(Properties.Settings.Default.p_dir))
            {
                pathToFolderForRecodreFiles = Properties.Settings.Default.p_dir;
                btnRecord.Enabled = true;
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
                    case "--cut":
                        if (i + 1 < arguments.Length)
                        {
                            i += 1;
                            int num = Int32.Parse(arguments[i]);
                            if (num == 0)
                            {
                                rbnNoCut.Checked = true;
                            }
                            else if (num > 0)
                            {
                                    rbnCut.Checked = true;
                                    nudCutTime.Value = num;
                            }
                            else if (num < 0)
                            {
                                rbnCutEmpty.Checked = true;
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
                string outputFilename = String.Format("{0:00}-{1:00}-{2}_{3:00}-{4:00}-{5:00}.mp3", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second);
                //Инициализируем объект WaveFileWriter
                //writer = new WaveFileWriter(pathToFolderForRecodreFiles+ "\\" + outputFilename, waveIn.WaveFormat);
                writer = new LameMP3FileWriter(pathToFolderForRecodreFiles + "\\" + outputFilename, waveIn.WaveFormat, Properties.Settings.Default.bitrate);
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

                if (rbnNoCut.Checked || rbnCut.Checked)
                {
                    tmrWriteData.Interval = 1000;
                    if (!openRecordFile())
                        return;
                }
                else
                {
                    tmrWriteData.Interval = 10000;
                }

                btnRecord.Enabled = false;
                btnStop.Enabled = true;
                btnFolderSelect.Enabled = false;
                gpbAudioInput.Enabled = false;
                gpbFileCut.Enabled = false;
                //Начало записи
                stopStatus = 0;
                wave_data.Clear();
                waveIn.StartRecording();
                tmrRecordTime.Enabled = true;
                tmrWriteData.Enabled = true;
            }
            catch (Exception ex)
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                waveIn.Dispose();
                btnRecord.Enabled = true;
                btnStop.Enabled = false;
                btnFolderSelect.Enabled = true;
                gpbAudioInput.Enabled = true;
                tmrRecordTime.Enabled = false;
                gpbFileCut.Enabled = true;
                tmrRecordTime.Enabled = false;
                lblTime.Text = "";
                MessageBox.Show(ex.Message);
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
                if (rbnCutEmpty.Checked)
                {
                    tmrWriteData_Tick(sender, e);
                }
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
            if (rbnCut.Checked)
            {
                sectionRecordTime += 1;
                if (sectionRecordTime >= nudCutTime.Value)
                {
                    stopStatus = 2;
                }
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
                Properties.Settings.Default.p_cut = 0;
            }
            if (rbnCutEmpty.Checked == true)
            {
                Properties.Settings.Default.p_cut = -1;
            }
            if (rbnCut.Checked == true && nudCutTime.Value > 0)
            {
                Properties.Settings.Default.p_cut = Convert.ToInt32(nudCutTime.Value);
            }
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
            byte[] bufer = new byte[count];
            byte wave_bufer;
            int sum = 0;
            for (int i = 0; i < count; i++) 
            {
                wave_bufer = wave_data.Dequeue();
                bufer[i] = wave_bufer;
                sum += wave_bufer;
            }

            if (rbnCutEmpty.Checked && (sum / count > Properties.Settings.Default.levelOfSilence) && (writer == null))
            {
                stopStatus = 2;
            }
            if (rbnCutEmpty.Checked && (sum / count < Properties.Settings.Default.levelOfSilence) && (writer != null)) 
            {
                writer.Close();
                writer.Dispose();
                writer = null;
            }
            // Новый файл
            if (stopStatus == 2)
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }

                if (!openRecordFile())
                    return;
                sectionRecordTime = 0;
            }
            // Скидываем в файл всё что скопилось
            if(writer != null) 
            {
                writer.Write(bufer, 0, count);
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
                writer.Close();
                prbLeftChanel.Value = 0;
                prbRightChanel.Value = 0;
                tmrRecordTime.Enabled = false;
                btnStop.Enabled = false;
                btnRecord.Enabled = true;
                btnFolderSelect.Enabled = true;
                gpbAudioInput.Enabled = true;
                gpbFileCut.Enabled = true;
            }
        }
    }
}

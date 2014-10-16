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
                MessageBox.Show("Не обнаружено ни одного устройства дял записи", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            totalRecodrTime = 0;
            sectionRecordTime = 0;
            lblTime.Text = "";
        }

        private bool openRecordFile()
        {
            try
            {
                DateTime dt = DateTime.Now;
                string outputFilename = String.Format("{0}-{1}-{2}_{3}-{4}-{5}.mp3", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second);
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

                if (!openRecordFile())
                    return;

                btnRecord.Enabled = false;
                btnStop.Enabled = true;
                btnFolderSelect.Enabled = false;
                gpbAudioInput.Enabled = false;
                gpbFileCut.Enabled = false;
                //Начало записи
                waveIn.StartRecording();
                tmrRecordTime.Enabled = true;
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
                //writer.WriteData(e.Buffer, 0, e.BytesRecorded);
                writer.Write(e.Buffer, 0, e.BytesRecorded);

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
                tmrRecordTime.Enabled = false;
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                switch(stopStatus)
                {
                    case 1:
                        waveIn.Dispose();
                        prbLeftChanel.Value = 0;
                        prbRightChanel.Value = 0;
                        btnStop.Enabled = false;
                        btnRecord.Enabled = true;
                        btnFolderSelect.Enabled = true;
                        gpbAudioInput.Enabled = true;
                        gpbFileCut.Enabled = true;
                        break;
                    case 2:
                        if (!openRecordFile())
                            return;
                        try
                        {
                            waveIn.StartRecording();
                            tmrRecordTime.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            waveIn.Dispose();
                            btnRecord.Enabled = true;
                            btnStop.Enabled = false;
                            btnFolderSelect.Enabled = true;
                            gpbAudioInput.Enabled = true;
                            tmrRecordTime.Enabled = false;
                            gpbFileCut.Enabled = true;
                            totalRecodrTime = 0;
                            sectionRecordTime = 0;
                            tmrRecordTime.Enabled = false;
                            lblTime.Text = "";
                            MessageBox.Show(ex.Message);
                        }
                    break;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stopStatus = 1;
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
                    waveIn.StopRecording();
                    sectionRecordTime = 0;
                }
                time += "-" + SecondsToMinSec(sectionRecordTime);
            }
            lblTime.Text = time;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveIn != null)
            {
                stopStatus = 1;
                waveIn.StopRecording();
            }
        }
    }
}

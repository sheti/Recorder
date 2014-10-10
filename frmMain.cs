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
            //cmbWasapiDevices.DataSource = devices;
            //cmbWasapiDevices.DisplayMember = "FriendlyName";
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
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
               
                //Инициализируем объект WaveFileWriter
                DateTime dt = DateTime.Now;
                string outputFilename = String.Format("{0}-{1}-{2}_{3}-{4}-{5}.mp3", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second);
                //writer = new WaveFileWriter(pathToFolderForRecodreFiles+ "\\" + outputFilename, waveIn.WaveFormat);
                writer = new LameMP3FileWriter(pathToFolderForRecodreFiles + "\\" + outputFilename, waveIn.WaveFormat, 128);
                btnRecord.Enabled = false;
                btnStop.Enabled = true;
                gpbAudioInput.Enabled = false;
                //Начало записи
                waveIn.StartRecording();
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
                gpbAudioInput.Enabled = true;
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
                btnStop.Enabled = false;
                btnRecord.Enabled = true;
                gpbAudioInput.Enabled = true;
                waveIn.Dispose();
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            prbLeftChanel.Value = 0;
            prbRightChanel.Value = 0;
        }
    }
}

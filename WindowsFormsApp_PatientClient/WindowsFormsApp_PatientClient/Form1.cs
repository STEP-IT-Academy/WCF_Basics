using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;
using WindowsFormsApp_PatientClient.ServiceReference;

namespace WindowsFormsApp_PatientClient
{
    public partial class Form1 : Form
    {
        private class CallbackHandler : IPatientWriterCallback
        {
            private readonly DataGridView _dataGridView;
            private readonly Label _label;

            public CallbackHandler(DataGridView dataGridView, Label label) => (_dataGridView, _label) = (dataGridView, label);

            public void GetAllDoctorPatients(string doctorSurname, List<Patient> patients)
            {
                _label.Visible = true;
                _label.Text = $"Фамилия врача: {doctorSurname}. Список его пациентов:";

                _dataGridView.Visible = true;
                _dataGridView.DataSource = patients;
                _dataGridView.Columns[0].HeaderText = "Возраст";
                _dataGridView.Columns[1].HeaderText = "Фамилия";
            }
        }

        private static readonly DoctorsReaderClient DoctorsReaderClient = new DoctorsReaderClient();
        private static PatientWriterClient _patientWriterClient;
        private static readonly PatientDeterminingClient PatientDeterminingClient = new PatientDeterminingClient();

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = DoctorsReaderClient.ReadAllDoctorNames();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler(dataGridView1, label7));
            _patientWriterClient = new PatientWriterClient(instanceContext);

            await _patientWriterClient.AddPatientAsync(new Doctor { Surname = textBox2.Text }, new Patient { Surname = textBox1.Text, Age = (int)numericUpDown1.Value});
            comboBox1.DataSource = await DoctorsReaderClient.ReadAllDoctorNamesAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PatientDeterminingClient.IsAlreadyRecorded(new Doctor {Surname = comboBox1.SelectedItem.ToString()}, new Patient {Surname = textBox3.Text, Age = (int) numericUpDown2.Value}))
            {
                MessageBox.Show("Пациент уже записан к врачу.", "Результат проверки" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Пациент не записан к врачу.", "Результат проверки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
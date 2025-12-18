using login_page;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_Management_System
{
    public class appointments : Form
    {
        HospitalService service = new HospitalService();

        ComboBox cmbPatient, cmbDoctor;
        TextBox txtTime, txtSymptom;
        DateTimePicker dtDate;
        DataGridView grid;

        public appointments()
        {
            this.Text = "Appointments";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel header = new Panel()
            {
                BackColor = Color.FromArgb(0, 117, 124),
                Dock = DockStyle.Top,
                Height = 60
            };
            Label title = new Label()
            {
                Text = "Appointment Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(350, 20)
            };
            header.Controls.Add(title);
            Controls.Add(header);

            Label lblPatient = new Label() { Text = "Patient Name", Location = new Point(30, 90) };
            cmbPatient = new ComboBox()
            {
                Location = new Point(150, 90),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPatient.SelectedIndexChanged += CmbPatient_SelectedIndexChanged;

            Label lblSymptom = new Label() { Text = "Symptoms", Location = new Point(30, 130) };
            txtSymptom = new TextBox()
            {
                Location = new Point(150, 130),
                Width = 150,
                ReadOnly = true,
                Multiline = true,
                Height = 60
            };

            Label lblDoctor = new Label() { Text = "Doctor", Location = new Point(30, 210) };
            cmbDoctor = new ComboBox()
            {
                Location = new Point(150, 210),
                Width = 200, 
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblDate = new Label() { Text = "Date", Location = new Point(30, 250) };
            dtDate = new DateTimePicker()
            {
                Location = new Point(150, 250),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today
            };

            Label lblTime = new Label() { Text = "Time", Location = new Point(30, 290) };
            txtTime = new TextBox() { Location = new Point(150, 290), Width = 150 };

            Button btnAdd = CreateButton("Add", 30, 340, Color.FromArgb(0, 117, 124));
            btnAdd.Click += AddAppointment;

            Button btnDelete = CreateButton("Delete", 150, 340, Color.FromArgb(0, 117, 124));
            btnDelete.Click += DeleteAppointment;

            Button btnBack = CreateButton("Back", 30, 390, Color.Black);
            btnBack.Click += (s, e) =>
            {
                dashboard d = new dashboard();
                d.Show();
                this.Hide();
            };

            Button btnExit = CreateButton("Exit", 150, 390, Color.Black);
            btnExit.Click += (s, e) => Application.Exit();

            grid = new DataGridView()
            {
                Location = new Point(370, 90), 
                Size = new Size(500, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Controls.AddRange(new Control[]
            {
                lblPatient, cmbPatient,
                lblSymptom, txtSymptom,
                lblDoctor, cmbDoctor,
                lblDate, dtDate,
                lblTime, txtTime,
                btnAdd, btnDelete, btnBack, btnExit,
                grid
            });

            LoadPatients();
            LoadDoctors();
            LoadGrid();
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            return new Button()
            {
                Text = text,
                Location = new Point(x, y),
                Width = 100,
                Height = 35,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private void LoadPatients()
        {
            cmbPatient.Items.Clear();
            for (int i = 0; i < service.patientCount; i++)
            {
                cmbPatient.Items.Add(service.patientName[i]);
            }
            if (cmbPatient.Items.Count > 0) cmbPatient.SelectedIndex = 0;
        }

        private void LoadDoctors()
        {
            cmbDoctor.Items.Clear();
            for (int i = 0; i < service.doctorCount; i++)
            {
                string doctorDisplay = $"{service.doctorName[i]} ({service.doctorSpecialization[i]})";
                cmbDoctor.Items.Add(doctorDisplay);
            }
            if (cmbDoctor.Items.Count > 0) cmbDoctor.SelectedIndex = 0;
        }

        private void LoadGrid()
        {
            grid.Columns.Clear();
            grid.Rows.Clear();

            grid.Columns.Add("ID", "ID");
            grid.Columns.Add("Patient", "Patient");
            grid.Columns.Add("Symptoms", "Symptoms");
            grid.Columns.Add("Doctor", "Doctor");
            grid.Columns.Add("Date", "Date");
            grid.Columns.Add("Time", "Time");

            for (int i = 0; i < service.appointmentCount; i++)
            {
                grid.Rows.Add(
                    service.appointmentID[i],
                    service.appointmentPatientName[i],
                    service.appointmentPatientSymptom[i],
                    service.appointmentDoctorName[i],
                    service.appointmentDate[i],
                    service.appointmentTime[i]
                );
            }
        }

        private void CmbPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbPatient.SelectedIndex;
            if (index >= 0)
            {
                txtSymptom.Text = service.patientSymptom[index];
            }
        }

        private void AddAppointment(object sender, EventArgs e)
        {
            if (dtDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("You cannot book an appointment for a past date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPatient.SelectedIndex < 0 || cmbDoctor.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtTime.Text))
            {
                MessageBox.Show("Please fill all fields (Patient, Doctor, and Time).");
                return;
            }

            string patientName = cmbPatient.SelectedItem.ToString();

            string selectedDoctorInfo = cmbDoctor.SelectedItem.ToString();
            string doctorName = selectedDoctorInfo.Split('(')[0].Trim();

            service.AddAppointment(
                patientName,
                doctorName,
                dtDate.Value.ToShortDateString(),
                txtTime.Text,
                out _
            );

            LoadGrid();
            txtTime.Clear();
        }

        private void DeleteAppointment(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value);
            service.DeleteAppointment(id);
            LoadGrid();
        }
    }
}

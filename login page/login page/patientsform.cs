using login_page; // This connects to your HospitalService
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hospital_Management_System
{
    public class PatientsForm : Form
    {
        HospitalService hospital = new HospitalService();

        Label lblTitle, lblID, lblName, lblAge, lblSymptom;
        TextBox txtID, txtName, txtAge, txtSymptom;
        Button btnAdd, btnUpdate, btnDelete, btnClear, btnBack, btnExit;
        DataGridView dgvPatients;
        Panel headerPanel;

        public PatientsForm()
        {
            this.Text = "Patients Management";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            InitializeControls();
            LoadPatientsGrid();
        }

        private void InitializeControls()
        {
            headerPanel = new Panel();
            headerPanel.Size = new Size(this.Width, 70);
            headerPanel.BackColor = Color.FromArgb(0, 117, 124);
            headerPanel.Dock = DockStyle.Top;
            this.Controls.Add(headerPanel);

            lblTitle = new Label();
            lblTitle.Text = "Patients Management";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(350, 20);
            headerPanel.Controls.Add(lblTitle);

            lblID = new Label() { Text = "Patient ID", Location = new Point(50, 100) };
            lblName = new Label() { Text = "Name", Location = new Point(50, 150) };
            lblAge = new Label() { Text = "Age", Location = new Point(50, 200) };
            lblSymptom = new Label() { Text = "Symptoms", Location = new Point(50, 250) };

            this.Controls.Add(lblID);
            this.Controls.Add(lblName);
            this.Controls.Add(lblAge);
            this.Controls.Add(lblSymptom);

            txtID = new TextBox() { Location = new Point(150, 100), Width = 180, ReadOnly = true };
            txtName = new TextBox() { Location = new Point(150, 150), Width = 180 };
            txtAge = new TextBox() { Location = new Point(150, 200), Width = 180 };
            txtSymptom = new TextBox() { Location = new Point(150, 250), Size = new Size(180, 60), Multiline = true };

            this.Controls.Add(txtID);
            this.Controls.Add(txtName);
            this.Controls.Add(txtAge);
            this.Controls.Add(txtSymptom);

            Color btnColor = Color.FromArgb(0, 117, 124);
            btnAdd = CreateButton("Add", 50, 330, btnColor, BtnAdd_Click);
            btnUpdate = CreateButton("Update", 170, 330, btnColor, BtnUpdate_Click);
            btnDelete = CreateButton("Delete", 50, 380, btnColor, BtnDelete_Click);
            btnClear = CreateButton("Clear", 170, 380, btnColor, BtnClear_Click);
            btnBack = CreateButton("Back", 50, 440, Color.Black, BtnBack_Click);
            btnExit = CreateButton("Exit", 170, 440, Color.Black, BtnExit_Click);

            dgvPatients = new DataGridView();
            dgvPatients.Location = new Point(380, 100);
            dgvPatients.Size = new Size(580, 380);
            dgvPatients.ReadOnly = true;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.CellClick += DgvPatients_CellClick;

            dgvPatients.Columns.Add("ID", "Patient ID");
            dgvPatients.Columns.Add("Name", "Name");
            dgvPatients.Columns.Add("Age", "Age");
            dgvPatients.Columns.Add("Symptom", "Symptoms");

            this.Controls.Add(dgvPatients);
        }

        private Button CreateButton(string text, int x, int y, Color color, EventHandler click)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(100, 35);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Click += click;
            this.Controls.Add(btn);
            return btn;
        }

        private void LoadPatientsGrid()
        {
            dgvPatients.Rows.Clear();
            for (int i = 0; i < hospital.patientCount; i++)
            {
                dgvPatients.Rows.Add(
                    hospital.patientID[i],
                    hospital.patientName[i],
                    hospital.patientAge[i],
                    hospital.patientSymptom[i]
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Invalid Age");
                return;
            }

            int newID;
            if (hospital.RegisterPatient(txtName.Text, age, txtSymptom.Text, out newID))
            {
                MessageBox.Show("Patient added! ID: " + newID);
                LoadPatientsGrid();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to add patient");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int id, age;
            if (!int.TryParse(txtID.Text, out id) || !int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Select a patient from the list first");
                return;
            }

            if (hospital.UpdatePatient(id, txtName.Text, age, txtSymptom.Text))
            {
                MessageBox.Show("Patient updated successfully");
                LoadPatientsGrid();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Patient not found");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Select a patient from the list to delete");
                return;
            }

            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                hospital.DeletePatient(id);
                LoadPatientsGrid();
                ClearFields();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e) => ClearFields();

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Note: Ensure Dashboard class exists in your project
            dashboard d = new dashboard();
            d.Show();
            this.Hide();
        }

        private void BtnExit_Click(object sender, EventArgs e) => Application.Exit();

        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtSymptom.Clear();
        }

        private void DgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dgvPatients.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dgvPatients.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAge.Text = dgvPatients.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSymptom.Text = dgvPatients.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
        }
    }
}
using Hospital_Management_System;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace login_page
{
    public partial class DoctorForm : Form
    {
        HospitalService service = new HospitalService();

        TextBox txtName, txtSpec;
        DataGridView grid;
        Button btnAdd, btnDelete, btnBack, btnExit;

        public DoctorForm()
        {
            InitializeComponent(); // Must call this first

            // ===== HEADER =====
            Panel header = new Panel()
            {
                BackColor = Color.DarkGreen,
                Dock = DockStyle.Top,
                Height = 60
            };
            Label title = new Label()
            {
                Text = "Doctor Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15)
            };
            header.Controls.Add(title);
            Controls.Add(header);

            // ===== INPUTS =====
            Label lblName = new Label() { Text = "Doctor Name", Location = new Point(30, 90) };
            txtName = new TextBox() { Location = new Point(150, 90), Width = 150 };

            Label lblSpec = new Label() { Text = "Specialization", Location = new Point(30, 130) };
            txtSpec = new TextBox() { Location = new Point(150, 130), Width = 150 };

            // ===== BUTTONS =====
            btnAdd = CreateButton("Add", 30, 180, Color.FromArgb(0, 117, 124));
            btnAdd.Click += AddDoctor;

            btnDelete = CreateButton("Delete", 150, 180, Color.FromArgb(0, 117, 124));
            btnDelete.Click += DeleteDoctor;

            btnBack = CreateButton("Back", 30, 230, Color.Black);
            btnBack.Click += (s, e) =>
            {
                dashboard d = new dashboard();
                d.Show();
                this.Hide();
            };

            btnExit = CreateButton("Exit", 150, 230, Color.Black);
            btnExit.Click += (s, e) => Application.Exit();

            // ===== GRID =====
            grid = new DataGridView()
            {
                Location = new Point(350, 90),
                Size = new Size(500, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblSpec, txtSpec,
                btnAdd, btnDelete, btnBack, btnExit,
                grid
            });

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
                ForeColor = Color.White
            };
        }

        private void LoadGrid()
        {
            grid.Columns.Clear();
            grid.Rows.Clear();

            grid.Columns.Add("ID", "ID");
            grid.Columns.Add("Name", "Doctor Name");
            grid.Columns.Add("Spec", "Specialization");

            for (int i = 0; i < service.doctorCount; i++)
            {
                grid.Rows.Add(service.doctorID[i], service.doctorName[i], service.doctorSpecialization[i]);
            }
        }

        private void AddDoctor(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSpec.Text))
            {
                MessageBox.Show("Please enter Name and Specialization");
                return;
            }

            service.AddDoctor(txtName.Text, txtSpec.Text, out int newID);
            LoadGrid();
            txtName.Clear();
            txtSpec.Clear();
        }

        private void DeleteDoctor(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(grid.SelectedRows[0].Cells[0].Value);
            service.DeleteDoctor(id);
            LoadGrid();
        }

        private void DoctorForm_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Repositories;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        #region Fields
        #endregion

        #region Ctor
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new[] { "Aggregate", "Course", "Section" });
        }
        #endregion

        #region Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Adjusts width to fill available space
            }

            UnitOfWork.StudentRepository.Add("Nin", "Alamo", "41A1", "BSCS");

            RefreshDataGridView();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form = new();
            form.ShowDialog(this);

            RefreshDataGridView();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    var obj = dataGridView1.Rows[e.RowIndex].DataBoundItem;
                    Student? selectedStudent = UnitOfWork.StudentRepository.Get()
                        .FirstOrDefault(s => s.ID == (int)obj.GetType().GetProperty("ID").GetValue(obj));

                    if (selectedStudent == null)
                    {
                        MessageBox.Show("Invalid selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Form2 form = new(selectedStudent);
                        form.ShowDialog(this);
                        RefreshDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Methods
        private void RefreshDataGridView(IEnumerable<Student>? students = default, string filter = "Aggregate")
        {
            students = students ?? UnitOfWork.StudentRepository.Get() ?? Enumerable.Empty<Student>();

            if (filter == "Aggregate")
            {
                dataGridView1.DataSource = students
                    .Select(s => new StudentModel(s.ID, s.Name, s.Section ?? "Unknown Section", s.Course ?? "Unknown Course"))
                    .ToArray();
            }
            else if (filter == "Course")
            {
                var courseGroup = students
                    .GroupBy(s => s.Course)
                    .Select(a => new
                    {
                        Course = a.Key,
                        Count = a.Count()
                    }).ToArray();

                dataGridView1.DataSource = courseGroup;
            }
            else
            {
                var sectionGroup = students
                    .GroupBy(s => new { s.Section, s.Course })
                    .Select(a => new
                    {
                        Section = $"{a.Key.Course}-{a.Key.Section}",
                        Count = a.Count()
                    }).ToArray();

                dataGridView1.DataSource = sectionGroup;
            }
        }
        #endregion

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            var text = (sender as ComboBox)?.SelectedItem;

            var str = text?.ToString();
            RefreshDataGridView(filter: str ?? "Aggregate");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed events in this method if needed
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem?.ToString() == "Aggregate")
            {
                var searchFilter = textBox1.Text;
                var filteredStudents = UnitOfWork.StudentRepository.Get()
                    .Where(i => i.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase));

                RefreshDataGridView(filteredStudents.ToArray());
            }
            else
            {
                MessageBox.Show("lEO cALBO");
            }
        }

        record StudentModel(int ID, string Name, string Section, string Course);
    }
}

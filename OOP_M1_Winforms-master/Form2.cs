using System;
using System.Security.Policy;
using System.Windows.Forms;
using WinFormsApp1.Repositories;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private int? id = null;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Student student) : this()
        {
            id = student.ID;
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtSection.Text = student.Section;
            txtCourse.Text = student.Course;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (id.HasValue)
                {
                    // Update an existing student
                    UnitOfWork.StudentRepository.Update(
                        id.Value, txtFirstName.Text, txtLastName.Text, txtCourse.Text, txtSection.Text);
                }
                else
                {
                    // Add a new student
                    UnitOfWork.StudentRepository.Add(
                        txtFirstName.Text, txtLastName.Text, txtCourse.Text, txtSection.Text);
                }

                // Close the form after saving successfully
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      




    }
}
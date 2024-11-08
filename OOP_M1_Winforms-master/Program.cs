using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Open Form2 first
            using (Form2 form2 = new Form2())
            {
                // Show Form1 only if Form2's Next button is clicked
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }
            }
        }
    }
}
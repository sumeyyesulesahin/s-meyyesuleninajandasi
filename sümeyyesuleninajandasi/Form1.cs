using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace sümeyyesuleninajandasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Tiklanan;
        public int SelectedYear;
        public int SelectedMonth;

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("dd/MM/yyyy"); 
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button clickedButton= sender as Button;
            int selectedMonth=0;
            if (clickedButton != null)
            {
                Tiklanan = clickedButton.Text;
                int buttonIndex = Convert.ToInt32(clickedButton.Name.Split('n')[1]);

                
                selectedMonth = buttonIndex ;
                int selectedYear = DateTime.Today.Year; 
               
            }

            Form2 form2 = new Form2();
            form2.bilgi = Tiklanan;
            form2.selectedMonth = selectedMonth;
            form2.Show();

            
        }
    }
}

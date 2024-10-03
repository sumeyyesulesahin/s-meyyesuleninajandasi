using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace sümeyyesuleninajandasi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private System.Windows.Forms.TextBox[] textBoxArray = new System.Windows.Forms.TextBox[10];
        private System.Windows.Forms.CheckBox[] checkboxArray = new System.Windows.Forms.CheckBox[10];
        public void SaveToDatabase(int SIRANO, bool ISARET, string NOTE)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string query = "IF EXISTS (SELECT 1 FROM todolist WHERE SIRANO = @SIRANO) " +
                       "UPDATE todolist SET ISARET = @ISARET, NOTE = @NOTE WHERE SIRANO = @SIRANO " +
                       "ELSE INSERT INTO todolist (SIRANO, ISARET, NOTE) VALUES (@SIRANO, @ISARET, @NOTE)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SIRANO", SIRANO);
                    command.Parameters.AddWithValue("@ISARET", ISARET);
                    command.Parameters.AddWithValue("@NOTE", NOTE);
                    int rowsAffected = command.ExecuteNonQuery();

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Location = new System.Drawing.Point(114, 105);
            textBox2.Location = new System.Drawing.Point(114, 150);
            textBox3.Location = new System.Drawing.Point(114, 195);
            textBox4.Location = new System.Drawing.Point(114, 240);
            textBox5.Location = new System.Drawing.Point(114, 285);
            textBox6.Location = new System.Drawing.Point(114, 330);
            textBox7.Location = new System.Drawing.Point(114, 375);
            textBox8.Location = new System.Drawing.Point(114, 420);
            textBox9.Location = new System.Drawing.Point(114, 465);
            textBox10.Location = new System.Drawing.Point(114, 510);
            checkBox1.Location = new System.Drawing.Point(81, 110);
            checkBox2.Location = new System.Drawing.Point(81, 155);
            checkBox3.Location = new System.Drawing.Point(81, 200);
            checkBox4.Location = new System.Drawing.Point(81, 245);
            checkBox5.Location = new System.Drawing.Point(81, 290);
            checkBox6.Location = new System.Drawing.Point(81, 335);
            checkBox7.Location = new System.Drawing.Point(81, 380);
            checkBox8.Location = new System.Drawing.Point(81, 425);
            checkBox9.Location = new System.Drawing.Point(81, 470);
            checkBox10.Location = new System.Drawing.Point(81, 515);
            checkBox1.Tag = textBox1;
            checkBox2.Tag = textBox2;
            checkBox3.Tag = textBox3;
            checkBox4.Tag = textBox4;
            checkBox5.Tag = textBox5;
            checkBox6.Tag = textBox6;
            checkBox7.Tag = textBox7;
            checkBox8.Tag = textBox8;
            textBoxArray = new System.Windows.Forms.TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
            checkboxArray = new System.Windows.Forms.CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10 };

            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            string query = "SELECT SIRANO, ISARET, NOTE FROM todolist";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    int sirano = Convert.ToInt32(row["SIRANO"])-1;
                    bool isaret = Convert.ToBoolean(row["ISARET"]);
                    string note = row["NOTE"].ToString();

                        checkboxArray[sirano].Checked = isaret;
                        textBoxArray[sirano].Text = note;
                }
            }
        }
            private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxArray = new System.Windows.Forms.TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10 };
            checkboxArray = new System.Windows.Forms.CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10 };


            for (int i = 0; i < checkboxArray.Length; i++)
            {
                if (checkboxArray[i].Checked)
                {
                    int selectedIndex = i;
                    textBoxArray[i].Font = new Font(textBoxArray[i].Font, FontStyle.Strikeout);
                }
                else
                {
                    textBoxArray[i].Font = new Font(textBoxArray[i].Font, FontStyle.Regular);
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            SaveFormDataToDatabase();
            this.Close();
        }


        private void SaveFormDataToDatabase()
        {


            for (int i = 0; i < checkboxArray.Length; i++)
            {

                int sirano = i + 1;
                bool isaret = checkboxArray[i].Checked;
                string note = textBoxArray[i].Text;
                SaveToDatabase(sirano, isaret, note);



            }
        }
    }
}


   
    




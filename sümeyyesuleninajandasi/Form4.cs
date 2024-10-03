using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sümeyyesuleninajandasi
{
    public partial class Form4 : Form
    {
        public int SelectedMonth { get; set; }
        public int SelectedDay { get; set; }

        public Form4()
        {
            InitializeComponent();
        }

        public void LoadNotesFromDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            string query = "SELECT GUN, NOTE FROM takvim WHERE AY = @AY";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@AY", SelectedMonth);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Her gün için TextBox'ı bulup notu yükle
                foreach (DataRow row in dataTable.Rows)
                {
                    int gun = Convert.ToInt32(row["GUN"]);
                    string note = row["NOTE"].ToString();

                    // Gün numarasına göre TextBox'ı bul
                    string textBoxName = "textBox" + gun;
                    TextBox textBox = this.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == textBoxName);

                    if (textBox != null)
                    {
                        textBox.Text = note;
                    }
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadNotesFromDatabase();
        }
    }
}

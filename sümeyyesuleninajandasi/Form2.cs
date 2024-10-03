using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace sümeyyesuleninajandasi
{


    public partial class Form2 : Form
    {
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        public Form2()
        {
            InitializeComponent();

        }
        public string GetDayName(int day)
        {
            DateTime date = new DateTime(DateTime.Today.Year, selectedMonth, day);
            return date.DayOfWeek.ToString();
        }
        public string bilgi;
        public int selectedMonth = 0;
        private Dictionary<Button, TextBox> buttonTextBoxes = new Dictionary<Button, TextBox>();

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = bilgi;
            int a = 0;
           
            Point currentLocation = new Point(100, 100);
            DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            int firstDayOffset = (int)firstDayOfMonth.DayOfWeek;
            int buttonsayisi = DateTime.DaysInMonth(DateTime.Today.Year, selectedMonth);
            if (firstDayOffset == 0)
            {
                firstDayOffset = 6;
            }
            else
            {
                firstDayOffset--;
            }
            a = firstDayOffset;

            for (int i = 0; i < buttonsayisi; i++)
            {
                Button button = new Button();
                button.Width = 40;
                button.Height = 30;
                button.Text = (i + 1).ToString();
                button.Visible = true;
                this.Controls.Add(button);

                if (a == 7)
                {
                    a = 0;
                    button.Location = new Point(100, currentLocation.Y + 30);
                }
                else
                {
                    button.Location = new Point(100 + (a * 40), currentLocation.Y);
                }
                a++;
                currentLocation = button.Location;
                button.Click += new EventHandler(DayButton_Click);
            }
            Button notlarButton = new Button();
            notlarButton.Text = "Notlar";
            notlarButton.Location = new Point(300, 300);
            notlarButton.Width = 80;
            notlarButton.Height = 50;
            notlarButton.Click += NotlarButton_Click;
            this.Controls.Add(notlarButton);
        }
        private void NotlarButton_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }
        private void DayButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                foreach (var tb in buttonTextBoxes.Values)
                {
                    tb.Visible = false;
                }

                TextBox noteTextBox;

                if (buttonTextBoxes.ContainsKey(clickedButton))
                {
                    noteTextBox = buttonTextBoxes[clickedButton];
                    noteTextBox.Visible = true;
                }
                else
                {
                    noteTextBox = new TextBox();
                    noteTextBox.Location = new Point(clickedButton.Location.X, clickedButton.Location.Y + 35);
                    noteTextBox.Visible = true;
                    noteTextBox.Width = 200;
                    noteTextBox.Height = 30;

                    this.Controls.Add(noteTextBox);
                    noteTextBox.BringToFront();
                    noteTextBox.Focus();
                    buttonTextBoxes[clickedButton] = noteTextBox;
                    noteTextBox.TextChanged += (s, args) =>
                    {
                        if (!string.IsNullOrEmpty(noteTextBox.Text))
                        {
                            clickedButton.Text = $"{clickedButton.Text.Split(' ')[0]} *";
                        }
                        else
                        {
                            clickedButton.Text = (clickedButton.TabIndex + 1).ToString();
                        }
                    };
                }

                Button saveButton = new Button();
                saveButton.Text = "Kaydet";
                saveButton.Location = new Point(100, 300);
                saveButton.Visible = true;
                saveButton.Width = 80;
                saveButton.Height = 50; this.Controls.Add(saveButton);
                noteTextBox.BringToFront();
                noteTextBox.Focus();
                saveButton.Click += (s, args) =>
                {
                    string note = noteTextBox.Text;
                    SaveNoteToDatabase(selectedMonth.ToString(), int.Parse(clickedButton.Text.ToString().Split('*')[0]), GetDayName(int.Parse(clickedButton.Text.ToString().Split('*')[0])), note);
                    MessageBox.Show("Not kaydedildi: " + note);
                    foreach (var tb in buttonTextBoxes.Values)
                    {
                        tb.Visible = false;
                    }

                    saveButton.Visible = false;

                };

            }
        }
    
        


        private void SaveNoteToDatabase(string v1, int v2, Func<int, string> getDayName, string note)
        {
            throw new NotImplementedException();
        }
        private void SaveNoteToDatabase(string AY,int GUN,string GUNADI,string NOTE)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Takvim (AY, GUN,GUNADI, NOTE) VALUES (@AY, @GUN,@GUNADI, @NOTE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@AY", AY);
                cmd.Parameters.AddWithValue("@GUN", GUN);
                cmd.Parameters.AddWithValue("@GUNADI", GUNADI);
                cmd.Parameters.AddWithValue("@NOTE", NOTE);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

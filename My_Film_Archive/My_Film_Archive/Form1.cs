using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace My_Film_Archive
{
    public partial class TxtFilmName : Form
    {
        public TxtFilmName()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=MyFilmArchive;Integrated Security=True");
           

        void filmler()
        {
            SqlDataAdapter da=new SqlDataAdapter("Select * from TBLFILMs",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            filmler();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFILMS (NAME,CATEGORY, LINK) values (@P1,@P2,@P3)", baglanti);  
            komut.Parameters.AddWithValue("@P1", TxtName.Text);
            komut.Parameters.AddWithValue("@P2", TxtCategory.Text);
            komut.Parameters.AddWithValue("@P3", TxtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Added to your film list", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            filmler();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string link=dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            webBrowser1.Navigate(link);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

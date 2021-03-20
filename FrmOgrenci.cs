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

namespace BonusOkulProje
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-DQNGF2TH\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cmbogrkulup.DisplayMember = "KULUPAD";
            Cmbogrkulup.ValueMember = "KULUPID";
            Cmbogrkulup.DataSource = dt;
            baglanti.Close();
        }
        string c = "";
        private void btnekle_Click(object sender, EventArgs e)
        {
            if (rdberkek.Checked==true)
            {
                c = "ERKEK";
            }
            if (rdbkız.Checked==true)
            {
                c = "KIZ";
            }
            ds.OgrenciEkle(Txtograd.Text, Txtogrsoyad.Text, byte.Parse(Cmbogrkulup.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci Ekleme Tamamlandı.");
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void Cmbogrkulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Txtogrıd.Text = Cmbogrkulup.SelectedValue.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(Txtogrıd.Text));
            MessageBox.Show("Silme İşlemi Tamamlandı.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Txtogrıd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Txtograd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Txtogrsoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(Txtograd.Text, Txtogrsoyad.Text, byte.Parse(Cmbogrkulup.SelectedValue.ToString()), c,int.Parse(Txtogrıd.Text));
            MessageBox.Show("Güncelleme İşlemi Tamamlandı");
        }

        private void rdbkız_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbkız.Checked == true)
            {
                c = "KIZ";
            }
        }

        private void rdberkek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdberkek.Checked == true)
            {
                c = "ERKEK";
            }
        }

        private void btnara_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(Txtara.Text);
        }
    }
}

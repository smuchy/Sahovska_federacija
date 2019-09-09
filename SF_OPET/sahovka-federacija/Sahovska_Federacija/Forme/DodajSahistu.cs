using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Sahovska_Federacija.Entiteti;
using Sahovska_Federacija.Forme;

namespace Sahovska_Federacija.Forme
{
    public partial class DodajSahistu : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public string tip;
        public bool jeste;
        public DodajSahistu()
        {
            this.jeste = false;
            InitializeComponent();
        }

        private void createLabels1()
        {
            flowLayoutPanel1.Controls.Clear();
            Label l1 = new Label();
            l1.Name = "lblPartije";
            l1.Text = "Broj odigranih partija: ";
            l1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(l1);
            TextBox t1 = new TextBox();
            t1.Name = "txtPartije";
            flowLayoutPanel1.Controls.Add(t1);
            Label l2 = new Label();
            l2.Name = "lblZvanje";
            l2.Text = "Broj partija do zvanja: ";
            l2.AutoSize = true;
            flowLayoutPanel1.Controls.Add(l2);
            TextBox t2 = new TextBox();
            t2.Name = "txtZvanje";
            flowLayoutPanel1.Controls.Add(t2);
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            createLabels1();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Label l = new Label();
            l.Name = "lblSticanje";
            l.Text = "Datum sticanja zvanja";
            l.AutoSize = true;
            flowLayoutPanel1.Controls.Add(l);
            TextBox t = new TextBox();
            t.Name = "txtSticanje";
            flowLayoutPanel1.Controls.Add(t);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                ISession s = DataLayer.GetSession();
                Sahovska_Federacija.Entiteti.Federacija f = s.Load<Sahovska_Federacija.Entiteti.Federacija>(1);

                if (radioButton1.Checked)
                {
                    Obican_clan obican = new Obican_clan();

                    obican.br_pasosa = Int32.Parse(textBox1.Text);
                    obican.tip = "obican_clan";
                    obican.ime = textBox2.Text;
                    obican.prezime = textBox3.Text;
                    obican.ulica = textBox4.Text;
                    obican.broj = Int32.Parse(textBox5.Text);
                    obican.datum_rodjenja = textBox6.Text;
                    obican.zemlja_porekla = textBox7.Text;


                    obican.ClanFederacije = f;
                    s.Save(obican);
                    f.Sahisti.Add(obican);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste dodali sahistu!");
                }
                if(radioButton2.Checked)
                {
                    Majstorski_kandidat m = new Majstorski_kandidat();
                    TextBox text1 = flowLayoutPanel1.Controls.Find("txtPartije", false).First() as TextBox;
                    TextBox text2 = flowLayoutPanel1.Controls.Find("txtZvanje", false).First() as TextBox;

                    m.br_pasosa = Int32.Parse(textBox1.Text);
                    m.ime = textBox2.Text;
                    m.prezime = textBox3.Text;
                    m.ulica = textBox4.Text;
                    m.broj = Int32.Parse(textBox5.Text);
                    m.datum_rodjenja = textBox6.Text;
                    m.zemlja_porekla = textBox7.Text;
                    m.br_odigranih_partija = Int32.Parse(text1.Text);
                    m.br_partija_do_zvanja = Int32.Parse(text2.Text);
                    m.tip = "majstorski_kandidat";

                    m.ClanFederacije = f;
                    s.Save(m);
                    f.Sahisti.Add(m);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste dodali sahistu!");
                }
                if(radioButton3.Checked)
                {
                    Majstor m = new Majstor();
                    TextBox text1 = flowLayoutPanel1.Controls.Find("txtSticanje", false).First() as TextBox;

                    m.br_pasosa = Int32.Parse(textBox1.Text);
                    m.tip = "majstor";
                    m.ime = textBox2.Text;
                    m.prezime = textBox3.Text;
                    m.ulica = textBox4.Text;
                    m.broj = Int32.Parse(textBox5.Text);
                    m.datum_rodjenja = textBox6.Text;
                    m.zemlja_porekla = textBox7.Text;
                    m.datum_sticanja_zvanja = text1.Text;


                    m.ClanFederacije = f;
                    s.Save(m);

                    f.Sahisti.Add(m);

                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste dodali sahistu!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DodajSahistu_Load(object sender, EventArgs e)
        {

        }
    }
}

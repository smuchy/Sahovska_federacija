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
    public partial class OrganizujTurnir : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public bool jeste;


        public OrganizujTurnir()
        {
            this.jeste = false;
            InitializeComponent();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Label l = new Label();
            l.Name = "lblMaxPartije";
            l.Text = "Maksimalno vreme trajanja partija";
            l.AutoSize = true;
            flowLayoutPanel1.Controls.Add(l);
            TextBox t = new TextBox();
            t.Name = "txtMaxPartije";
            flowLayoutPanel1.Controls.Add(t);
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Label l1 = new Label();
            l1.Name = "lblKome";
            l1.Text = "Kome je turnir namenjen";
            l1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(l1);
            TextBox t1 = new TextBox();
            t1.Name = "txtKome";
            flowLayoutPanel1.Controls.Add(t1);
            Label l2 = new Label();
            l2.Name = "lblIznos";
            l2.Text = "Prikupljeni iznos";
            flowLayoutPanel1.Controls.Add(l2);
            TextBox t2 = new TextBox();
            t2.Name = "txtIznos";
            flowLayoutPanel1.Controls.Add(t2);
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            RadioButton r1 = new RadioButton();
            r1.Name = "radioButton7";
            r1.Text = "Nacionalni";
            flowLayoutPanel1.Controls.Add(r1);
            RadioButton r2 = new RadioButton();
            r2.Name = "radioButton8";
            r2.Text = "Regionalni";
            flowLayoutPanel1.Controls.Add(r2);
            RadioButton r3 = new RadioButton();
            r3.Name = "radioButton9";
            r3.Text = "Internacionalni";
            flowLayoutPanel1.Controls.Add(r3);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Sahovska_Federacija.Entiteti.Federacija f = s.Load<Sahovska_Federacija.Entiteti.Federacija>(1);

                if(radioButton1.Checked)
                {
                    Brzopotezni b = new Brzopotezni();
                    TextBox text1 = flowLayoutPanel1.Controls.Find("txtMaxPartije", false).First() as TextBox;

                    b.naziv = textBox1.Text;
                    b.zemlja = textBox2.Text;
                    b.grad = textBox3.Text;
                    b.god_odrzavanja = Int32.Parse(textBox4.Text);
                    b.max_vreme_trajanja_partija = Int32.Parse(text1.Text);
                    b.tip = "brzopotezni";
                    b.odigran = "N";

                    b.Je_pokrovitelj = f;
                    s.Save(b);
                    f.Sahovski_turniri.Add(b);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
                if(radioButton2.Checked)
                {
                    Egzibicioni eg = new Egzibicioni();

                    eg.naziv = textBox1.Text;
                    eg.zemlja = textBox2.Text;
                    eg.grad = textBox3.Text;
                    eg.god_odrzavanja = Int32.Parse(textBox4.Text);
                    eg.tip = "egzibicioni";
                    eg.odigran = "N";

                    eg.Je_pokrovitelj = f;
                    s.Save(eg);
                    f.Sahovski_turniri.Add(eg);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
                if(radioButton3.Checked)
                {
                    Humanitarni h = new Humanitarni();
                    TextBox text1 = flowLayoutPanel1.Controls.Find("txtKome", false).First() as TextBox;
                    TextBox text2 = flowLayoutPanel1.Controls.Find("txtIznos", false).First() as TextBox;

                    h.naziv = textBox1.Text;
                    h.zemlja = textBox2.Text;
                    h.grad = textBox3.Text;
                    h.god_odrzavanja = Int32.Parse(textBox4.Text);
                    h.tip = "humanitarni";
                    h.odigran = "N";
                    h.kome_je_namenjen = text1.Text;
                    h.prikupljeni_iznos = Int32.Parse(text2.Text);

                    h.Je_pokrovitelj = f;
                    s.Save(h);
                    f.Sahovski_turniri.Add(h);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
                if(radioButton4.Checked)
                {
                    Promotivni p = new Promotivni();

                    p.naziv = textBox1.Text;
                    p.zemlja = textBox2.Text;
                    p.grad = textBox3.Text;
                    p.god_odrzavanja = Int32.Parse(textBox4.Text);
                    p.tip = "promotivni";
                    p.odigran = "N";

                    p.Je_pokrovitelj = f;
                    s.Save(p);
                    f.Sahovski_turniri.Add(p);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
                if(radioButton5.Checked)
                {
                    Takmicarski t = new Takmicarski();
                    RadioButton nacionalni = flowLayoutPanel1.Controls.Find("radioButton7", false).First() as RadioButton;
                    RadioButton regionalni = flowLayoutPanel1.Controls.Find("radioButton8", false).First() as RadioButton;
                    RadioButton internacionalni = flowLayoutPanel1.Controls.Find("radioButton9", false).First() as RadioButton;

                    t.naziv = textBox1.Text;
                    t.zemlja = textBox2.Text;
                    t.grad = textBox3.Text;
                    t.god_odrzavanja = Int32.Parse(textBox4.Text);
                    t.tip = "takmicarski";
                    t.odigran = "N";
                    if(nacionalni.Checked)
                    {
                        t.nacionalni = "Y";
                        t.regionalni = "N";
                        t.internacionalni = "N";
                    }
                    if(regionalni.Checked)
                    {
                        t.nacionalni = "N";
                        t.regionalni = "Y";
                        t.internacionalni = "N";
                    }
                    if(internacionalni.Checked)
                    {
                        t.nacionalni = "N";
                        t.regionalni = "N";
                        t.internacionalni = "Y";
                    }
                    t.Je_pokrovitelj = f;
                    s.Save(t);
                    f.Sahovski_turniri.Add(t);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
                if(radioButton6.Checked)
                {
                    Normalni n = new Normalni();

                    n.naziv = textBox1.Text;
                    n.zemlja = textBox2.Text;
                    n.grad = textBox3.Text;
                    n.god_odrzavanja = Int32.Parse(textBox4.Text);
                    n.tip = "normalni";
                    n.odigran = "N";

                    n.Je_pokrovitelj = f;
                    s.Save(n);
                    f.Sahovski_turniri.Add(n);
                    s.Save(f);
                    this.jeste = true;
                    MessageBox.Show("Uspesno ste organizovali turnir!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrganizujTurnir_Load(object sender, EventArgs e)
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
    }
}

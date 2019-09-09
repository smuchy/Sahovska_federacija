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
    public partial class DodajPoteze : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int turnirId;
        public DodajPoteze()
        {
            InitializeComponent();
        }
        public DodajPoteze(int id)
        {
            this.turnirId = id;
            InitializeComponent();
        }

        private void DodajPoteze_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView1.Items.Clear();
            List<PartijePregledBezIshoda> odInfos = DTOManager.getInfosPartijeBezIshoda(this.turnirId);
            foreach (PartijePregledBezIshoda pp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { pp.id_partije.ToString(), pp.bele_figure, pp.crne_figure, pp.kad_se_igra, pp.trajanje.ToString() });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            RadioButton r1 = new RadioButton();
            r1.Name = "radioButton1";
            r1.Text = "Pat";
            flowLayoutPanel1.Controls.Add(r1);
            RadioButton r2 = new RadioButton();
            r2.Name = "radioButton2";
            r2.Text = "Mat";
            flowLayoutPanel1.Controls.Add(r2);
            RadioButton r3 = new RadioButton();
            r3.Name = "radioButton3";
            r3.Text = "Rem";
            flowLayoutPanel1.Controls.Add(r3);
            Button b = new Button();
            b.Name = "button3";
            b.Text = "Zavrsi";
            b.Click += new EventHandler(Button3_Click);
            flowLayoutPanel1.Controls.Add(b);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RadioButton r1 = flowLayoutPanel1.Controls.Find("radioButton1", false).First() as RadioButton;
            RadioButton r2 = flowLayoutPanel1.Controls.Find("radioButton2", false).First() as RadioButton;
            RadioButton r3 = flowLayoutPanel1.Controls.Find("radioButton3", false).First() as RadioButton;
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite partiju!");
                return;
            }
            int partijaId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Partija partija = s.Load<Sahovska_Federacija.Entiteti.Partija>(partijaId);
            if (r1.Checked)
            {
                partija.pat = "Y";
                partija.mat = "N";
                partija.rem = "N";
                s.Update(partija);
                s.Flush();
                MessageBox.Show("Partija je zavrsena sa ishodom pat");
            }
            if(r2.Checked)
            {
                partija.pat = "N";
                partija.mat = "Y";
                partija.rem = "N";
                s.Update(partija);
                s.Flush();
                MessageBox.Show("Partija je zavrsena sa ishodom mat");
            }
            if(r3.Checked)
            {
                partija.pat = "N";
                partija.mat = "N";
                partija.rem = "Y";
                s.Update(partija);
                s.Flush();
                MessageBox.Show("Partija je zavrsena sa ishodom rem");
            }
            s.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

            int partijaId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Partija partija = s.Load<Sahovska_Federacija.Entiteti.Partija>(partijaId);
            Potez potez = new Potez();

            potez.opis = richTextBox1.Text;
            potez.kad_odigran = textBox1.Text;

            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite partiju!");
                return;
            }

            potez.je_odigran = partija;
            s.Save(potez);
            partija.Potezi.Add(potez);
            s.Save(partija);
            MessageBox.Show("Uspesno ste dodali potez!");
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

        private void Button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

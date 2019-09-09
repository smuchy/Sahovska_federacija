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
    public partial class DodajPartiju : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int turnirId;
        public string beleIme;
        public string belePrezime;
        public string crneIme;
        public string crnePrezime;
        public DodajPartiju()
        {
            InitializeComponent();
        }
        public DodajPartiju(int id)
        {
            this.turnirId = id;
            this.beleIme = "";
            this.belePrezime = "";
            this.crneIme = "";
            this.crnePrezime = "";
            InitializeComponent();
        }

        private void DodajPartiju_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }
        private void populateInfos()
        {
            listView1.Items.Clear();
            List<SahistiPregled> odInfos = DTOManager.getInfosSahisti(1);
            foreach (SahistiPregled sp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { sp.registracioni_broj_sahiste.ToString(), sp.ime, sp.prezime, sp.zemlja_porekla, sp.tip });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite sahistu!");
                return;
            }
            int sahistaId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Sahista sh = s.Load<Sahovska_Federacija.Entiteti.Sahista>(sahistaId);
            this.beleIme = sh.ime;
            this.belePrezime = sh.prezime;
            MessageBox.Show(this.beleIme + " " + this.belePrezime + " ce igrati belim figurama!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite sahistu!");
                return;
            }
            int sahistaId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Sahista sh = s.Load<Sahovska_Federacija.Entiteti.Sahista>(sahistaId);
            this.crneIme = sh.ime;
            this.crnePrezime = sh.prezime;
            MessageBox.Show(this.crneIme + " " + this.crnePrezime + " ce igrati crnim figurama!");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(this.beleIme=="" || this.belePrezime=="")
            {
                MessageBox.Show("Izaberite sahistu za bele figure!");
                return;
            }
            if(this.crneIme == "" || this.crnePrezime == "")
            {
                MessageBox.Show("Izaberite sahistu za crne figure!");
                return;
            }
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Sahovski_turnir st = s.Load<Sahovska_Federacija.Entiteti.Sahovski_turnir>(this.turnirId);
            Partija partija = new Partija();

            partija.bele = this.beleIme + " " + this.belePrezime;
            partija.crne = this.crneIme + " " + this.crnePrezime;
            partija.kad_se_igra = textBox1.Text;
            partija.trajanje = Int32.Parse(textBox2.Text);
            partija.pat = "N";
            partija.mat = "N";
            partija.rem = "N";

            partija.IgraSe = st;
            s.Save(partija);
            st.Partije.Add(partija);
            s.Save(st);
            MessageBox.Show("Uspesno ste organizovali partiju!");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DodajPoteze forma = new DodajPoteze(this.turnirId);
            forma.ShowDialog();
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

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

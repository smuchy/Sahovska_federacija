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
    public partial class IzaberiSponzora : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public TurnirBasic tBasic;


        public IzaberiSponzora()
        {
            InitializeComponent();
        }

        public IzaberiSponzora(TurnirBasic sb)
        {
            this.tBasic = sb;
            InitializeComponent();
        }

        private void IzaberiSponzora_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        public void populateInfos()
        {
            listView1.Items.Clear();
            List<SponzoriPregled> odInfos = DTOManager.getInfosSponzori();
            foreach (SponzoriPregled sp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { sp.reg_broj.ToString(), sp.ime, sp.opis, sp.logo });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite redni broj sponzora koga zelite dodati turniru!");
                return;
            }

            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SponzorBasic sb = DTOManager.GetSponzorBasic(odId);
            try
            {
                ISession s = DataLayer.GetSession();
                Sahovska_Federacija.Entiteti.Sahovski_turnir t = s.Load<Sahovska_Federacija.Entiteti.Sahovski_turnir>(tBasic.turnirId);
                Sahovska_Federacija.Entiteti.Sponzor sp = s.Load<Sahovska_Federacija.Entiteti.Sponzor>(sb.reg_broj);

                Je_sponzor js = new Je_sponzor();

                js.SponzoriseTurnir = t;
                js.SponzorJe = sp;

                s.Save(js);
                MessageBox.Show("Uspesno ste dodali sponzora na odabranom turniru!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using NHibernate;
using Sahovska_Federacija.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate.Linq;
using Sahovska_Federacija.Forme;

namespace Sahovska_Federacija.Forme
{
    public partial class SponzoriInformacije : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public SponzoriInformacije()
        {
            InitializeComponent();
        }

        private void SponzoriInformacije_Load(object sender, EventArgs e)
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

        private void Button5_Click(object sender, EventArgs e)
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

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DodajSponzora dodajSponzora = new DodajSponzora();
            dodajSponzora.ShowDialog();
            if (dodajSponzora.jeste == true)
            {
                populateInfos();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite sponzora!");
                return;
            }
            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SponzorBasic sb = DTOManager.GetSponzorBasic(odId);

            IzmeniSponzora sbForm = new IzmeniSponzora(sb);
            if (sbForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DTOManager.UpdateSponzorBasic(sbForm.tBasic);
                populateInfos();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Odaberite sponzora!");
                    return;
                }
                int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                Sponzor sh = s.Load<Sponzor>(odId);
                s.Delete(sh);

                s.Flush();
                s.Close();
                MessageBox.Show("Uspesno ste obrisali spoznora! ");
                populateInfos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    public partial class SahistiInformacije : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public int federacijaBroj { get; set; }
        public SahistiInformacije()
        {
            InitializeComponent();
        }
        public SahistiInformacije(int fb)
        {
            this.federacijaBroj = fb;
            InitializeComponent();
        }


        private void SahistiInformacije_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView2.Items.Clear();
            List<SahistiPregled> odInfos = DTOManager.getInfosSahisti(this.federacijaBroj);
            foreach(SahistiPregled sp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { sp.registracioni_broj_sahiste.ToString(), sp.ime, sp.prezime, sp.zemlja_porekla, sp.tip });
                listView2.Items.Add(item);
            }
            listView2.Refresh();
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            DodajSahistu dodajSahistu = new DodajSahistu();
            dodajSahistu.ShowDialog();
            if (dodajSahistu.jeste == true)
            {
                populateInfos();
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if(listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite sahistu");
                return;
            }
            int odId = Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            SahistaBasic sb = DTOManager.GetSahistaBasic(odId);

            SahistaEditBasic sbForm = new SahistaEditBasic(sb);
            if (sbForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DTOManager.UpdateSahistaBasic(sbForm.tBasic);
                populateInfos();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                if (listView2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Odaberite sahistu");
                    return;
                }
                int odId = Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text);
                Sahista sh = s.Load<Sahista>(odId);
                s.Delete(sh);

                s.Flush();
                s.Close();
                MessageBox.Show("Uspesno ste obrisali sahistu");
                populateInfos();
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

        private void Button6_Click(object sender, EventArgs e)
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

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

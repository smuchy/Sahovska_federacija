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
    public partial class OrganizatoriHome : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public OrganizatoriHome()
        {
            InitializeComponent();
        }

        private void OrganizatoriHome_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        public void populateInfos()
        {
            listView1.Items.Clear();
            List<OrganizatoriHomePregled> odInfos = DTOManager.getInfosOrganizatoriHome();
            foreach (OrganizatoriHomePregled sp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { sp.maticni_broj.ToString(), sp.ime, sp.prezime, sp.adresa });
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

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite organizatora!");
                return;
            }
            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            OrganizatoriHomeBasic sb = DTOManager.GetOrganizatoriHomeBasic(odId);

            IzmeniOrganizatora sbForm = new IzmeniOrganizatora(sb);
            if (sbForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DTOManager.UpdateOrganizatoriHomeBasic(sbForm.tBasic);
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
                    MessageBox.Show("Odaberite organizatora!");
                    return;
                }
                int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                Organizator sh = s.Load<Organizator>(odId);
                s.Delete(sh);

                s.Flush();
                s.Close();
                MessageBox.Show("Uspesno ste obrisali organizatora! ");
                populateInfos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}

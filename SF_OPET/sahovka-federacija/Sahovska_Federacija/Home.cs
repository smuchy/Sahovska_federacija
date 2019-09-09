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

namespace Sahovska_Federacija
{
    
    public partial class Home : Form
    {
      
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public Home()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SahistiInformacije odInfoForm = new SahistiInformacije(1);
            odInfoForm.Show();
            panelLeft.Height = button1.Height;
            panelLeft.Top = button1.Top;
        }

        private void ToolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TurniriInformacije odInfoForm = new TurniriInformacije(1);
            odInfoForm.Show();
            panelLeft.Height = button2.Height;
            panelLeft.Top = button2.Top;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SponzoriInformacije odInfoForm = new SponzoriInformacije();
            odInfoForm.Show();

            panelLeft.Height = button3.Height;
            panelLeft.Top = button3.Top;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OrganizatoriHome odInfoForm = new OrganizatoriHome();
            odInfoForm.Show();

            panelLeft.Height = button4.Height;
            panelLeft.Top = button4.Top;
        }

        private void Panel3_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Home_Move(object sender, EventArgs e)
        {
            
        }

        private void Panel3_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
             this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            OrganizujTurnir forma = new OrganizujTurnir();
            forma.ShowDialog();
            if(forma.jeste == true)
            {
                populateInfos();
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            int turnirId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            DodajPartiju forma = new DodajPartiju(turnirId);
            forma.ShowDialog();
            
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }
        private void populateInfos()
        {
            listView1.Items.Clear();
            List<TurnirPregled> odInfos = DTOManager.getInfosTurniriHome(1);
            foreach (TurnirPregled tp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { tp.turnirId.ToString(), tp.naziv, tp.grad, tp.zemlja, tp.god_odrzavanja.ToString(), tp.tip, tp.brojPartija.ToString() });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }

        private void Button10_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir kome zelite dodati partiju!");
                return;
            }

            int turnirId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            DodajPartiju forma = new DodajPartiju(turnirId);
            forma.ShowDialog();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir kome zelite dodati sponzora!");
                return;
            }
            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            TurnirBasic sb = DTOManager.GetTurnirBasic(odId);

            IzaberiSponzora sbForm = new IzaberiSponzora(sb);
            sbForm.ShowDialog();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir kome zelite dodati organizatora!");
                return;
            }
            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            TurnirBasic sb = DTOManager.GetTurnirBasic(odId);

            IzaberiOrganizatora sbForm = new IzaberiOrganizatora(sb);
            sbForm.ShowDialog();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir koji zelite zavrsiti!");
                return;
            }
            int odId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            ISession s = DataLayer.GetSession();
            Sahovska_Federacija.Entiteti.Sahovski_turnir turnir = s.Load<Sahovska_Federacija.Entiteti.Sahovski_turnir>(odId);

            turnir.odigran = "Y";

            s.Update(turnir);
            s.Flush();
            s.Close();
            this.populateInfos();
            MessageBox.Show("Turnir je zavrsen!");
        }
    }

}


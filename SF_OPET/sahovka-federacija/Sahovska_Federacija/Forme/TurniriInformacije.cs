using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sahovska_Federacija.Forme
{
    public partial class TurniriInformacije : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int federacijaBroj { get; set; }
        public TurniriInformacije()
        {
            InitializeComponent();
        }
        public TurniriInformacije(int fb)
        {
            this.federacijaBroj = fb;
            InitializeComponent();
        }

        private void TurniriInformacije_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView1.Items.Clear();
            List<TurnirPregled> odInfos = DTOManager.getInfos(this.federacijaBroj);
            foreach(TurnirPregled tp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { tp.turnirId.ToString(), tp.naziv, tp.grad, tp.zemlja, tp.god_odrzavanja.ToString(), tp.tip, tp.brojPartija.ToString() });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir!");
                return;
            }
            int turnirId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            OrganizatorInformacije orgForma = new OrganizatorInformacije(turnirId);
            orgForma.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir!");
                return;
            }
            int turnirId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            SponzoriInformacijeTurnir forma = new SponzoriInformacijeTurnir(turnirId);
            forma.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite turnir!");
                return;
            }
            int turnirId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            PartijeInformacije forma = new PartijeInformacije(turnirId);
            forma.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
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

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

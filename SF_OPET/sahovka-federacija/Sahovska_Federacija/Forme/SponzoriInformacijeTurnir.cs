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
    public partial class SponzoriInformacijeTurnir : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int turnirId;
        public SponzoriInformacijeTurnir()
        {
            InitializeComponent();
        }
        public SponzoriInformacijeTurnir(int id)
        {
            this.turnirId = id;
            InitializeComponent();
        }

        private void SponzoriInformacijeTurnir_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView1.Items.Clear();
            List<SponzoriPregled> odInfos = DTOManager.getInfosSponzoriTurnir(this.turnirId);
            foreach (SponzoriPregled sp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { sp.reg_broj.ToString(), sp.ime, sp.opis, sp.logo });
                listView1.Items.Add(item);
            }
            listView1.Refresh();
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

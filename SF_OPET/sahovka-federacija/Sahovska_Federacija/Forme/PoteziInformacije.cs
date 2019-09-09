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
    public partial class PoteziInformacije : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int partijaId;
        public PoteziInformacije()
        {
            InitializeComponent();
        }

        public PoteziInformacije(int partijaId)
        {
            this.partijaId = partijaId;
            InitializeComponent();
        }

        private void PoteziInformacije_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView1.Items.Clear();
            List<PoteziPregled> odInfos = DTOManager.getInfosPoteziTurnir(this.partijaId);
            foreach (PoteziPregled pp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { pp.redni_br.ToString(), pp.opis, pp.kad_odigran });
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
    }
}

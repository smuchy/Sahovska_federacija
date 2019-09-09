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
    public partial class PartijeInformacije : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public int turnirId;
        public PartijeInformacije()
        {
            InitializeComponent();
        }

        public PartijeInformacije(int id)
        {
            this.turnirId = id;
            InitializeComponent();
        }

        private void PartijeInformacije_Load(object sender, EventArgs e)
        {
            this.populateInfos();
        }

        private void populateInfos()
        {
            listView1.Items.Clear();
            List<PartijePregled> odInfos = DTOManager.getInfosPartije(this.turnirId);
            foreach (PartijePregled pp in odInfos)
            {
                ListViewItem item = new ListViewItem(new string[] { pp.id_partije.ToString(), pp.bele_figure, pp.crne_figure, pp.kad_se_igra, pp.trajanje.ToString(), pp.ishod });
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Odaberite sahistu");
                return;
            }
            int partijaId = Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            PoteziInformacije forma = new PoteziInformacije(partijaId);
            forma.ShowDialog();
        }
    }
}

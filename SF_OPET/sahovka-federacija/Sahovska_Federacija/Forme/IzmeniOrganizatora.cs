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
    public partial class IzmeniOrganizatora : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public OrganizatoriHomeBasic tBasic;
        public IzmeniOrganizatora()
        {
            InitializeComponent();
        }

        public IzmeniOrganizatora(OrganizatoriHomeBasic sb)
        {
            this.tBasic = sb;
            InitializeComponent();
            PopulateData();
        }

        public void PopulateData()
        {
            textBox1.Text = tBasic.ime;
            textBox2.Text = tBasic.prezime;
            textBox4.Text = tBasic.adresa;
        }

        private void IzmeniOrganizatora_Load(object sender, EventArgs e)
        {

        }

        private void Label5_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Label5_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Label5_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            tBasic.ime = textBox1.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            tBasic.prezime = textBox2.Text;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            tBasic.adresa = textBox4.Text;
        }
    }
}

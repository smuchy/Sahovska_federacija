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
    public partial class IzmeniSponzora : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public SponzorBasic tBasic;
        public IzmeniSponzora()
        {
            InitializeComponent();
        }

        public IzmeniSponzora(SponzorBasic sb)
        {
            this.tBasic = sb;
            InitializeComponent();
            PopulateData();
        }

        public void PopulateData()
        {
            textBox1.Text = tBasic.ime;
            textBox2.Text = tBasic.logo;
            richTextBox1.Text = tBasic.opis;
        }

        private void IzmeniSponzora_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            tBasic.ime = textBox1.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            tBasic.logo = textBox2.Text;
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            tBasic.opis = richTextBox1.Text;
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

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}

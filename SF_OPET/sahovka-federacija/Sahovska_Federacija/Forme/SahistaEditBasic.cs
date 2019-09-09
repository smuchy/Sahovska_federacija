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
    public partial class SahistaEditBasic : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public SahistaBasic tBasic;
        public SahistaEditBasic()
        {
            InitializeComponent();
        }
        public SahistaEditBasic(SahistaBasic sb)
        {
            this.tBasic = sb;
            InitializeComponent();
            PopulateData();
        }

        private void PopulateData()
        {
            textBox1.Text = tBasic.ime;
            textBox2.Text = tBasic.prezime;
            textBox3.Text = tBasic.ulica;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            tBasic.ime = textBox1.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            tBasic.prezime = textBox2.Text;
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            tBasic.ulica = textBox3.Text;
        }

        

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void SahistaEditBasic_Load(object sender, EventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tBasic.tip = radioButton1.Text;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tBasic.tip = radioButton2.Text;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tBasic.tip = radioButton3.Text;
        }

        private void Button2_Click(object sender, EventArgs e)
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
    }
}

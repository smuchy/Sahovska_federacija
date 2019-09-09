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

namespace Sahovska_Federacija.Forme
{
    public partial class DodajSponzora : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public bool jeste;

        public DodajSponzora()
        {
            this.jeste = false;
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
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

            try
            {
                ISession s = DataLayer.GetSession();
           
                Sponzor sponzor = new Sponzor();

                sponzor.ime = textBox2.Text;
                sponzor.opis = richTextBox1.Text;
                sponzor.logo = textBox3.Text;


                s.Save(sponzor);
                s.Flush();
                s.Close();
                this.jeste = true;

                MessageBox.Show("Uspesno ste dodali sponzora!");
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DodajSponzora_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class DodajOrganizatora : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public bool jeste;

        public DodajOrganizatora()
        {
            this.jeste = false;
            InitializeComponent();
        }

        private void DodajOrganizatora_Load(object sender, EventArgs e)
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Organizator organizator = new Organizator();

                organizator.ime = textBox2.Text;
                organizator.prezime = textBox3.Text; 
                organizator.adresa = textBox4.Text;


                s.Save(organizator);
                s.Flush();
                s.Close();
                this.jeste = true;

                MessageBox.Show("Uspesno ste dodali organizatora!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

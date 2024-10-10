using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service;
using Domaine;

namespace Bibliotheque.IHM
{
    public partial class ModifOuvrage : Form
    {
        ServiceOuvrages serviceOuvrage;
        Ouvrage o;
        public ModifOuvrage(ServiceOuvrages serviceOuvrage, Ouvrage ouvrage)
        {
            InitializeComponent();
            this.serviceOuvrage = serviceOuvrage;
            this.o = ouvrage;
            textBox1.Text = o.Titre;
            textBox2.Text = o.Auteur;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = textBox1.Text;
            string auteur = textBox2.Text;
            o.Titre = titre;
            o.Auteur = auteur;
            serviceOuvrage.Modifier(o);
            this.Close();
        }
    }
}

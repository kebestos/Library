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
    public partial class ModifExemplaire : Form
    {
        Exemplaire exemplaire;
        ServiceExemplaires ServiceExemplaires;
        public ModifExemplaire(Exemplaire e, ServiceExemplaires serviceExemplaires)
        {
            InitializeComponent();
            this.ServiceExemplaires = serviceExemplaires;
            this.exemplaire = e;
            textBox1.Text = exemplaire.Etat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string etat = textBox1.Text;
            exemplaire.Etat = etat;
            ServiceExemplaires.Modifier(exemplaire);
            this.Close();
        }
    }
}

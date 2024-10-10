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
    public partial class ModifAdherent : Form
    {
        ServiceAdherents ServiceAdherents;
        Adherent Adherent;
        public ModifAdherent(ServiceAdherents serviceAdherents, Adherent adherent)
        {
            InitializeComponent();
            this.Adherent = adherent;
            this.ServiceAdherents = serviceAdherents;
            textBox1.Text = Adherent.Nom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = textBox1.Text;
            Adherent.Nom = nom;
            ServiceAdherents.Modifier(Adherent);
            this.Close();
        }
    }
}

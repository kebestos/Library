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
using NHibernate;
using Persistance;
using Domaine;
using IHM;

namespace Bibliotheque.IHM
{
    public partial class AddOuvrage : Form
    {        
        ServiceOuvrages serviceOuvrages;
                
        public AddOuvrage(ServiceOuvrages serv)
        {
            InitializeComponent();
            this.serviceOuvrages = serv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = textBox1.Text;
            string auteur = textBox2.Text;

            if(titre != "" && auteur != "")
            {                
                serviceOuvrages.Ajouter(titre, auteur);                     
                this.Close();
            }
        }
    }
}

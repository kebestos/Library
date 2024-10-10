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

namespace Bibliotheque.IHM
{
    public partial class AddAdherent : Form
    {
        ServiceAdherents ServiceAdherents;
        public AddAdherent(ServiceAdherents service)
        {
            InitializeComponent();
            this.ServiceAdherents = service;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = textBox1.Text;
            ServiceAdherents.Ajouter(nom);
            this.Close();
        }
    }
}

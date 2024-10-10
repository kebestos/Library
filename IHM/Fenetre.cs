using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Domaine;
using Service;

namespace IHM
{
    public partial class Fenetre : Form
    {
        ServiceExemplaires  serviceExemplaires;
        ServiceAdherents    serviceAdherents;
        ServiceOuvrages     serviceOuvrages;
        ServicePrets        servicePrets;

        List<Exemplaire> exemplaires;
        List<Adherent>   adherents;
        List<Ouvrage>    ouvrages;
        List<Pret>       prets;

        public Fenetre(ServiceAdherents adherents, ServiceOuvrages ouvrages, ServicePrets prets, ServiceExemplaires exemplaires)
        {
            InitializeComponent();

            InitialiserServices(adherents, ouvrages, prets, exemplaires);
            ActualiserAdherents();
            ActualiserOuvrages();
            ActualiserPrets();
        }

        void InitialiserServices(ServiceAdherents adherents, ServiceOuvrages ouvrages, ServicePrets prets, ServiceExemplaires exemplaires)
        {
            serviceExemplaires = exemplaires;
            serviceAdherents  = adherents;
            serviceOuvrages   = ouvrages;
            servicePrets      = prets;
        }

        void ActualiserAdherents()
        {
            adherents = serviceAdherents.ObtenirListe();
            AfficherListe(adherents, listBoxAdherents);
            ActualiserPrets();
        }

        public void ActualiserOuvrages()
        {
            ouvrages = serviceOuvrages.ObtenirListe();
            AfficherListe(ouvrages, listBoxOuvrages);
            ActualiserExemplaires();
        }

        void ActualiserPrets()
        {
            int idx = listBoxAdherents.SelectedIndex;            
            // TODO:
            // 1. Recuperer l'identifiant de l'adherent selectionné            
            int Ida = adherents[idx].Id;
            // 2. Recuperer la liste des prets associés à l'adherent
            prets = servicePrets.ObtenirListeParAdherent(Ida);
            // 3. Afficher la liste des prets
            AfficherListe(prets, listBoxPrets);
        }

        void ActualiserExemplaires()
        {
            int idx = listBoxOuvrages.SelectedIndex;

            // TODO:
            // 1. Recuperer l'identifiant de l'ouvrage selectionné
            int Ido = ouvrages[idx].Id;
            // 2. Recuperer la liste des exemplaires associés à l'ouvrage
            exemplaires = serviceExemplaires.ObtenirListeParOuvrage(Ido);
            // 3. Afficher la liste des exemplaires
            AfficherListe(exemplaires, listBoxExemplaires);
        }

        private void listBoxOuvrages_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualiserExemplaires();
        }

        private void listBoxAhderents_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualiserPrets();
        }

        void AfficherListe<T>(List<T> items, ListBox box)
        {
            List<string> liste = new List<string>();
            foreach (T a in items)
                liste.Add(a.ToString());
            box.DataSource = liste;
        }

        private void buttonEmprunter_Click(object sender, EventArgs e)
        {
            // TODO:
            // 1. Recuperer l'identifiant de l'adherent selectionné
            int idx = listBoxAdherents.SelectedIndex;
            int Ida = adherents[idx].Id;
            // 2. Recuperer l'identifiant de l'exemplaire selectionné
            int idx2 = listBoxExemplaires.SelectedIndex;
            int Ide = exemplaires[idx2].Id;

            try
            {
                // 3. Execution de l'emprunt
                servicePrets.TraiterEmprunt(Ida, Ide);
                // 4. Mis à jour de l'IHM
                ActualiserPrets();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Emprunt échoué", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRetourner_Click(object sender, EventArgs e)
        {
            // TODO:
            // 1. Recuperer l'identifiant de l'exemplaire selectionné
            int idx2 = listBoxPrets.SelectedIndex;
            int Ide = prets[idx2].Exemplaire.Id;
            try
            {
                // 2. Execution du retour
                servicePrets.TraiterRetour(Ide);
                // 3. Mis à jour de l'IHM
                ActualiserPrets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Retour échoué", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSupprimerOuvrage_Click(object sender, EventArgs e)
        {
            int idx = listBoxOuvrages.SelectedIndex;
            Ouvrage o = ouvrages[idx];
            List<Exemplaire> listE = serviceExemplaires.ObtenirListeParOuvrage(o.Id);
            foreach(Exemplaire ec in listE)
            {
                ec.Ouvrage = null;
                serviceExemplaires.Modifier(ec);
            }
            serviceOuvrages.Supprimer(ouvrages[idx]);
            ActualiserOuvrages();
        }

        private void buttonAjouterOuvrage_Click(object sender, EventArgs e)
        {            
            Bibliotheque.IHM.AddOuvrage addOuvrage = new Bibliotheque.IHM.AddOuvrage(serviceOuvrages);
            if(addOuvrage.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserOuvrages();
            }            
            addOuvrage.Dispose();
        }

        private void buttonModifierOuvrage_Click(object sender, EventArgs e)
        {
            int idx = listBoxOuvrages.SelectedIndex;
            Ouvrage o = ouvrages[idx];
            Bibliotheque.IHM.ModifOuvrage modifOuvrage = new Bibliotheque.IHM.ModifOuvrage(serviceOuvrages, o);
            if (modifOuvrage.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserOuvrages();
            }
            modifOuvrage.Dispose();
        }

        private void buttonAjouterAdherent_Click(object sender, EventArgs e)
        {
            Bibliotheque.IHM.AddAdherent addAdherent = new Bibliotheque.IHM.AddAdherent(serviceAdherents);
            if(addAdherent.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserAdherents();
            }
            addAdherent.Dispose();
        }

        private void buttonSupprimerAdherent_Click(object sender, EventArgs e)
        {
            int idx = listBoxAdherents.SelectedIndex;
            Adherent a = adherents[idx];
           foreach (Pret p in a.Prets)
           {
               if(p.Adherent != null && p.DateRetour == DateTime.MinValue)
               {
                    int i = p.Exemplaire.Id;
                    servicePrets.TraiterRetour(i);
               }        
                p.Adherent = null;
                servicePrets.Modifier(p);
           }
            a.Prets = null;
            serviceAdherents.Modifier(a);
           serviceAdherents.Supprimer(a);
           ActualiserAdherents();
        }

        private void buttonModifierAdherent_Click(object sender, EventArgs e)
        {
            int idx = listBoxAdherents.SelectedIndex;
            Adherent a = adherents[idx];
            Bibliotheque.IHM.ModifAdherent modifAdherent = new Bibliotheque.IHM.ModifAdherent(serviceAdherents, a);
            if(modifAdherent.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserAdherents();
            }
            modifAdherent.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idx = listBoxOuvrages.SelectedIndex;
            Ouvrage o = ouvrages[idx];
            Bibliotheque.IHM.AddExemplaire addExemplaire = new Bibliotheque.IHM.AddExemplaire(o, serviceExemplaires);
            if(addExemplaire.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserExemplaires();
            }
            addExemplaire.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idx = listBoxExemplaires.SelectedIndex;
            Exemplaire exemple = exemplaires[idx];
            Bibliotheque.IHM.ModifExemplaire ModifExemplaire = new Bibliotheque.IHM.ModifExemplaire(exemple, serviceExemplaires);
            if (ModifExemplaire.ShowDialog(this) != DialogResult.OK)
            {
                ActualiserExemplaires();
            }
            ModifExemplaire.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idx = listBoxExemplaires.SelectedIndex;
            Exemplaire ex = exemplaires[idx];
            foreach(Adherent al in adherents)
            {
                foreach(Pret p in al.Prets)
                {
                    if(p.Exemplaire == ex)
                    {
                        p.Exemplaire = null;
                        servicePrets.Modifier(p);
                    }
                }
            }
            ex.Ouvrage = null;
            serviceExemplaires.Modifier(ex);
            serviceExemplaires.Supprimer(ex);
            ActualiserExemplaires();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Pret
    {
        public virtual int Id { get; set; }

        public virtual DateTime DateEmprunt { get; set; }

        public virtual DateTime DateRetour { get; set; }

        public virtual Exemplaire Exemplaire { get; set; }

        public virtual Adherent Adherent { get; set; }   

        public virtual bool EstTerminer()
        {
            return DateRetour >= DateEmprunt;
        }

        public override string ToString()
        {
            string s;
            if(Exemplaire != null)
            {
                if (Exemplaire.Ouvrage != null)
                {
                    s = Exemplaire.Ouvrage.Titre + " " + Exemplaire.Id.ToString() + " " + DateRetour.ToString();
                }
                else
                {
                    s = Exemplaire.Id.ToString() + " " + DateRetour.ToString();
                }
            }
            else
            {
                s = "exemplaire supprimer" + DateRetour.ToString();
            }
            return s;
        }
    }
}

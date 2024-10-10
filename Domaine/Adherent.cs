using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Adherent
    {

        public virtual IList<Pret> Prets { get; set; }
        public virtual int Id { get; set; }

        public virtual String Nom { get; set; }

        public Adherent()
        {
            Prets  = new List<Pret>();
        }

        public virtual Pret Emprunte(Exemplaire exemplaire)
        {
            if(exemplaire.EstDisponible())
            {
                exemplaire.Adherent = this;
                Pret pret = new Pret { Exemplaire = exemplaire , Adherent = this};
                pret.DateEmprunt = DateTime.Now;
                pret.DateRetour = DateTime.MinValue;
                Prets.Add(pret);
                return pret;
            }
            throw new Exception();
        }

        public virtual void Retourne(Exemplaire exemplaire)
        {
            foreach(Pret p in Prets)
            {
                if(p.Exemplaire == exemplaire && p.DateRetour == DateTime.MinValue)
                {
                    p.DateRetour = DateTime.Now;
                    exemplaire.Adherent = null;
                    return;
                }                
            }
            throw new Exception();
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}

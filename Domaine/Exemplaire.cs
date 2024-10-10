using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Exemplaire
    {
       
        public virtual int Id { get; set; }
               
        public virtual Adherent Adherent { get; set; }

        public virtual Ouvrage Ouvrage { get; set; }

        public virtual String Etat { get; set; }

        public virtual bool EstDisponible()
        {
            if (Adherent != null) return false;
            return true;
        }

        public override string ToString()
        {
            string s = Ouvrage.Titre + " " + Id.ToString();
            return s;
        }
    }
}
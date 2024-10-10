using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceOuvrages : Service
    {
        public ServiceOuvrages(IDataAccess factory) : base(factory) { }

        public List<Ouvrage> ObtenirListe()
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                List<Ouvrage> liste = depotOuvrages.Query().ToList();
                uow.Commit();
                return liste;
            }
        }

        public void Ajouter(string titre, string auteur)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                Ouvrage o = new Ouvrage { Titre = titre, Auteur = auteur };
                depotOuvrages.Create(o);
                uow.Commit();
                return;
            }
        }

        public void Modifier(Ouvrage o)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotOuvrages.Update(o);
                uow.Commit();
                return;
            }
        }

        public void Supprimer(Ouvrage o)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotOuvrages.Delete(o);
                uow.Commit();
                return;
            }
        }
    }
}

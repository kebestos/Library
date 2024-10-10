using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceExemplaires : Service
    {
        public ServiceExemplaires(IDataAccess factory) : base(factory) { }

        public List<Exemplaire> ObtenirListeParOuvrage(int id_ouvrage)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                List<Exemplaire> liste = depotExemplaires.Query().Where(ex => ex.Ouvrage.Id == id_ouvrage /* TODO: écrire la bonne condition logique */).ToList();
                uow.Commit();
                return liste;
            }
        }

        public void Ajouter(Ouvrage o, string etat)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                Exemplaire e = new Exemplaire { Ouvrage = o, Etat = etat };
                depotExemplaires.Create(e);
                uow.Commit();
                return;
            }
        }

        public void Modifier(Exemplaire e)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotExemplaires.Update(e);
                uow.Commit();
                return;
            }
        }

        public void Supprimer(Exemplaire e)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotExemplaires.Delete(e);
                uow.Commit();
                return;
            }
        }
    }
}

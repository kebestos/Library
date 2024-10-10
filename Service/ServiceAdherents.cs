 using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceAdherents : Service
    {
        public ServiceAdherents(IDataAccess factory) : base(factory) { }
 

        public List<Adherent> ObtenirListe()
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                List<Adherent> liste = depotAdherents.Query().ToList();
                uow.Commit();
                return liste;
            }
        }

        public void Ajouter(string nom)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                Adherent a = new Adherent { Nom = nom };
                depotAdherents.Create(a);
                uow.Commit();
                return;
            }
        }

        public void Modifier(Adherent a)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotAdherents.Update(a);
                uow.Commit();
                return;
            }
        }

        public void Supprimer(Adherent a)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotAdherents.Delete(a);
                uow.Commit();
                return;
            }
        }
    }
}

using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServicePrets : Service
    {
        public ServicePrets(IDataAccess factory) : base(factory) { }

        public List<Pret> ObtenirListeParAdherent(int id_adherent)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                List<Pret> liste = depotPrets.Query().Where(p => p.Adherent.Id == id_adherent).ToList();
                uow.Commit();
                return liste;
            }
        }

        public void TraiterEmprunt(int idAdherent, int idExemplaire)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                Adherent a = depotAdherents.Read(idAdherent);
                Exemplaire e = depotExemplaires.Read(idExemplaire);
                if(a != null && e != null)
                {
                    Pret p = a.Emprunte(e);
                    //update
                    depotPrets.Create(p);
                    depotAdherents.Update(a);
                    depotExemplaires.Update(e);
                    uow.Commit();
                    return;
                }
                throw new Exception();
            }
        }

        public void TraiterRetour(int idExemplaire)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                Exemplaire e = depotExemplaires.Read(idExemplaire);
                if( e != null && e.Adherent != null)
                {
                    Adherent a = depotAdherents.Read(e.Adherent.Id);
                    a.Retourne(e);
                    depotAdherents.Update(a);
                    depotExemplaires.Update(e);                    
                    uow.Commit();
                    return;
                }                
                throw new Exception();
            }                
        }

        public void Modifier(Pret p)
        {
            using (IUnitOfWork uow = BeginTransaction())
            {
                depotPrets.Update(p);
                uow.Commit();
                return;
            }
            throw new Exception();
        }
    }
}

using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public Final_ProjectContext context { get; set; }
        public ClientManager(Final_ProjectContext db)
        {
            context = db;
        }

        public void Create(ClientProfile item)
        {
            context.ClientProfiles.Add(item);
            context.SaveChanges();
        }
        public IEnumerable<ClientProfile> GetAll()
        {
            return context.ClientProfiles.ToList();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
   
}
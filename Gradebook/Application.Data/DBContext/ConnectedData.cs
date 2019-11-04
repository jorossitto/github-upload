using AppCore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppCore.Data
{
    public class ConnectedData
    {
        private BusinessDBContext context;

        public ConnectedData()
        {
            context = new BusinessDBContext();
            context.Database.Migrate();
        }

        public Samurai CreateNewSamurai()
        {
            var samurai = new Samurai { Name = "New Samurai" };
            context.Samurais.Add(samurai);
            return samurai;
        }

        public BindingList<Samurai> SamuraisListInMemory()
        {
            if(context.Samurais.Local.Count == 0)
            {
                context.Samurais.ToList();
            }
            return context.Samurais.Local.ToBindingList();
        }

        public ObservableCollection<Samurai> ObservableSamuraisListInMemory()
        {
            if (context.Samurais.Local.Count == 0)
            {
                context.Samurais.ToList();
            }

            return context.Samurais.Local.ToObservableCollection();
        }

        public Samurai LoadSamuraiGraph(int samuraiID)
        {
            var samurai = context.Samurais.Find(samuraiID);
            context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
            context.Entry(samurai).Collection(s => s.Quotes).Load();

            return samurai;
        }

        public bool ChangesNeedToBeSaved()
        {
            return context.ChangeTracker.Entries()
                .Any(e => e.State == EntityState.Added
                | e.State == EntityState.Modified
                | e.State == EntityState.Deleted);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

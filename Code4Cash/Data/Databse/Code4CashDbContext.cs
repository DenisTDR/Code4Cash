using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels.Base;
using MySql.Data.Entity;

namespace Code4Cash.Data.Databse
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Code4CashDbContext: DbContext
    {
        public Code4CashDbContext()
        {
            this.ConfigAndEvents();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityMapsTypes = EntityMap<Entity>.GetAllEntityMaps().ToList();

            entityMapsTypes.ForEach(entityMapsType =>
            {
                dynamic configurationInstance = Activator.CreateInstance(entityMapsType);
                modelBuilder.Configurations.Add(configurationInstance);
            });
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        private void SavingChanges(object sender, EventArgs e)
        {
            var context = sender as ObjectContext;
            if (context == null) return;

            // You can use other EntityState constants here
            foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                var entity = entry.Entity as Entity;
                if (entity != null)
                {
                    entity.Created = DateTime.Now;
                    entity.Selector = Guid.NewGuid().ToString();
                }
            }
            foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (entry.Entity is Entity)
                {
                    ((Entity)entry.Entity).Updated = DateTime.Now;
                }
            }
        }

        private void ConfigAndEvents()
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.SavingChanges += SavingChanges;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ScanRunner.Data.Entities;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Infastructure;
using System;
using System.Collections.Generic;

namespace SecureGovernment.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private Queue<ICommand> _commands { get; } = new Queue<ICommand>();
        private ObservatoryContext Database { get; set; }

        public UnitOfWork() { }
        public UnitOfWork(ObservatoryContext observatoryContext) { Database = observatoryContext; }

        public void Queue(ICommand command) => _commands.Enqueue(command);
        
        public void Dispose()
        {
            if (Database == null) Database = new ObservatoryContext();
            while (_commands.Count > 0)
            {
                ICommand command = _commands.Dequeue();
                if (command is EntityCommand<Websites> website) CommandHandler<Websites>.Handle(Database.Websites, website);
                else if (command is EntityCommand<Scans> scan) CommandHandler<Scans>.Handle(Database.Scans, scan);
                else if (command is EntityCommand<Analysis> analysis) CommandHandler<Analysis>.Handle(Database.Analysis, analysis);
                else if (command is EntityCommand<Certificates> certificate) CommandHandler<Certificates>.Handle(Database.Certificates, certificate);
                else if (command is EntityCommand<Trust> trust) CommandHandler<Trust>.Handle(Database.Trust, trust);
                else if (command is EntityCommand<WebsiteCategories> websiteCategory) CommandHandler<WebsiteCategories>.Handle(Database.WebsiteCategories, websiteCategory);
                else throw new NotImplementedException();
            }

            Database.SaveChanges();
            _commands.Clear();
        }
    }

    static class CommandHandler<TEntity> where TEntity : class
    {
        public static void Handle(DbSet<TEntity> dbSet, EntityCommand<TEntity> entityCommand)
        {
            switch (entityCommand.EntityCommandType)
            {
                case CommandType.CREATE:
                    Create(dbSet, entityCommand.Entity);
                    break;
                case CommandType.UPDATE:
                    Update(dbSet, entityCommand.Entity);
                    break;
                case CommandType.DELETE:
                    Delete(dbSet, entityCommand.Entity);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void Create(DbSet<TEntity> dbSet, TEntity entity) => dbSet.Add(entity);
        private static void Update(DbSet<TEntity> dbSet, TEntity entity) => dbSet.Update(entity);
        private static void Delete(DbSet<TEntity> dbSet, TEntity entity) => dbSet.Remove(entity);
    }
}

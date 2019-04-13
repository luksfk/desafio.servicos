using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TesteServicos.DAL.Interfaces;
using TesteServicos.Models;
using TesteServicos.Utils;

namespace TesteServicos.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;
        private readonly IUserService userService;

        public GenericRepository(ApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.userService = userService;
        }

        private string UserName => this.userService.UserName();

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool filterUser = true)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (filterUser && !string.IsNullOrEmpty(UserName))
            {
                query = query.Where(t => t.FornecedorId == FornecedorIdLogado);
            }            

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            if (!Exists((int) id)) return null;
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {            
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            if (!Exists((int)id)) return;
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (!Exists(entityToDelete.Id)) return;
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate.Id)) return;            
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public bool Exists(int? id)
        {
            return dbSet.Where(t => t.Id == id && t.FornecedorId == FornecedorIdLogado).Count() > 0;
        }        

        public int FornecedorIdLogado => context.Users.Where(t => t.UserName == this.UserName).FirstOrDefault().FornecedorId;

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Entity;

namespace Data.Repositories
{
    public abstract class RepositoryBase<T, S> 
        where T : BaseEntity<S>
        where S : struct, IEquatable<S>
    {

        /// <summary>
        /// The entity set for this entity.
        /// </summary>
        internal DbSet<T> EntitySet { get; set; }

        /// <summary>
        /// The model context to be used in this repository.
        /// </summary>
        internal ModelContext Context { get; set; }

        public RepositoryBase(ModelContext context)
        {
            Context = context;
            EntitySet = context.Set<T>();
        }

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Created entity</returns>
        public T Add(T entity)
        {
            T savedEntity = EntitySet.Add(entity);
            return savedEntity;
        }

        /// <summary>
        /// Get all entities from repository
        /// </summary>
        /// <returns>A queryable object</returns>
        public T GetById(S id)
        {
            return EntitySet.FirstOrDefault(a => a.Id.Equals(id) && !a.Deleted);
        }

        /// <summary>
        /// Get an entity by Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Recovered entity</returns>
        public IQueryable<T> GetAll()
        {
            return EntitySet.Where(a => !a.Deleted);
        }

        /// <summary>
        /// Removes an entity from repository
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public void Remove(T entity)
        {
            entity.Deleted = true;
        }

        /// <summary>
        /// Save the changes in the context
        /// </summary>
        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

    }
}

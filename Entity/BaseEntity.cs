using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// Base entity for all the entities in the Domain model.
    /// </summary>
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Identifies if the entity was deleted logically.
        /// To delete objects use the proper repository method.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Unique Id for an entity
        /// </summary>
        public T Id { get; set; }
    }
}
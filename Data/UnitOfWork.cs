using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork
    {
        private ModelContext context;
        /// <summary>
        /// Creates a new UnitOfWork
        /// </summary>
        public UnitOfWork()
        {
            this.context = new ModelContext();
        }

        /// <summary>
        /// Bank repository
        /// </summary>
        public BankRepository BankRepository
        {
            get
            {
                return new BankRepository(this.context);
            }
        }

        /// <summary>
        /// Branch repository
        /// </summary>
        public BranchRepository BranchRepository
        {
            get
            {
                return new BranchRepository(this.context);
            }
        }

    }
}

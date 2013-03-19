using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Entity;

namespace Data.Repositories
{
    public class BankRepository: RepositoryBase<Bank,int>
    {
        public BankRepository(ModelContext context): base(context)
        {
        }
    }
}

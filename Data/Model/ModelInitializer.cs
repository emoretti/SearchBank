using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Data.Model
{
    internal class ModelInitializer : CustomModelInitializer<ModelContext>
    {

        protected override void Seed(ModelContext context)
        {
            LoadBanks(context);
        }

        private void LoadBanks (ModelContext context)
        {
            var bank1 = new Bank {Name = "HSBC"};
            var bank2 = new Bank { Name = "Standar Bank" };
            context.Banks.Add(bank1);
            context.Banks.Add(bank2);
            context.SaveChanges();
        }
    }
}
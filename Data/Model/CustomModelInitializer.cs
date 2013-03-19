using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    
public class CustomModelInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Initialize database
        /// </summary>
        /// <param name="context">Data context</param>
        public void InitializeDatabase(TContext context)
        {

           #if DEBUG
            if (context.Database.Exists() && !context.Database.CompatibleWithModel(false))
                context.Database.Delete();
            #endif
            
            if (!context.Database.Exists())
            {
                context.Database.Create();
               this.RunDatabaseInitializationScripts(context);
               this.Seed(context);
            }
        }
        
        protected virtual void Seed(TContext context)
        { 
        }

        private void RunDatabaseInitializationScripts(TContext context)
        {
        }

    }
}
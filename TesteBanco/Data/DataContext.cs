using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TesteBanco.Entities;

namespace TesteBanco.Data
{
    public class DataContext : DbContext
    {

        static private string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Initial Catalog=Teste3;Integrated Security=True;";


        public DataContext() : base(ConexaoString)
        {
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            Database.Initialize(false);
        }

        public DbSet<Versao> versao { get; set; }
    }
}

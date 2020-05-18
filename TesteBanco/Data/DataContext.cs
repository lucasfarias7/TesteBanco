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

        //String de conexao para instancia nessa maquina
        //static private string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Initial Catalog=Teste;Integrated Security=True;";

        //String de conexao para o banco da web
        static private string ConexaoString = @"Data Source=64.37.58.162,1598;Initial Catalog=2507507;User id=SIGP-Entidade;Password=5q8b>S!1ORsgcmC;Connect Timeout=15;MultipleActiveResultSets=True;Integrated Security=False;";


        public DataContext() : base(ConexaoString)
        {
            Database.SetInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>());
            Database.Initialize(false);
        }

        public DbSet<Versao> versao { get; set; }
    }
}

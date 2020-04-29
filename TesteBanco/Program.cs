using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBanco.Funcao;
using TesteBanco.Entities;
using TesteBanco.Data;

namespace TesteBanco
{
    class Program
    {
        static void Main(string[] args)
        {

            //  DataContext db = new DataContext();
            FuncaoBanco.AtualizarVersao();

            //FuncaoBanco func = new FuncaoBanco();

            //FuncaoBanco.UpdateDatabaseEntities();
            
            Console.ReadKey();

          
        }
    }
}

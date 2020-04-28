using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBanco.Funcao;

namespace TesteBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            FuncaoBanco func = new FuncaoBanco();

            FuncaoBanco.UpdateDatabaseEntities();

            Console.ReadKey();
          
        }
    }
}

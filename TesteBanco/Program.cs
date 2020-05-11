using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBanco.Funcao;
using TesteBanco.Entities;
using TesteBanco.Data;
using System.Threading;
using System.Net;

namespace TesteBanco
{
    class Program
    {
        static string DownloaddHtml()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            Thread.Sleep(5000);
            return wc.DownloadString("http://www.google.com");
        }

        static void Main(string[] args)
        {

            //  DataContext db = new DataContext();
             
            //var result = DownloaddHtml();
            //Console.WriteLine(result);
            Console.WriteLine("OI");
            Thread.Sleep(5000);
            Task.Run(() => FuncaoBanco.AtualizarVersao());
            //FuncaoBanco.AtualizarVersao();
            //Task.Run(() => FuncaoBanco.AtualizarVersao());



            //FuncaoBanco func = new FuncaoBanco();

            //FuncaoBanco.UpdateDatabaseEntities();

            Console.ReadKey();

          
        }
    }
}

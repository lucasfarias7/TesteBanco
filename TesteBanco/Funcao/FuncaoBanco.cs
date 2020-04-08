using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Glimpse.AspNet.Tab;

namespace TesteBanco.Funcao
{
    public class FuncaoBanco
    {
        public void UpdateDatabase()
        {
			string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Initial Catalog=Teste;Integrated Security=True;";

			using(SqlConnection conn = new SqlConnection(ConexaoString))
			{
				try
				{
					conn.Open();

					string caminho = @"C:\Arqu\Atualizacao_02.sql";
					string script = File.ReadAllText(caminho);

					IEnumerable<string> commandoStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

					foreach(string commando in commandoStrings)
					{
						
						if(commando.Trim() != "")
						{
							new SqlCommand(commando, conn).ExecuteNonQuery();
						}
						
					}
					Console.WriteLine("Banco de dados atualizado com sucesso!!");
				}
				catch (SqlException er)
				{
					Console.WriteLine(er.Message);
				}
				finally
				{
					conn.Close();
				}
			}
			
        }
    }
}

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
using System.Data;
using TesteBanco.Data;
using TesteBanco.Entities;

namespace TesteBanco.Funcao
{
    public class FuncaoBanco
    {

		public static void AtualizarVersao()
		{
			DataContext db = new DataContext();

			Versao versao = db.versao.Find(1);

			if (versao.numero == 1)
			{
				//caminho do arquivo .sql
				string caminho = @"C:\Arqu\Atualizacao_01.sql";

				if (UpdateDatabaseEntities(caminho))
				{
					versao.numero += 1;
				}
			}

			else if (versao.numero == 2)
			{
				//caminho do arquivo .sql
				string caminho = @"C:\Arqu\Atualizacao_02.sql";

				if (UpdateDatabaseEntities(caminho))
				{
				     versao.numero += 1;
				}
			}

			db.SaveChanges();
		}

      
	public static bool UpdateDatabaseEntities(string caminho)
    {
		List<string> listaBancos = new List<string>();

		//string de conexao
		string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Integrated Security=True;MultipleActiveResultSets=true;";

		//Passando a string de conexao
		using (SqlConnection conn = new SqlConnection(ConexaoString))
		{
					
				//abrindo a conexao
				conn.Open();

				string selectBanco = "SELECT name from sys.databases where name LIKE 'Teste%'";

				using (SqlCommand cmd = new SqlCommand(selectBanco, conn))
				{

					using (SqlDataReader rdr = cmd.ExecuteReader())
					{

						while (rdr.Read())
						{
							listaBancos.Add(rdr[0].ToString());		
						}

					}		

				}

				SqlTransaction tran = conn.BeginTransaction();
					
				try
				{
					//recupero o conteudo do arquivo .sql
					string script = File.ReadAllText(caminho);

					//Separando em uma lista o que tiver com instruções a cada 'GO'
					IEnumerable<string> commandoStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

						
					//percorro toda a lista e executo a instrução que ali estiver em cada sequencia. 
					foreach (string commando in commandoStrings)
					{			
						//o metodo trim ele serve para tirar espaços em branco que estiver em sua string e deixar juntos.
						if (commando.Trim() != "")
						{
							  for(int i=0; i<listaBancos.Count; i++)
							  {
								   SqlCommand sql = conn.CreateCommand();
								   sql.Transaction = tran;
								   sql.CommandText = "USE [" + listaBancos[i] + "]";
								   sql.ExecuteNonQuery();
								   sql.CommandText = commando;
								   sql.ExecuteNonQuery();
							  }
																							
						}
							
					}

					tran.Commit();

					Console.WriteLine("Banco de dados atualizado com sucesso!!");

					return true;
					
				}
				catch (SqlException er)
				{
					tran.Rollback();
					//mostro alguma mensagem de erro caso venha a falhar essa conexao, ou que venha falhar com o arquivo .sql passado.
					Console.WriteLine(er.Message);

					return false;
				}
				finally
				{
					//fecho a conexao com o banco usando o close.
					conn.Close();
					conn.Dispose();
				}
		}
     }






		public List<string> GetAllDataBases()
		{
			List<string> list = new List<string>();

			string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Integrated Security=True;";

			using (SqlConnection conn = new SqlConnection(ConexaoString))
			{
				conn.Open();

				using(SqlCommand cmd = new SqlCommand("SELECT name from sys.databases where name LIKE 'Teste%'", conn))
				{
					using(IDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							list.Add(rdr[0].ToString());
						}
					}
				}
			}

			return list;
		}

		public static string[] GetDatabaseNames()
		{
			List<string> databases = new List<string>();

			string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Integrated Security=True;";

			SqlConnection conn;

			using (SqlCommand cmd = new SqlCommand("SELECT [name] FROM sysdatabases", conn = new SqlConnection(ConexaoString)))
			{
				conn.Open();

				using (SqlDataReader rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						databases.Add((string)rdr["name"]);						
					}
				}
			}

			return databases.ToArray();
		}

		public static List<string> gGetAllDataBases()
		{
			List<string> list = new List<string>();

			string ConexaoString = @"Data Source=64.37.58.162,1598;Integrated Security=False;User ID=SIGP-Entidade;Password=5q8b>S!1ORsgcmC;";

			using (SqlConnection conn = new SqlConnection(ConexaoString))
			{
				conn.Open();

				string select = "SELECT name from sys.databases where name LIKE '2%'";

				using (SqlCommand cmd = new SqlCommand(select, conn))
				{
					using (SqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							list.Add(rdr[0].ToString());
						}
					}
				}
			}

			return list;
		}

		/* 
	   public void atualizaVersao()
	   {
		   if controleVersao.numero == 1 { 
			   arquivo = lerAqruivoAtualizacao .SQL _1

			   UpdateDatabase(arqivo);

			   controleVersao = 2;


		   }

		   if controleVersao.numero == 2 {
			   arquivo = lerAqruivoAtualizacao.SQL _2

			   UpdateDatabase(arqivo);

			   controleVersao = 3;


		   }


	   }
	   */
	}
}

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

namespace TesteBanco.Funcao
{
    public class FuncaoBanco
    {
       /* @concorrencia
		public void atualizaVersao
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
		public void UpdateDatabase()
        {
			List<string> listaBancos = new List<string>();

			//string de conexao
			string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Integrated Security=True;MultipleActiveResultSets=true;";

				//Passando a string de conexao
				using (SqlConnection conn = new SqlConnection(ConexaoString))
				{
					try
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

						//caminho do arquivo .sql
						string caminho = @"C:\Arqu\Atualizacao_02.sql";
						//recupero o conteudo do arquivo .sql
						string script = File.ReadAllText(caminho);

						//uma lista para ser executado o comando a partir do go
						IEnumerable<string> commandoStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

						// percorro toda a lista e executo a instrução que ali estiver em cada sequencia. 
						foreach (string commando in commandoStrings)
						{			
								//o metodo trim ele serve para tirar espaços em branco que estiver em sua string e             deixar juntos.
								if (commando.Trim() != "")
								{
							       for(int i=0; i<listaBancos.Count; i++)
									{
										SqlCommand sql = conn.CreateCommand();
										sql.CommandText = "USE [" + listaBancos[i] + "]";
										sql.ExecuteNonQuery();
										sql.CommandText = commando;
										sql.ExecuteNonQuery();
									}
																							
								}
							
						}
						
					Console.WriteLine("Banco de dados atualizado com sucesso!!");
					}
					catch (SqlException er)
					{
						//mostro alguma mensagem de erro caso venha a falhar essa conexao, ou que venha falhar com o arquivo .sql passado.
						Console.WriteLine(er.Message);
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
	}
}

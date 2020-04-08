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
			// Criando minha string de conexao com o banco, ou seja, dizendo onde ele estar.
			string ConexaoString = @"Data Source=DESKTOP-11CCP3D\SQLEXPRESS;Initial Catalog=Teste;Integrated Security=True;"; 

			// Aqui se faz necessário criar um obj do tipo de classe para ser feita a conexao com o banco
			using(SqlConnection conn = new SqlConnection(ConexaoString))
			{
				try
				{
					// Apartir do obj da classe SqlConnection acesso o metodo 'open' para abrir a conexao com o banco
					conn.Open();

					// Aqui estou passando o caminho onde esta o arquivo .sql
					string caminho = @"C:\Arqu\Atualizacao_02.sql";
					// Aqui recupero todo o conteudo do arquivo .sql
					string script = File.ReadAllText(caminho);

					// crio uma lista do tipo referente ao que é o arquivo, no caso foi de strings, e passo uma expressao regular dizendo que apartir do conteudo .sql onde tiver o go execute a instrução que estiver acima.
					IEnumerable<string> commandoStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

					// percorro toda a lista e executo a instrução que ali estiver em cada sequencia. 
					foreach(string commando in commandoStrings)
					{
						//o metodo trim ele serve para tirar espaços em branco que estiver em sua string e deixar juntos.
						if(commando.Trim() != "")
						{   //executo a minha instrução que estiver no arquivo em sequencia apartir do metodo ExecuteNonQuery().
							new SqlCommand(commando, conn).ExecuteNonQuery();
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
				}
			}
			
        }
    }
}

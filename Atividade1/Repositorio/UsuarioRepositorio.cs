using Atividade1.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Atividade1.Repositorio
{
    public class UsuarioRepositorio(IConfiguration Configuration)
    {
        private readonly string _conexaoMySQL = Configuration.GetConnectionString("ConexaoMySQL");
        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * from Usuarios where Email = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;

                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUser = Convert.ToInt32(dr["IdUser"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = Convert.ToString(dr["Senha"]),
                            


                        };
                    }

                    return usuario;
                }
            }
        }
        public void AdcionarUsuario(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO Usuarios(Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)", conexao);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}
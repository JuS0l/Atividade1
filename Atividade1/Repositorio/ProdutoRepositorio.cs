using Atividade1.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC.Multiplier;
using System.Data;


namespace Atividade1.Repositorio
{
    public class  ProdutoRepositorio(IConfiguration Configuration)
    {
        private readonly string _conexaoMySQL = Configuration.GetConnectionString("ConexaoMySQL");
        public Produto ObterProduto(string IdProd)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * from Produtos where IdProd = @IdProd", conexao);
                cmd.Parameters.Add("@IdProd", MySqlDbType.VarChar).Value = IdProd;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) 
                {
                    Produto produto = null;

                    if(dr.Read())
                    {
                        produto = new Produto
                        {
                            IdProd = Convert.ToInt32(dr["IdProd"]),
                            Nome = dr["Nome"].ToString(),
                            Descricao = dr["Descricao"].ToString(),
                            Preco = Convert.ToDouble (dr["Preco"]),
                            Quantidade =  Convert.ToInt32 (dr["Quantidade"]),


                        };
                    }

                    return produto;
                }
            }
        }
        public void AdcionarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("INSERT INTO Produto(IdProd, Nome, Descricao, Preco, Quantidade) VALUES (@IdProd, @Nome, @Descricao, @Preco, @Quantidade)", conexao);
                cmd.Parameters.AddWithValue("@IdProd", produto.IdProd);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}

using Atividade1.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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

                MySqlCommand cmd = new("INSERT INTO Produtos(Nome, Descricao, Preco, Quantidade) VALUES (@Nome, @Descricao, @Preco, @Quantidade)", conexao);
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
        // Método para listar todos os clientes do banco de dados
        public IEnumerable<Produto> TodosProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Cliente
            List<Produto> Produtolist = new List<Produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'cliente'
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Cliente e preenche suas propriedades com os valores da linha atual
                    Produtolist.Add(
                                new Produto
                                {
                                    IdProd = Convert.ToInt32(dr["IdProd"]), // Converte o valor da coluna "codigo" para inteiro
                                    Nome = ((string)dr["Nome"]), // Converte o valor da coluna "nome" para string
                                });
                }
                // Retorna a lista de todos os clientes
                return Produtolist;
            }
        }

        // Método para buscar um produto específico pelo seu código (Codigo)
        public Produto ObterProduto(int IdProd)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'cliente' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos where IdProd=@IdProd", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@IdProd", IdProd);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto Cliente para armazenar os resultados
                Produto produto = new Produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    // Preenche as propriedades do objeto Cliente com os valores da linha atual
                    produto.IdProd = Convert.ToInt32(dr["IdProd"]);//propriedade Codigo e convertendo para int
                    produto.Nome = (string)(dr["Nome"]); // propriedade Nome e passando string
                }
                // Retorna o objeto Cliente encontrado (ou um objeto com valores padrão se não encontrado)
                return produto;
            }
        }
    }
}

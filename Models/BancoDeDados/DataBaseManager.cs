using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using SoftwareFB_DISTRIBUIDORA.BancoDeDados.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareFB_DISTRIBUIDORA.BancoDeDados
{
    class DataBaseManager
    {
        private static DataBaseManager _instance;
        MySqlConnection conn;
        private static object lockBancoDados = new object();
        string connectionString = "server=localhost;user=root;database=db_fb_distribuidora;port=3306;password=1234";

        public static DataBaseManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataBaseManager();
                return _instance;
            }
        }

        private DataBaseManager()
        {
            // Construtor privado
        }

        #region TbProduto

        public void AdicionarProduto(Produto produto)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Buscar o Id da categoria pelo nome
                    int IdCategoria = ObterIdCategoriaPorNome(produto.Categoria);

                    string query = @"INSERT INTO TbProdutos 
                                    (nomeProduto, idCategoria, precoUnitario, precoVenda, PorcentagemDeLucro, codigoBarra, ativo) 
                                    VALUES 
                                    (@nomeProduto, @idCategoria, @precoUnitario, @precoVenda, @PorcentagemDeLucro, @codigoBarra, @ativo)";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomeProduto", produto.NomeProduto);
                    command.Parameters.AddWithValue("@idCategoria", IdCategoria);
                    command.Parameters.AddWithValue("@precoUnitario", produto.PrecoUnitario);
                    command.Parameters.AddWithValue("@precoVenda", produto.PrecoVenda);
                    command.Parameters.AddWithValue("@PorcentagemDeLucro", produto.PorcentagemDeLucro);
                    command.Parameters.AddWithValue("@codigoBarra", produto.CodigoBarra);
                    command.Parameters.AddWithValue("@ativo", produto.Ativo);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Remover Produto
        public void RemoverProduto(int id)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM TbProdutos WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Atualizar Produto
        public void AtualizarProduto(Produto produto)
        {
            lock (lockBancoDados) 
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Buscar o Id da categoria pelo nome
                    int IdCategoria = ObterIdCategoriaPorNome(produto.Categoria);

                    string query = @"UPDATE TbProdutos SET 
                                    nomeProduto = @nomeProduto, 
                                    idCategoria = @idCategoria, 
                                    precoUnitario = @precoUnitario, 
                                    precoVenda = @precoVenda, 
                                    PorcentagemDeLucro = @PorcentagemDeLucro, 
                                    codigoBarra = @codigoBarra, 
                                    ativo = @ativo 
                                    WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", produto.Id);
                    command.Parameters.AddWithValue("@nomeProduto", produto.NomeProduto);
                    command.Parameters.AddWithValue("@idCategoria", IdCategoria);
                    command.Parameters.AddWithValue("@precoUnitario", produto.PrecoUnitario);
                    command.Parameters.AddWithValue("@precoVenda", produto.PrecoVenda);
                    command.Parameters.AddWithValue("@PorcentagemDeLucro", produto.PorcentagemDeLucro);
                    command.Parameters.AddWithValue("@codigoBarra", produto.CodigoBarra);
                    command.Parameters.AddWithValue("@ativo", produto.Ativo);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Produto> ObterTodosProdutos()
        {
            List<Produto> produtos = new List<Produto>();
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                                SELECT 
                                    p.Id, p.NomeProduto, c.Descricao AS Categoria,
                                    p.PrecoUnitario, p.PrecoVenda, 
                                    p.PorcentagemDeLucro, p.CodigoBarra, p.Ativo
                                FROM TbProdutos p
                                INNER JOIN TbCategoria c ON p.IdCategoria = c.Id";
                    var command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto()
                            {
                                Id = reader.GetInt32("id"),
                                NomeProduto = reader.GetString("nomeProduto"),
                                Categoria = reader.IsDBNull(reader.GetOrdinal("Categoria")) ? "Desconhecida" : reader.GetString(reader.GetOrdinal("Categoria")),
                                PrecoUnitario = reader.GetFloat("precoUnitario"),
                                PrecoVenda = reader.GetFloat("precoVenda"),
                                PorcentagemDeLucro = reader.IsDBNull(reader.GetOrdinal("porcentagemDeLucro")) ? 0 : reader.GetFloat("PorcentagemDeLucro"),
                                CodigoBarra = reader.IsDBNull(reader.GetOrdinal("codigoBarra")) ? 0 : reader.GetInt64("codigoBarra"),

                                Ativo = reader.GetBoolean("ativo")
                            };
                            produtos.Add(produto);
                        }
                    }
                }
            }
            return produtos;
        }

        #endregion

        #region TbUsuario

        public void AdicionarUsuario(Usuario usuario)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO TbUsuarios (login, senha, idPermissao, status) VALUES (@login, @senha, @idPermissao, @status)";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", usuario.Login);
                command.Parameters.AddWithValue("@senha", usuario.Senha);
                command.Parameters.AddWithValue("@idPermissao", usuario.IdPermissao);
                command.Parameters.AddWithValue("@status", usuario.Status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Remover Usuario
        public void RemoverUsuario(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM TbUsuarios WHERE id = @id";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Atualizar Usuario
        public void AtualizarUsuario(Usuario usuario)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE TbUsuarios SET login = @login, senha = @senha, idPermissao = @idPermissao, status = @status WHERE id = @id";

                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", usuario.Id);
                command.Parameters.AddWithValue("@login", usuario.Login);
                command.Parameters.AddWithValue("@senha", usuario.Senha);
                command.Parameters.AddWithValue("@idPermissao", usuario.IdPermissao);
                command.Parameters.AddWithValue("@status", usuario.Status);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region TbComanda

        public void AdicionarComanda(Comanda comanda)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbComandas (nomecliente, numeroMesa, dataAbertura, valorTotal, status) VALUES (@nomecliente, @numeroMesa, @dataAbertura, @valorTotal, @status)";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomecliente", comanda.NomeCliente);
                    command.Parameters.AddWithValue("@numeroMesa", comanda.NumeroMesa);
                    command.Parameters.AddWithValue("@dataAbertura", comanda.DataAbertura);
                    command.Parameters.AddWithValue("@valorTotal", comanda.ValorTotal);
                    command.Parameters.AddWithValue("@status", comanda.Status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Remover Comanda
        public void RemoverComanda(int id)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM TbComandas WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Atualizar Comanda
        public void AtualizarComanda(Comanda comanda)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbComandas SET nomecliente = @nomecliente, " +
                        "numeroMesa = @numeroMesa, " +
                        "dataAbertura = @dataAbertura, " +
                        "valorTotal = @valorTotal, " +
                        "status = @status WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", comanda.Id);
                    command.Parameters.AddWithValue("@nomecliente", comanda.NomeCliente);
                    command.Parameters.AddWithValue("@numeroMesa", comanda.NumeroMesa);
                    command.Parameters.AddWithValue("@dataAbertura", comanda.DataAbertura);
                    command.Parameters.AddWithValue("@valorTotal", comanda.ValorTotal);
                    command.Parameters.AddWithValue("@status", comanda.Status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Comanda> ConsultarComandasAbertas()
        {
            List<Comanda> Comandas = new List<Comanda>();
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM TbComandas";
                    var command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var comanda = new Comanda()
                            {
                                Id = reader.GetInt32("id"),
                                NomeCliente = reader.GetString("nomeCliente"),
                                NumeroMesa = reader.IsDBNull(reader.GetOrdinal("numeroMesa")) ? 0 : reader.GetInt32("numeroMesa"),
                                ValorTotal = reader.GetFloat("valorTotal"),
                                DataAbertura = reader.GetDateTime("dataAbertura"),
                                Status = reader.GetString("status")
                            };

                            if (comanda.Status.Equals("ABERTA"))
                            {
                                Comandas.Add(comanda);
                            }                     
                        }
                    }
                }
            }
            return Comandas;
        }

        public void FecharComanda(int id)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbComandas SET status = 'FECHADA' WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region ComandaItens

        public void AdicionarComandaItem(ComandaItem item)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbComandaItens (idComanda, idProduto, quantidade, horaVenda) VALUES (@idComanda, @idProduto, @quantidade, @horaVenda)";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idComanda", item.IdComanda);
                    command.Parameters.AddWithValue("@idProduto", item.IdProduto);
                    command.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    command.Parameters.AddWithValue("@horaVenda", item.HoraVenda);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            //pode gerar um erro caso o id do produto não esteja registrado, precoTotal NULL
            //'Cannot add or update a child row: a foreign key constraint fails (`db_fb_distribuidora`.`tbcomandaitens`, CONSTRAINT `tbcomandaitens_ibfk_1` FOREIGN KEY (`idComanda`) REFERENCES `tbcomandas` (`id`))'
        }

        public void RemoverComandaItem(int id)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM TbComandaItens WHERE id = @id";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarComandaItem(ComandaItem item) //essa função eu nao vou usar
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbComandaItens SET idComanda = @idComanda, idProduto = @idProduto, quantidade = @quantidade, precoTotal = @precoTotal, horaVenda = @horaVenda WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.Parameters.AddWithValue("@idComanda", item.IdComanda);
                    command.Parameters.AddWithValue("@idProduto", item.IdProduto);
                    command.Parameters.AddWithValue("@quantidade", item.Quantidade);
                    command.Parameters.AddWithValue("@precoTotal", item.PrecoTotal);
                    command.Parameters.AddWithValue("@horaVenda", item.HoraVenda);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Categoria

        public void AdicionarCategoria(Categoria categoria)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbCategoria (descricao) VALUES (@descricao)";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descricao", categoria.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoverCategoria(int id)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM TbCategoria WHERE id = @id";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbCategoria SET descricao = @descricao WHERE id = @id";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", categoria.Id);
                    command.Parameters.AddWithValue("@descricao", categoria.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private int ObterIdCategoriaPorNome(string nomeCategoria)
        {
            lock (lockBancoDados) 
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT Id FROM TbCategoria WHERE descricao = @descricao";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descricao", nomeCategoria);

                    connection.Open();
                    var result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int idCategoria))
                    {
                        return idCategoria;
                    }
                    else
                    {
                        throw new Exception($"Categoria '{nomeCategoria}' não encontrada.");
                    }
                }
            }
        }


        #endregion

        #region TbEstoque

        public void AdicionarEstoque(Estoque estoque)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbEstoque (idProduto, quantidadeDisponivel, idUnidade ,ultimaAlteracao) VALUES (@idProduto, @quantidadeDisponivel, @idUnidade, @ultimaAlteracao)";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idProduto", estoque.IdProduto);
                    command.Parameters.AddWithValue("@idUnidade", estoque.IdUnidade);
                    command.Parameters.AddWithValue("@quantidadeDisponivel", estoque.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@ultimaAlteracao", estoque.UltimaAlteracao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarEstoque(Estoque produto)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbEstoque SET quantidadeDisponivel = @quantidadeDisponivel, " +
                        "idUnidade = @idUnidade, " +
                        "ultimaAlteracao = @ultimaAlteracao WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", produto.Id);
                    command.Parameters.AddWithValue("@quantidadeDisponivel", produto.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@idUnidade", produto.IdUnidade);
                    command.Parameters.AddWithValue("@ultimaAlteracao", produto.UltimaAlteracao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Estoque> ConsultarEstoque()
        {
            List<Estoque> produtos = new List<Estoque>();
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM TbEstoque";
                    var command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Estoque()
                            {
                                Id = reader.GetInt32("id"),
                                IdProduto = reader.GetInt32("idProduto"),
                                QuantidadeDisponivel = reader.GetInt32("quantidadeDisponivel"),
                                IdUnidade = reader.GetInt32("idUnidade"),
                                UltimaAlteracao = reader.GetDateTime("ultimaAlteracao")
                            };
                            produtos.Add(produto);
                        }
                    }
                }
            }
            return produtos;
        }

        public void AtualizarQuantidadeEstoque(Estoque produto)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE TbEstoque SET ultimaAlteracao = @ultimaAlteracao, " +
                        "quantidadeDisponivel = @quantidadeDisponivel WHERE id = @id";

                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", produto.Id);
                    command.Parameters.AddWithValue("@quantidadeDisponivel", produto.QuantidadeDisponivel);
                    command.Parameters.AddWithValue("@ultimaAlteracao", produto.UltimaAlteracao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        
        #endregion

        #region Permissao

        public void AdicionarPermissao(Permissao permissao)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbPermissao (descricao) VALUES (@descricao)";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descricao", permissao.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region TbUnidade

        public void AdicionarUnidade(Unidade unidade)
        {
            lock (lockBancoDados)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO TbUnidade (descricao) VALUES (@descricao)";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descricao", unidade.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}

﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomMotos.Conexao;
using TomMotos.Model;

namespace TomMotos.Classes
{
    class CaixaDAO
    {
        MySqlConnection conexao = ConnectionFactory.getConnection();

        #region METODO CADASTRAR

        public void cadastrar(CaixaModel obj)
        {
            try
            {
                string insert = @"CALL criacaoVenda(@descricao, @validade_orcamento_servico, @preco_mao_de_obra, @fk_veiculo_id, @fk_cliente_id)";

                MySqlCommand executacmdsql = new MySqlCommand(insert, conexao);
                executacmdsql.Parameters.AddWithValue("@descricao", obj.descricao);
                executacmdsql.Parameters.AddWithValue("@validade_orcamento_servico", obj.validade_orcamento_servico);
                executacmdsql.Parameters.AddWithValue("@preco_mao_de_obra", obj.preco_mao_de_obra);
                executacmdsql.Parameters.AddWithValue("@fk_veiculo_id", obj.fk_veiculo_id);
                executacmdsql.Parameters.AddWithValue("@fk_cliente_id", obj.fk_cliente_id);

                conexao.Open();
                executacmdsql.ExecuteNonQuery();
                MessageBox.Show("Cadastrado com sucesso!");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro);
                MessageBox.Show("Cadastrado não Realizado!");
            }
        }


        #endregion

        #region METODO PESQUISAR ULTIMA VENDA

        public void mudarStatusVenda(CaixaModel objVenda, bool status)
        {
            try
            {
                string update = @"CALL mudarStatusVenda(@id_venda, " + status + ");";

                MySqlCommand executacmdsql = new MySqlCommand(update, conexao);
                executacmdsql.Parameters.AddWithValue("@id_venda", objVenda.id_venda);

                conexao.Open();
                executacmdsql.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro);
            }
        }

        #endregion

        #region METODO MUDAR STATUS DA VENDA
        
        public string listarUltimaVenda()
        {
            string resultado = "";
            try
            {
                string select = @"SELECT MAX(id_venda) FROM tb_venda;";

                MySqlCommand executacmdsql = new MySqlCommand(select, conexao);
                conexao.Open();
                resultado = executacmdsql.ExecuteScalar().ToString();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro);
            }
            return resultado;
        }

        #endregion

        #region METODO ALTERAR
        public void alterar()
        {
        }
        #endregion

        #region METODO EXCLUIR
        public void Excluir()
        {
        }
        #endregion
    }
}
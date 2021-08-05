﻿using MySql.Data.MySqlClient;
using System;
using TomMotos.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomMotos.Conexao;

namespace TomMotos.Classes
{
    class ClienteDAO
    {
        MySqlConnection conexao = ConnectionFactory.getConnection();

        public ClienteDAO()
        {
        }

        #region METODO LISTAR
        public DataTable ListarTodosClientes()
        {
            string sql = @"select * from tb_cliente";

            MySqlCommand executacmdsql = new MySqlCommand(sql, conexao);

            conexao.Open();
            executacmdsql.ExecuteNonQuery();

            MySqlDataAdapter da = new MySqlDataAdapter(executacmdsql);

            DataTable tabelaCliente = new DataTable();
            da.Fill(tabelaCliente);

            conexao.Close();

            return tabelaCliente;
        }
        #endregion


        #region METODO CADASTRAR

        public void cadastrar(ClienteModel obj)
        {
            if (obj.nome == "")
            {
                MessageBox.Show("Preencha todos valores Obrigatorio! = *");
            }
            else {

            try
            {
                string insert = @"CALL criacaoCliente(@nome, @sobrenome, @data_nasc ,@cpf, @cnpj)";

                MySqlCommand executacmdsql = new MySqlCommand(insert, conexao);
                executacmdsql.Parameters.AddWithValue("@nome", obj.nome);
                executacmdsql.Parameters.AddWithValue("@sobrenome", obj.sobrenome);
                executacmdsql.Parameters.AddWithValue("@data_nasc", obj.data_nasc);
                executacmdsql.Parameters.AddWithValue("@cpf", obj.cpf);
                executacmdsql.Parameters.AddWithValue("@cnpj", obj.cnpj);

                conexao.Open();
                executacmdsql.ExecuteNonQuery();
                conexao.Close();
                    MessageBox.Show("Cadastrado com sucesso!");
                }
            catch (Exception erro)
                {
                    MessageBox.Show("Cadastrado não Realizado!");
                MessageBox.Show("Erro: " + erro);
            }
            }
        }
        #endregion


        #region METODO ALTERAR
        public void alterar(ClienteModel obj)
        {
            try
            {

                string update = @"Update  tb_cliente set  nome_cliente=@nome, sobrenome_cliente=@sobrenome,data_nascimento_cliente=@data_nasc ,cpf_cliente=@cpf,cnpj_cliente=@cnpj where id_cliente=@id";
                MySqlCommand executacmdsql = new MySqlCommand(update, conexao);
                executacmdsql.Parameters.AddWithValue("@id", obj.id);
                executacmdsql.Parameters.AddWithValue("@nome", obj.nome);
                executacmdsql.Parameters.AddWithValue("@sobrenome", obj.sobrenome);
                executacmdsql.Parameters.AddWithValue("@data_nasc", obj.data_nasc);
                executacmdsql.Parameters.AddWithValue("@cpf", obj.cpf);
                executacmdsql.Parameters.AddWithValue("@cnpj", obj.cnpj);
                conexao.Open();
                executacmdsql.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Aconteceu um Erro" + erro);
            }


        }
        #endregion

    }
}

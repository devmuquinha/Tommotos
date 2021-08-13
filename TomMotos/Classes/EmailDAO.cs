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
    class EmailDAO
    {
        MySqlConnection conexao = ConnectionFactory.getConnection();



        #region METODO CADASTRAR

        public void cadastrarEmail(EmailModel obj)
        {
            int a = 1;
            if (obj.nome == "")
            {
                MessageBox.Show("Preencha todos valores Obrigatorio! = *");
            }
            else
            {
                try
                {
                    string insert = @"CALL criacaoEmail(@nome, @id)";

                    MySqlCommand executacmdsql = new MySqlCommand(insert, conexao);
                    executacmdsql.Parameters.AddWithValue("@nome", obj.nome);
                    executacmdsql.Parameters.AddWithValue("@id", EmailModel.id);

                    conexao.Open();
                    executacmdsql.ExecuteNonQuery();
                    conexao.Close();
                }
                catch (Exception erro)
                {
                    a = 2;
                    MessageBox.Show("Erro: " + erro);
                }
                if (a == 1)
                {
                    MessageBox.Show("Cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Cadastrado não Realizado!");
                }
            }
        }
        #endregion


        #region METODO LISTAR
        public DataTable ListarEmails()
        {


            string sql = @"select * from tb_email where fk_usuario_id = @id";

            conexao.Open();
            MySqlCommand executacmdsql = new MySqlCommand(sql, conexao);
            executacmdsql.Parameters.AddWithValue("@id", EmailModel.id);


            executacmdsql.ExecuteNonQuery();

            MySqlDataAdapter da = new MySqlDataAdapter(executacmdsql);

            DataTable tabelaEmail = new DataTable();
            da.Fill(tabelaEmail);

            conexao.Close();
            return tabelaEmail;

        }

        #endregion
        #region METODO ALTERAR
        public void alterar(EmailModel obj)
        {
            if (obj.nome == "")
            {
                MessageBox.Show("Preencha todos valores Obrigatorio! = *");
            }
            else
            {
                try
                {
                    string update = @"Update tb_email set nome_email=@nome where id_email = @id";

                    MySqlCommand executacmdsql = new MySqlCommand(update, conexao);
                    executacmdsql.Parameters.AddWithValue("@nome", obj.nome);
                    executacmdsql.Parameters.AddWithValue("@id", EmailModel.id_email);

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

        }
        #endregion

        #region METODO EXCLUIR
        public void Excluir(EmailModel obj)
        {
            if (EmailModel.id_email != "")
            {
                var result = MessageBox.Show("Deseja excluir o Telefone " + obj.nome + "?", "EXCLUIR",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {

                    try
                    {

                        string delete = @"Delete from tb_email  where id_email = @id";
                        MySqlCommand executacmdsql = new MySqlCommand(delete, conexao);
                        executacmdsql.Parameters.AddWithValue("@id", EmailModel.id_email);
                        executacmdsql.Parameters.AddWithValue("@nome", obj.nome);

                        conexao.Open();
                        executacmdsql.ExecuteNonQuery();
                        MessageBox.Show("Excluido com Sucesso!");
                        conexao.Close();
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Aconteceu um Erro" + erro);
                        MessageBox.Show("Não foi possivel excluir", "EXCLUIR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }


        #endregion
       }
    }


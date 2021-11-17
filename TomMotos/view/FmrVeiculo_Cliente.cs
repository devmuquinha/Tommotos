﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomMotos.Classes;
using TomMotos.Conexao;
using TomMotos.Model;

namespace TomMotos.view
{
    
    public partial class FmrVeiculo_Cliente : Form
    {
        MySqlConnection conexao = ConnectionFactory.getConnection();
        Fmrcaixa fp;
        public FmrVeiculo_Cliente(Fmrcaixa f)
        {           
            InitializeComponent();
            fp = f;             
        }

        private void FmrVeiculo_Cliente_Load(object sender, EventArgs e)
        {
            conexao.Open();
            VendaDAO Cadastro = new VendaDAO();
            if (CaixaModel.valorPesquisa == "veiculo") {                
                
                cbxBuscar.Items.AddRange(new object[] { "ID","MODELO","MARCA","COR","ANO","KM","PLACA","OBS"});
                Cadastro.ListarTodosVeiculo();
                dg_listarVeiculoOuCliente.DataSource = Cadastro.ListarTodosVeiculo(); }
            else {
                cbxBuscar.Items.AddRange(new object[] { "ID", "NOME", "SOBRENOME", "CPF", "CNPJ"});
                Cadastro.ListarTodosCliente();
                dg_listarVeiculoOuCliente.DataSource = Cadastro.ListarTodosCliente();
            }
            conexao.Close();

            }

        private void dg_listarVeiculoOuCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_listarVeiculoOuCliente_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CaixaModel.valorPesquisa == "veiculo")
            {
                CaixaModel.fk_veiculo_id = dg_listarVeiculoOuCliente.CurrentRow.Cells[0].Value.ToString();
                CaixaModel.fk_cliente_id = dg_listarVeiculoOuCliente.CurrentRow.Cells[8].Value.ToString();

                if (CaixaModel.fk_veiculo_id !="")
                {
                    fp.lbl_BuscarVeiculo.Text = CaixaModel.fk_veiculo_id.ToString();
                    fp.lbl_buscarCliente.Text = CaixaModel.fk_cliente_id.ToString();
                }
                this.Close();
            }
            else 
            {
                CaixaModel.fk_cliente_id = dg_listarVeiculoOuCliente.CurrentRow.Cells[0].Value.ToString();
                if (CaixaModel.fk_cliente_id != "")
                {
                    fp.lbl_buscarCliente.Text = CaixaModel.fk_cliente_id.ToString();
                }
                this.Close();
            }

        }

        private void FmrVeiculo_Cliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            cbxBuscar.Items.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxBuscar.Text != "")
                {
                    string campo = cbxBuscar.Text.ToString() + "_veiculo";
                    if(CaixaModel.valorPesquisa == "veiculo") FiltroModel.filtro = @"select * from tb_veiculo where " + campo.ToLower() + " like " + "'%" + txtBuscar.Text.ToString() + "%'";
                    else FiltroModel.filtro = @"select * from tb_cliente where " + campo.ToLower() + " like " + "'%" + txtBuscar.Text.ToString() + "%'";
                    // MessageBox.Show("Test " + FiltroModel.filtro);
                    FiltroDAO dao = new FiltroDAO();
                    dg_listarVeiculoOuCliente.DataSource = dao.buscaCargo();
                }
            }
            catch (Exception erro) { MessageBox.Show("Ouve um Erro " + erro.Message); }
        }
    }
}

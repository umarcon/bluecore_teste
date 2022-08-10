using bluecore.Dados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bluecore
{
    public partial class frmCadastroProduto : Form
    {
        public frmCadastroProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            IniciaComboGrupo();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = true;
            txtDescricao.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
            cboGrupo.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var listItem = new Item();
            var listResult = new List<Item>();

            listItem.Codigo = txtCodigo.Text;
            listItem.Descricao = txtDescricao.Text;
            listItem.Valor = Convert.ToDouble(txtValor.Text);
            listItem.Qtde = Convert.ToInt32(txtQuantidade.Text);
            listItem.Grupo = Convert.ToInt32(cboGrupo.SelectedIndex) + 1;

            listResult.Add(listItem);

            SqlUtils.InsertItem(listResult);

            btnSalvar.Enabled = false;
            txtCodigo.Enabled = false;
            txtCodigo.Text = string.Empty;
            txtDescricao.Enabled = false;
            txtDescricao.Text = string.Empty;
            txtValor.Enabled = false;
            txtValor.Text = string.Empty;
            txtQuantidade.Enabled = false;
            txtQuantidade.Text = string.Empty;
            cboGrupo.Enabled = false;

            btnSalvar.Enabled = false;
        }

        public void IniciaComboGrupo()
        {
            MySqlConnection cn = new MySqlConnection();

            cn.ConnectionString = (@"server=localhost;database=produtos;pwd=123456;uid=root");
            cn.Open();

            MySqlCommand comm = new MySqlCommand();

            comm.Connection = cn;
            comm.CommandText = "select grupo_codigo from grupo;";

            MySqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cboGrupo.DisplayMember = "grupo_codigo";
            cboGrupo.DataSource = dt;

            cn.Close();

        }
    }
}

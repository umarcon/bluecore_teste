using bluecore.Dados;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bluecore
{
    public class SqlUtils
    {
        private static MySqlConnection mConn;
        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;

        //Servidor de dados
        private const string SqlHost = @"localhost";
        //Usuário do banco de dados
        private const string SqlUser = "root";
        //Senha do usuário
        private const string SqlPass = "123456";
        //Database que irá se conectar
        private const string SqlDb = "produtos";

        public static void Connect()
        {
            //Verifica se o status da conexão é null
            //Este caso precisa ficar porque eu ainda não tenho o objeto connection instanciado pela sqlconnection
            try
            {
                mConn = new MySqlConnection(string.Format(
                "user id={0};password={1};server={2};database={3};connection timeout=60",
                SqlUser,
                SqlPass,
                SqlHost,
                SqlDb));

                //Caso tenha sucesso em se conectar, abre a sessão.
                mConn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexão com o banco não foi aberta. Favor, verificar!");
            }
        }

        public static void InsertItem(List<Item> result)
        {
            try
            {
                Connect();

                foreach (var item in result)
                {
                    var sql = string.Format(@"insert into item (item_codigo, item_descricao, item_valor, item_qtde, id_grupo)
                               values ('{0}', '{1}', {2}, '{3}', '{4}');",
                               item.Codigo, item.Descricao, item.Valor, item.Qtde, item.Grupo);
                    
                    MySqlCommand command = new MySqlCommand(sql, mConn);
                    
                    command.ExecuteNonQuery();
                    
                    mConn.Close();
                    
                    MessageBox.Show("Morador cadastrado com sucesso!");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                mConn.Close();
            }
        }
    }
}

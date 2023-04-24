using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace think
{
    public class InternalSqlCrud
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;attachdbfilename=|DataDirectory|\\db.mdf;Integrated Security=true;User Instance=true;MultipleActiveResultSets=true";
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private SqlDataReader reader;
        private SqlCommand command;

        public InternalSqlCrud() {
            try
            {
                this.connection = new SqlConnection(this.connectionString);
                this.connection.Open();
            }
            catch (SqlException) { 
                //code
            }
        }

        public bool executeCommand(string query){
            try
            {
                this.command = new SqlCommand(query, this.connection);
                this.command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public SqlDataReader executeReader(string query) {
            try
            {
                this.command = new SqlCommand(query, this.connection);
                this.reader = this.command.ExecuteReader();
            }
            catch (SqlException)
            {
                //error
            }
            return this.reader;
        }

        public void fillGrid(string query,GridView view) {
            try
            {
                this.adapter = new SqlDataAdapter(query, this.connection);
                DataSet data = new DataSet();
                this.adapter.Fill(data);
                DataView dataObj = new DataView(data.Tables[0]);
                view.DataSource = dataObj;
                view.DataBind();
            }
            catch (SqlException) { }
        }
    }
}
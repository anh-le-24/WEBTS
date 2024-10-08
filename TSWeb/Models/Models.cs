using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TSWeb.Models
{
    public class Models
    {
        private string connecttionStrings = "workstation id=TSWebDB.mssql.somee.com;packet size=4096;user id=Vanh_SQLLogin_2;pwd=123456789;data source=TSWebDB.mssql.somee.com;persist security info=False;initial catalog=TSWebDB;TrustServerCertificate=True";
        public ArrayList get(String sql)
        {
            ArrayList datalist = new ArrayList();
            SqlConnection connection = new SqlConnection(connecttionStrings);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            using (SqlDataReader r = command.ExecuteReader())
            {
                while (r.Read())
                {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        row.Add(r.GetValue(i).ToString());
                    }
                    datalist.Add(row);

                }
            }
            connection.Close();
            return datalist;
        }
    }
}
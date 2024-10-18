using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace TSWeb.Models
{
    public class DatabaseModels
    {
        private string connectionString = "workstation id=TSWebDaTa.mssql.somee.com;packet size=4096;user id=Vanh_SQLLogin_1;pwd=123456789;data source=TSWebDaTa.mssql.somee.com;persist security info=False;initial catalog=TSWebDaTa;TrustServerCertificate=True"; 

        // Phương thức lấy dữ liệu
        public List<Dictionary<string, object>> GetData(string sql)
        {
            var dataList = new List<Dictionary<string, object>>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            dataList.Add(row);
                        }
                    }
                }
            }
            return dataList;
        }
    }
}

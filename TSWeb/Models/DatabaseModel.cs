using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace TSWeb.Models {
    public class DatabaseModel {
        private string connecttionStrings = "workstation id=TSWebDaTa.mssql.somee.com;packet size=4096;user id=Vanh_SQLLogin_1;pwd=123456789;data source=TSWebDaTa.mssql.somee.com;persist security info=False;initial catalog=TSWebDaTa;TrustServerCertificate=True";
        public List<object> get(string sql, SqlParameter[] parameters = null) {
            List<object> datalist = new List<object>(); // Sử dụng List<object> thay vì ArrayList
            using (SqlConnection connection = new SqlConnection(connecttionStrings)) {
                SqlCommand command = new SqlCommand(sql, connection);

                // Thêm các tham số vào câu lệnh nếu có
                if (parameters != null) {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        List<object> row = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            row.Add(reader.GetValue(i));
                        }
                        datalist.Add(row);
                    }
                }
            }
            return datalist;
        }
    }
}

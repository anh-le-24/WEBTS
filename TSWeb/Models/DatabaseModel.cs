﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TSWeb.Models {
    public class DatabaseModel {
        private string connecttionStrings = "workstation id=TSWebDaTa.mssql.somee.com;packet size=4096;user id=Vanh_SQLLogin_1;pwd=123456789;data source=TSWebDaTa.mssql.somee.com;persist security info=False;initial catalog=TSWebDaTa;TrustServerCertificate=True";
        public ArrayList get(String sql) {
            ArrayList datalist = new ArrayList();
            SqlConnection connection = new SqlConnection(connecttionStrings);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            using (SqlDataReader r = command.ExecuteReader()) {
                while (r.Read()) {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < r.FieldCount; i++) {
                        row.Add(r.GetValue(i).ToString());
                    }
                    datalist.Add(row);

                }
            }
            connection.Close();
            return datalist;
        }
        public void execute(string sql, object parameters) {
            using (SqlConnection connection = new SqlConnection(connecttionStrings))
            using (SqlCommand command = new SqlCommand(sql, connection)) {
                command.CommandType = CommandType.Text; // Use CommandType.StoredProcedure if calling a stored procedure
                connection.Open();

                // Handle parameters
                if (parameters != null) {
                    var paramDict = parameters.GetType().GetProperties();
                    foreach (var param in paramDict) {
                        command.Parameters.AddWithValue("@" + param.Name, param.GetValue(parameters) ?? DBNull.Value);
                    }
                }

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
    }
}
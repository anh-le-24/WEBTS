using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TSWeb.Models
{
    public class DatabaseModel
    {
        private string connectionString = "workstation id=TSWebDaTa.mssql.somee.com;packet size=4096;user id=Vanh_SQLLogin_1;pwd=123456789;data source=TSWebDaTa.mssql.somee.com;persist security info=False;initial catalog=TSWebDaTa;TrustServerCertificate=True";

        // Phương thức get nhận hai tham số: SQL và mảng SqlParameter
        public ArrayList get(string sql, SqlParameter[] parameters)
        {
            ArrayList datalist = new ArrayList();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(parameters); // Thêm các tham số vào câu truy vấn
                connection.Open();

                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ArrayList row = new ArrayList();
                        for (int i = 0; i < r.FieldCount; i++)
                        {
                            row.Add(r.GetValue(i)); // Lưu trữ giá trị của mỗi trường
                        }
                        datalist.Add(row);
                    }
                }

                connection.Close();
            }

            return datalist;
        }

        // Phương thức get chỉ nhận một tham số SQL (giữ nguyên phương thức này)
        public ArrayList get(string sql)
        {
            ArrayList datalist = new ArrayList();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader r = command.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ArrayList row = new ArrayList();
                        for (int i = 0; i < r.FieldCount; i++)
                        {
                            row.Add(r.GetValue(i)); // Lưu trữ giá trị của mỗi trường
                        }
                        datalist.Add(row);
                    }
                }
                connection.Close();
            }
            return datalist;
        }
        public DataTable GetDataTable(string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        var adapter = new SqlDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi nếu cần thiết
                    throw new Exception("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }
        }


    }

}
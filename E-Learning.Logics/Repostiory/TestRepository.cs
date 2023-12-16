using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class TestRepository : ITestRepository
    {
        private readonly string? connection;
        public TestRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("E-Learning");
        }
        public string TestMethod()
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = "SELECT * FROM dbo.Provinsi";
                var province = con.Query<ProvinsiModel>(query).ToList();

                if(province != null)
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "Berhasil mendapatkan data Provinsi",
                        Provinsi = province
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error!!"
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }
            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
                returnedOutput = JsonSerializer.Serialize(responseBody);
            }
            return returnedOutput;
        }
    }
}

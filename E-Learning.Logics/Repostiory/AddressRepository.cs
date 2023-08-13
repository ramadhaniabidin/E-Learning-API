using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AddressRepository: IAddressRepository
    {
        private readonly string? connection;
        public AddressRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("E-Learning");
        }

        public List<ProvinsiModel> GetAllProvinsi()
        {
            using var con = new SqlConnection(connection);
            con.Open();
            var query = "SELECT * FROM dbo.Provinsi";
            var province = con.Query<ProvinsiModel>(query).ToList();

            return province;
        }

        public List<KabupatenModel> GetKabupatenByProvinsiName(string provinsiName)
        {
            using var con = new SqlConnection(connection);
            con.Open();
            var query = @"SELECT * FROM dbo.Kabupaten WHERE idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)";
            var kabupaten = con.Query<KabupatenModel>(query, new {provinsiName}).ToList();
            return kabupaten;
        }
    }
}

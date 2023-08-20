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
    public class AccountDetailRepository: IAccountDetailRepository
    {
        private readonly string? connection;
        public AccountDetailRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("E-Learning");
        }

        public string InsertUpdateDetail(AccountDetailModel accountDetail)
        {
            string returnedOutput = "";
            try
            {
                if(accountDetail != null)
                {
                    using var con = new SqlConnection(connection);
                    con.Open();
                    var query = @"DECLARE @id_akun INT SET @id_akun = (SELECT id FROM dbo.master_akun WHERE username = )";
                }

                else
                {

                }
            }

            catch (Exception ex)
            {

            }

            return returnedOutput;
        }
    }
}

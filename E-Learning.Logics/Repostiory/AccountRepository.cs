using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AccountRepository: IAccountRepository
    {
        private readonly string? connecton;
        public AccountRepository(IConfiguration configuration)
        {
            connecton = configuration.GetConnectionString("E-Learning");
        }

        public string CreateNewAccount(AccountModel account)
        {
            try
            {
                if(account != null)
                {
                    using var con = new SqlConnection(connecton);
                    con.Open();
                    var query = @"INSERT INTO master_akun (username, password, tanggal_daftar, id_peran)
                    VALUES (@username, @password, @tanggal_daftar, @id_peran)";

                    con.Execute(query, account);

                    var result = new
                    {
                        ProcessSucces = true,
                        InfoMessage = "Successfully Created new account"
                    };
                    return JsonSerializer.Serialize(result);
                }

                else
                {
                    var result = new
                    {
                        ProcessSucces = false,
                        InfoMessage = "Error: The inserted data is null"
                    };
                    return JsonSerializer.Serialize(result);
                }


            }
            
            catch (Exception ex)
            {
                var result = new
                {
                    ProcessSuccess = false,
                    InfoMessage = $"Error: {ex.Message}"
                };

                return JsonSerializer.Serialize(result);
            }
        }

        public string GetAccountByUsername(string username)
        {
            var returnedOutput = "";

            try
            {
                using(var con = new SqlConnection(connecton))
                {
                    con.Open();
                    var query = @"SELECT * FROM dbo.master_akun WHERE username = @username";
                    var account = con.QueryFirstOrDefault<AccountModel>(query, new {username});

                    if(account != null)
                    {
                        var result = new
                        {
                            ProcessSuccess = true,
                            InfoMessage = "Account found",
                            account
                        };

                        returnedOutput = JsonSerializer.Serialize(result);
                    }

                    else
                    {
                        var result = new
                        {
                            ProcessSuccess = false,
                            InfoMessage = "Account not found, please insert a valid username"
                        };

                        returnedOutput = JsonSerializer.Serialize(result);
                    }
                }
            }

            catch(Exception ex)
            {
                var result = new
                {
                    ProcessSuccess = false,
                    InfoMessage = $"Error: {ex.Message}"
                };

                returnedOutput = JsonSerializer.Serialize(result);
            }

            return returnedOutput;
        }

        public int GetAccountID(string username, string password)
        {
            using var con = new SqlConnection(connecton);
            con.Open();
            var query = @"SELECT id FROM master_akun WHERE username=@username AND password=@password";
            var id = con.QueryFirstOrDefault<int>(query, new { username, password });
            return id;
        }

        public List<AccountModel> GetAllAccounts()
        {
            using var con = new SqlConnection(connecton);
            con.Open();
            var query = @"SELECT * FROM master_akun";
            var accounts = con.Query<AccountModel>(query).ToList();
            return accounts;
        }
    }
}

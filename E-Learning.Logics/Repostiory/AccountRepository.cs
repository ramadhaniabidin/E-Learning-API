using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AccountRepository: IAccountRepository
    {
        private readonly string? connecton;

        private readonly string JwtKey = "LO6i4DuNxIpmGIpjCPRuPwx1NpA2Deuryh7HOsaw_b0";
        private readonly string JwtIssuer = "https://192.168.1.2:7290";
        private readonly string JwtAudience = "https://192.168.2.16:7290";
        public AccountRepository(IConfiguration configuration)
        {
            connecton = configuration.GetConnectionString("E-Learning");
        }

        public string InsertOrUpdateProfile(AccountDetailModel accountDetail, string token)
        {
            string returnedOutput = "";
            try
            {
                if((accountDetail != null) && (!string.IsNullOrWhiteSpace(token)))
                {
                    TokenValidationParameters validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = JwtIssuer,
                        ValidAudience = JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                    var claims = claimsPrincipal.Claims;

                    if(claims != null)
                    {
                        var accountID = claims.FirstOrDefault(c => c.Type == "AccountID")?.Value;
                        var con = new SqlConnection(connecton);
                        con.Open();

                        
                        var query = @"
                        IF NOT EXISTS (SELECT * FROM dbo.master_detail_akun WHERE id_akun = @accountID)
                        BEGIN
                            INSERT INTO dbo.master_detail_akun (id_akun, email, nama, provinsi, kabupaten, kecamatan, desa, no_telp)
                            VALUES (@accountID, @email, @nama, @provinsi, @kabupaten, @kecamatan, @desa, @no_telp)
                        END
                        ELSE
                        BEGIN
                            UPDATE dbo.master_detail_akun SET 
                            nama = @nama, email = @email, provinsi = @provinsi, kabupaten = @kabupaten, kecamatan = @kecamatan, desa = @desa, no_telp = @no_telp
                            WHERE id_akun = @accountID
                        END";

                        var queryParams = new
                        {
                            accountID = accountID,
                            email = accountDetail.email,
                            nama = accountDetail.nama,
                            provinsi = accountDetail.provinsi,
                            kabupaten = accountDetail.kabupaten,
                            kecamatan = accountDetail.kecamatan,
                            desa = accountDetail.desa,
                            no_telp = accountDetail.no_telp,
                        };

                        con.Execute(query, queryParams);

                        var responseBody = new
                        {
                            Success = true,
                            Message = "Successfully updating your profile"
                        };

                        returnedOutput = JsonSerializer.Serialize(responseBody);
                    }

                    else
                    {
                        var responseBody = new
                        {
                            Success = false,
                            Message = "Invalid credentials"
                        };
                        returnedOutput = JsonSerializer.Serialize(responseBody);
                    }
                    
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error: Periksa kembali parameter serta token Anda"
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

        public int GetAccountID(string username, string password, int role_id)
        {
            using var con = new SqlConnection(connecton);
            con.Open();
            var query = @"SELECT id FROM master_akun WHERE username=@username AND password=@password AND id_peran=@role_id";
            var id = con.QueryFirstOrDefault<int>(query, new { username, password, role_id });
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

        public string UpdatePassword(string username, string newPassword)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connecton);
                con.Open();

                var checkUserExistQuery = "SELECT COUNT(*) FROM dbo.master_akun WHERE username = @username";
                int userCount = con.ExecuteScalar<int>(checkUserExistQuery, new {username});

                if(userCount == 0)
                {
                    var resultBody = new
                    {
                        ProcessSuccess = false,
                        InfoMessage = "Username not found"
                    };

                    returnedOutput = JsonSerializer.Serialize(resultBody);
                }

                else
                {
                    var query = "UPDATE dbo.master_akun SET password = @newPassword WHERE username = @username";
                    con.Execute(query, new { newPassword, username });

                    var resultBody = new
                    {
                        ProcessSuccess = true,
                        InfoMessage = "Successfully resetting your password"
                    };

                    returnedOutput = JsonSerializer.Serialize(resultBody);
                }


            }

            catch (Exception ex)
            {
                var resultBody = new
                {
                    ProcessSuccess = false,
                    InfoMessage = $"Error: {ex.Message}"
                };

                returnedOutput = JsonSerializer.Serialize(resultBody);
            }

            return returnedOutput;
        }

        public string GetProfileByToken(string token)
        {
            var returnedOutput = "";
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    var accountID = GetAccountIdByToken(token);
                    var con = new SqlConnection(connecton);
                    con.Open();
                    var query = @"SELECT * FROM dbo.master_detail_akun WHERE id_akun = @accountID";

                    var accountDetail = con.QueryFirstOrDefault<AccountDetailModel>(query, new {accountID});

                    if(accountDetail != null)
                    {
                        var responseBody = new
                        {
                            Success = true,
                            Message = "Ok",
                            accountDetail
                        };

                        returnedOutput = JsonSerializer.Serialize(responseBody);
                    }
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error: Please check your token"
                        
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }
            catch(Exception ex)
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

        public string GetAccountIdByToken(string token)
        {
            string? output = "";
            if (!string.IsNullOrEmpty(token))
            {
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JwtIssuer,
                    ValidAudience = JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var claims = claimsPrincipal.Claims;

                if (claims != null)
                {
                    output = claims.FirstOrDefault(c => c.Type == "AccountID")?.Value;
                }

                else
                {
                    output = "";
                }
            }

            else
            {
                output = "";
            }

            return output;
        }
    }
}

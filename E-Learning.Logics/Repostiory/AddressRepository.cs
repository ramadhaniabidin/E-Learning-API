using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AddressRepository: IAddressRepository
    {
        private readonly string? connection;
        public AddressRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("connstring_OnPC");
        }

        public string FilterDesa(FilterDesa body)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"DECLARE @idProv INT SET @idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)
                DECLARE @idKab INT SET @idKab = (SELECT id FROM dbo.Kabupaten WHERE namaKabupaten = @kabupatenName AND idProv = @idProv)
                DECLARE @idKec INT SET @idKec = (SELECT id FROM dbo.Kecamatan WHERE namaKecamatan = @kecamatanName AND @idKab = @idKab)

                SELECT * FROM dbo.Desa WHERE idKec = @idKec AND namaDesa LIKE '%' + @desa + '%'";
                var queryParams = new
                {
                    provinsiName = body.provinsi,
                    kabupatenName = body.kabupaten,
                    kecamatanName = body.kecamatan,
                    desa = body.desa,
                };

                var desa = con.Query<DesaModel>(query, queryParams).ToList();
                if((desa != null) && (desa.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "OK",
                        Desa = desa
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = $"Tidak ada desa yang mengandung huruf {body.desa}",
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

            }
            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                };

                returnedOutput = JsonSerializer.Serialize(responseBody);
            }
            return returnedOutput;
        }

        public string FilterKabupaten(FilterKabupatenBody body)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"SELECT * FROM dbo.Kabupaten WHERE idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsi) 
                            AND namaKabupaten LIKE '%' + @kabupaten + '%'";
                var queryParams = new
                {
                    provinsi = body.provinsi,
                    kabupaten = body.kabupaten,
                };

                var kabupaten = con.Query<KabupatenModel>(query, queryParams).ToList();

                if((kabupaten != null) && (kabupaten.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "OK",
                        Kabupaten = kabupaten
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = $"Tidak ada kabupaten yang mengandung huruf {body.kabupaten}",
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }
            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                };

                returnedOutput = JsonSerializer.Serialize(responseBody);
            }
            return returnedOutput;
        }

        public string FilterKecamatan(FilterKecamatan body)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"DECLARE @idProv INT
                SET @idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)
                DECLARE @idKab INT
                SET @idKab = (SELECT id FROM dbo.Kabupaten WHERE namaKabupaten = @kabupatenName AND idProv = @idProv)
                SELECT * FROM dbo.Kecamatan WHERE idKab = @idKab AND namaKecamatan LIKE '%' + @kecamatan + '%'";

                var queryParams = new
                {
                    provinsiName = body.provinsi,
                    kabupatenName = body.kabupaten,
                    kecamatan = body.kecamatan,
                };

                var kecamatan = con.Query<KecamatanModel>(query, queryParams).ToList();
                if((kecamatan.Count > 0) && (kecamatan != null))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "OK",
                        Kecamatan = kecamatan
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = $"Tidak ada kecamatan dengan yang mengandung huruf {body.kecamatan}",
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }


                
            }
            catch(Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {body.kecamatan}",
                };
                returnedOutput = JsonSerializer.Serialize(responseBody);
            }
            return returnedOutput;
        }

        public string FilterProvinsi(FilterProvinsiBody body)
        {
            string returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"SELECT * FROM dbo.Provinsi WHERE namaProvinsi LIKE '%' + @provinsi + '%'";
                var prov = con.Query<ProvinsiModel>(query, new {body.provinsi}).ToList();
                if((prov != null) && (prov.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "OK",
                        Provinsi = prov
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);

                }
                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = $"Tidak ada Provinsi yang mengandung huruf {body.provinsi}",
                    };
                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }

            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                };
                returnedOutput = JsonSerializer.Serialize(responseBody);
            }

            return returnedOutput;
        }

        public string GetAllDesa(string provinsiName, string kabupatenName, string kecamatanName)
        {
            string returnedOutput = "";

            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"DECLARE @idProv INT SET @idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)
                DECLARE @idKab INT SET @idKab = (SELECT id FROM dbo.Kabupaten WHERE namaKabupaten = @kabupatenName AND idProv = @idProv)
                DECLARE @idKec INT SET @idKec = (SELECT id FROM dbo.Kecamatan WHERE namaKecamatan = @kecamatanName AND @idKab = @idKab)

                SELECT * FROM dbo.Desa WHERE idKec = @idKec";

                var desa = con.Query<DesaModel>(query, new { provinsiName, kabupatenName, kecamatanName }).ToList();
                if((desa != null) && (desa.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "OK",
                        desa
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error, mohon periksa kembali parameter yang Anda masukkan",
                        
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }
            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = true,
                    Message = $"Error: {ex.Message}",
                };

                returnedOutput = JsonSerializer.Serialize(responseBody);
            }


            return returnedOutput;
        }

        public string GetAllProvinsi(PopUp_Model model)
        {
            var returnedOutput = "";

            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var param = new
                {
                    pageSize = model.PageSize,
                    pageIndex = model.PageIndex,
                    searchValue = model.SearchValue
                };
                var query = $@"
                SELECT * FROM (
                 SELECT *, ROW_NUMBER() OVER (ORDER BY namaProvinsi ASC) AS rowNum, 
                 ((SELECT COUNT(*) FROM (
                  SELECT * FROM Provinsi WHERE namaProvinsi LIKE '%' + @searchValue + '%'
                 ) AS SubQuery) / @pageSize) + 1 AS totalPage
                 FROM Provinsi WHERE namaProvinsi LIKE '%' + @searchValue + '%'
                ) 
                AS SubQuery
                WHERE rowNum > (@pageSize * (@pageIndex - 1)) 
                AND rowNum <= (@pageSize * @pageIndex)";
                Console.WriteLine(query);
                var province = con.Query<ProvinsiModel>(query, param).ToList();
                con.Close();
                if((province != null) && (province.Count > 0))
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
                        Message = "Error!"
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

            //return province;
            return returnedOutput;
        }

        public string GetKabupatenByProvinsiName(string provinsiName)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"SELECT * FROM dbo.Kabupaten WHERE idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)";
                var kabupaten = con.Query<KabupatenModel>(query, new { provinsiName }).ToList();

                if((kabupaten != null) && (kabupaten.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "Berhasil mendapatkan data Kabupaten/Kota",
                        kabupaten
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error!, mohon periksa kembali parameter yang Anda masukkan"
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }
            }

            catch (Exception ex)
            {
                var responseBody = new
                {
                    Success = true,
                    Message = $"Error: {ex.Message}"
                };

                returnedOutput = JsonSerializer.Serialize(responseBody);
            }
            return returnedOutput;
        }

        public string GetKecamatan(string provinsiName, string kabupatenName)
        {
            var returnedOutput = "";
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var query = @"DECLARE @idProv INT
                SET @idProv = (SELECT id FROM dbo.Provinsi WHERE namaProvinsi = @provinsiName)
                DECLARE @idKab INT
                SET @idKab = (SELECT id FROM dbo.Kabupaten WHERE namaKabupaten = @kabupatenName AND idProv = @idProv)
                SELECT * FROM dbo.Kecamatan WHERE idKab = @idKab";

                var kecamatan = con.Query<KecamatanModel>(query, new { provinsiName, kabupatenName }).ToList();

                if((kecamatan != null) && (kecamatan.Count > 0))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "Berhasil mendapatkan data Kabupaten/Kota",
                        kecamatan
                    };

                    returnedOutput = JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error!, mohon periksa kembali parameter yang Anda masukkan"
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

        public string GetPopUpData(PopUp_Model model)
        {
            try
            {
                using var con = new SqlConnection(connection);
                con.Open();
                var param = new
                {
                    tableName = model.TableName,
                    searchColumn = model.SearchColumn,
                    searchValue = model.SearchValue,
                    pageIndex = model.PageIndex,
                    pageSize = model.PageSize,
                    searchType = model.SearchType
                };
                IEnumerable<dynamic> data = null;
                var query = "EXEC dbo.GetPopUpData @tableName, @searchColumn, @searchValue, @pageIndex, @pageSize, @searchType";
                if (model.TableName.ToLower().Contains("provinsi"))
                {
                    data = con.Query<ProvinsiModel>(query, param).ToList();
                }
                else if (model.TableName.ToLower().Contains("kabupaten"))
                {
                    data = con.Query<KabupatenModel>(query, param).ToList();
                }
                //var province = con.Query<ProvinsiModel>(query, param).ToList();
                con.Close();
                if ((data != null) && (data.Any()))
                {
                    var responseBody = new
                    {
                        Success = true,
                        Message = "Berhasil mendapatkan data Provinsi",
                        Data = data
                    };

                    return JsonSerializer.Serialize(responseBody);
                }

                else
                {
                    var responseBody = new
                    {
                        Success = false,
                        Message = "Error!"
                    };

                    return JsonSerializer.Serialize(responseBody);
                }

            }

            catch(Exception ex)
            {
                var responseBody = new
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
                return JsonSerializer.Serialize(responseBody);
            }
        }
    }
}

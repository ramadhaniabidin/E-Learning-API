using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class AccountDetailModel
    {
        public int id {get;set;}

        [JsonPropertyName("id_akun")]
        public int id_akun { get;set;}

        [JsonPropertyName("email")]
        public string? email { get;set;}

        [JsonPropertyName("nama")]
        public string? nama { get;set;}

        [JsonPropertyName("provinsi")]
        public string? provinsi { get;set;}

        [JsonPropertyName("kabupaten")]
        public string? kabupaten { get;set;}

        [JsonPropertyName("kecamatan")]
        public string? kecamatan { get;set;}

        [JsonPropertyName("desa")]
        public string? desa { get;set;}

        [JsonPropertyName("no_telp")]
        public string? no_telp { get;set;}
    }
}

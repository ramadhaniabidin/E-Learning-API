using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class AccountModel
    {
        public int id {  get; set; }

        [JsonPropertyName("username")]
        public string? username { get; set; }
        [JsonPropertyName("password")]
        public string? password { get; set; }
        [JsonPropertyName("id_peran")]
        public int id_peran { get; set; }

        [JsonPropertyName("tanggal_daftar")]
        public DateTime? tanggal_daftar { get; set; }
    }
}

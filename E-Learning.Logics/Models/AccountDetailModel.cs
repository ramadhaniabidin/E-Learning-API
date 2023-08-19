using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class AccountDetailModel
    {
        public int id {get;set;}
        public int id_akun { get;set;}
        public string? email { get;set;}
        public string? nama { get;set;}
        public string? provinsi { get;set;}
        public string? kabupaten { get;set;}
        public string? kecamatan { get;set;}
        public string? desa { get;set;}
        public string? no_telp { get;set;}
    }
}

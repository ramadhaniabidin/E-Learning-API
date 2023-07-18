using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class AccountModel
    {
        public int id {  get; set; }
        public string? username { get; set; }
        public string? password { get; set; }   
        public DateTime? tanggal_daftar { get; set; }
        public int id_peran { get; set; }
    }
}

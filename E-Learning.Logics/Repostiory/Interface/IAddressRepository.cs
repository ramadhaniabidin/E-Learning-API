﻿using E_Learning.Logics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory.Interface
{
    public interface IAddressRepository
    {
        public string GetAllProvinsi();
        public string GetKabupatenByProvinsiName(string provinsiName);
        public string GetKecamatan(string provinsiName, string kabupatenName);
        public List<DesaModel> GetAllDesa(string provinsiName, string kabupatenName, string kecamatanName);
    }
}

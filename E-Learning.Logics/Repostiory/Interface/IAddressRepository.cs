using E_Learning.Logics.Models;
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
        public string FilterProvinsi(FilterProvinsiBody body);
        public string FilterKabupaten(FilterKabupatenBody body);
        public string GetKabupatenByProvinsiName(string provinsiName);
        public string GetKecamatan(string provinsiName, string kabupatenName);
        public string GetAllDesa(string provinsiName, string kabupatenName, string kecamatanName);
    }
}

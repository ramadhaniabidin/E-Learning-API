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
        public List<ProvinsiModel> GetAllProvinsi();
        public List<KabupatenModel> GetKabupatenByProvinsiName(string provinsiName);
        public List<KecamatanModel> GetKecamatan(string provinsiName, string kabupatenName);
        public List<DesaModel> GetAllDesa(string provinsiName, string kabupatenName, string kecamatanName);
    }
}

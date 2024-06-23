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
        public string GetAllProvinsi(PopUp_Model model);
        public string GetPopUpData(PopUp_Model model);
        public string FilterProvinsi(FilterProvinsiBody body);
        public string FilterKabupaten(FilterKabupatenBody body);
        public string FilterKecamatan(FilterKecamatan body);
        public string FilterDesa(FilterDesa body);
        public string GetKabupatenByProvinsiName(string provinsiName);
        public string GetKecamatan(string provinsiName, string kabupatenName);
        public string GetAllDesa(string provinsiName, string kabupatenName, string kecamatanName);
        //public string PopUp_Alamat();
    }
}

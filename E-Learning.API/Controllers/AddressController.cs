using E_Learning.Logics.Repostiory.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers
{
    [Route("E-LearningAPI/[controller]/[action]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public IActionResult GetAllProvince()
        {
            var province = _addressRepository.GetAllProvinsi();
            return Ok(province);
        }

        [HttpGet("provinsiName/{provinsi}")]
        public IActionResult FilterProvinsi(string provinsi)
        {
            var province = _addressRepository.FilterProvinsi(provinsi);
            return Ok(province);
        }


        [HttpGet("provinsiName/{provinsiName}")]
        public IActionResult ListKabupatenByProvinsiName(string provinsiName) 
        {
            var kabupaten = _addressRepository.GetKabupatenByProvinsiName(provinsiName);
            return Ok(kabupaten);
        }

        [HttpGet("provinsi/{provinsi}/kabupaten/{kabupaten}")]
        public IActionResult GetAllKecamatan(string provinsi, string kabupaten) 
        {
            var kecamatan = _addressRepository.GetKecamatan(provinsi, kabupaten);
            return Ok(kecamatan);
        }

        [HttpGet("provinsi/{provinsi}/kabupaten/{kabupaten}/kecamatan/{kecamatan}")]
        public IActionResult GetAllDesa(string provinsi, string kabupaten, string kecamatan)
        {
            var desa = _addressRepository.GetAllDesa(provinsi, kabupaten, kecamatan);
            return Ok(desa);
        }
    }
}

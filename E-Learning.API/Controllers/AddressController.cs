using E_Learning.Logics.Models;
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

        [HttpPost]
        public IActionResult FilterProvinsi([FromBody]FilterProvinsiBody body)
        {
            var province = _addressRepository.FilterProvinsi(body);
            return Ok(province);
        }

        [HttpPost]
        public IActionResult FilterKabupaten([FromBody] FilterKabupatenBody body)
        {
            var kabupaten = _addressRepository.FilterKabupaten(body);
            return Ok(kabupaten);
        }

        [HttpPost]
        public IActionResult FilterKecamatan([FromBody] FilterKecamatan body)
        {
            var kecamatan = _addressRepository.FilterKecamatan(body);
            return Ok(kecamatan);
        }

        [HttpPost]
        public IActionResult FilterDesa([FromBody] FilterDesa body)
        {
            var desa = _addressRepository.FilterDesa(body);
            return Ok(desa);
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

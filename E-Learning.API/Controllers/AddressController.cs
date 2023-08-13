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

    }
}

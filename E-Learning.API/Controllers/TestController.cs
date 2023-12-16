using E_Learning.Logics.Repostiory.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers
{
    [Route("E-learningAPI/[controller]/[action]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        [HttpGet]
        public IActionResult TestMethod()
        {
            var response = _testRepository.TestMethod();
            return Ok(response);
        }
    }
}

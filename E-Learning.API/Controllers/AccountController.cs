using E_Learning.Logics.Repostiory.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.API.Controllers
{
    [Route("E-learningAPI/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var res = _accountRepository.GetAllAccounts();
            return Ok(res);
        }

        [HttpGet("username/{username}/password/{password}")]
        public IActionResult GetAccountID(string username, string password)
        {
            var res = _accountRepository.GetAccountID(username, password);
            return Ok(res);
        }
    }
}

using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("username/{username}")]
        public IActionResult GetAccountByUsername(string username)
        {
            var account = _accountRepository.GetAccountByUsername(username);
            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount( AccountModel model)
        {
            var result = _accountRepository.CreateNewAccount(model);
            Console.WriteLine("Received Data: " + JsonConvert.SerializeObject(model));
            return Ok(result);
        }

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

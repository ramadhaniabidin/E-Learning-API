using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Learning.API.Controllers
{
    [Route("E-learningAPI/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        

        private readonly string JwtKey = "LO6i4DuNxIpmGIpjCPRuPwx1NpA2Deuryh7HOsaw_b0";
        private readonly string JwtIssuer = "https://192.168.1.2:7290";
        private readonly string JwtAudience = "https://192.168.2.16:7290";
        
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost("username/{username}/password/{password}")]
        public IActionResult GetLoginToken(string username, string password)
        {
            bool falseCondition = (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password));
            if(!falseCondition)
            {
                var accountID = _accountRepository.GetAccountID(username, password);
                if(accountID != 0)
                {
                    var claims = new[]
                    {
                        new Claim("Username", username),
                        new Claim("Password", password),
                        new Claim("AccountID", accountID.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                    var login = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        JwtIssuer,
                        JwtAudience,
                        claims, expires: DateTime.UtcNow.AddHours(3), signingCredentials:login
                        );

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(jwtToken);
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPost("username/{username}/newPassword/{newPassword}")]
        public IActionResult UpdatePassword(string username, string newPassword)
        {
            var result = _accountRepository.UpdatePassword(username, newPassword);
            return Ok(result);
        }


        [HttpGet("username/{username}")]
        public IActionResult GetAccountByUsername(string username)
        {
            var account = _accountRepository.GetAccountByUsername(username);
            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountModel model)
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
            //HttpContext.Session.SetString($"User:{username}", username);
            //HttpContext.Session.SetString($"Password:{password}", password);
            var res = _accountRepository.GetAccountID(username, password);
            return Ok(res);
        }

        [HttpPost("token/{token}")]
        public IActionResult GetAccountIdByToken(string token)
        {
            if(!string.IsNullOrEmpty(token))
            {
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JwtIssuer,
                    ValidAudience = JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var claims = claimsPrincipal.Claims;

                var accountID = claims.FirstOrDefault(c => c.Type == "AccountID")?.Value;

                return Ok(accountID);
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPost("token/{token}")]
        public IActionResult InsertOrUpdateProfile(AccountDetailModel model, string token)
        {
            var result = _accountRepository.InsertOrUpdateProfile(model, token);
            return Ok(result);
        }
    }
}

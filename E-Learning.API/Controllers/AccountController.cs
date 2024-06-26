﻿using E_Learning.Logics.Models;
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

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpModel model)
        {
            var response = _accountRepository.SignUp(model);
            return Ok(response);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetAccountData(int id)
        {
            var response = _accountRepository.GetAccountData(id);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult UpdateProvile([FromBody]AccountModel model)
        {
            var response = _accountRepository.SaveUpdateProfile(model);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetLoginToken([FromBody] AuthModel model)
        {
            var response = _accountRepository.GenerateLoginToken(model.username, model.password, model.role_id);
            return Ok(response);
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

        [HttpGet("username/{username}/password/{password}/role_id/{role_id}")]
        public IActionResult GetAccountID(string username, string password, int role_id)
        {
            var res = _accountRepository.GetAccountID(username, password, role_id);
            return Ok(res);
        }

        [HttpGet("{token}")]
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

        [HttpGet("token/{token}")]
        public IActionResult GetAccountDetailByToken(string token)
        {
            var result = _accountRepository.GetProfileByToken(token);
            return Ok(result);
        }
    }
}

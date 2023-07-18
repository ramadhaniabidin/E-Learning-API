using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AuthRepository: IAuthRepository
    {
        private readonly string? connection;
        public AuthRepository(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("E-Learning");
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

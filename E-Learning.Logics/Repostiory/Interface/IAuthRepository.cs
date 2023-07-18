using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory.Interface
{
    public interface IAuthRepository
    {
        public bool Login(string username, string password);
        public bool SignUp(string username, string password);
    }
}

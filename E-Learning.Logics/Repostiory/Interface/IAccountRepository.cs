using E_Learning.Logics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory.Interface
{
    public interface IAccountRepository
    {
        public List<AccountModel> GetAllAccounts();
        public int GetAccountID(string username, string password);
        public string CreateNewAccount(AccountModel account);
        public string GetAccountByUsername(string username);
    }
}

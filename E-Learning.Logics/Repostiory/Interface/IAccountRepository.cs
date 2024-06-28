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
        public int GetAccountID(string username, string password, int role_id);
        public string CreateNewAccount(AccountModel account);
        public string InsertOrUpdateProfile(AccountDetailModel accountDetail, string token);
        public string GetAccountByUsername(string username);
        public string UpdatePassword(string username, string newPassword);
        public string GetProfileByToken(string token);
        public string GetAccountIdByToken(string token);
        public string GenerateLoginToken(string username, string password, int role_id);
        public string SignUp(SignUpModel model);
        public bool CheckEmailExists(string email);
        public string GetAccountData(int account_id);
        public string SaveUpdateProfile(AccountModel account);
    }
}

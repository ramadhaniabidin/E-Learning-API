using E_Learning.Logics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory.Interface
{
    public interface IAccountDetailRepository
    {
        public string InsertUpdateDetail(AccountDetailModel accountDetail);
    }
}

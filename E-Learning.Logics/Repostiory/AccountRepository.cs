﻿using Dapper;
using E_Learning.Logics.Models;
using E_Learning.Logics.Repostiory.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Logics.Repostiory
{
    public class AccountRepository: IAccountRepository
    {
        private readonly string? connecton;
        public AccountRepository(IConfiguration configuration)
        {
            connecton = configuration.GetConnectionString("E-Learning");
        }

        public List<AccountModel> GetAllAccounts()
        {
            using var con = new SqlConnection(connecton);
            con.Open();
            var query = @"SELECT * FROM master_akun";
            var accounts = con.Query<AccountModel>(query).ToList();
            return accounts;
        }
    }
}
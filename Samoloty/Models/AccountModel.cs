using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samoloty.Models
{
    public class AccountModel
    {
        private List<Account> listAccounts = new List<Account>();

        public AccountModel()
        {
            listAccounts.Add(new Account
            {
                UserName = "superadmin",
                Password = "123",
                Roles = new string[] { "superadmin" }
            });
            listAccounts.Add(new Account
            {
                UserName = "admin",
                Password = "123",
                Roles = new string[] { "admin" }
            });
            listAccounts.Add(new Account
            {
                UserName = "pilot",
                Password = "123",
                Roles = new string[] { "pilot" }
            });
        }

        public Account find(string username)
        {
            return listAccounts.Where(acc => acc.UserName.Equals(username)).FirstOrDefault(); ;
        }

        public Account login(string username, string password)
        {
            return listAccounts.Where(acc => acc.UserName.Equals(username) && acc.Password.Equals(password)).FirstOrDefault();
        }
    }
}
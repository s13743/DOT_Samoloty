using Samoloty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Samoloty.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private Account Account;

        public CustomPrincipal(Account account)
        {
            Account = account;
            Identity = new GenericIdentity(account.UserName);
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => Account.Roles.Contains(r));
        }
    }
}

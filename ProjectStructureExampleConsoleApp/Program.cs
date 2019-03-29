using BusinessModel;
using BusinessModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace ProjectStructureExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // change depending on machine
            ClientAuthentication.baseWebAddress = "http://localhost:51132/";
            // checks the authentication login username and password
            if (ClientAuthentication.login("bowles.lionie@itsligo.ie", "LBowles$1"))
            {
                // prints token and login status
                Console.WriteLine("Successful login Token acquired {0} user status is {1}", ClientAuthentication.AuthToken,ClientAuthentication.AuthStatus.ToString());
                try
                {
                    // tries to get the model view
                    AccountUserViewModel acuvm = ClientAuthentication.getItem<AccountUserViewModel>("api/AccountManager/getAccounts");
                    // if successful
                    if (acuvm != null)
                    {
                        // print the account names
                        Console.WriteLine("Got {0} accounts for current logged in user {1}", acuvm.accounts.Count(), acuvm.AccountManagerName);
                        // Get account list using current UserID
                        List<Account> accounts = ClientAuthentication.getList<Account>("api/AccountManager/getAccountsForCurrentManager/" + acuvm.AccountManagerID);
                        foreach (var item in accounts)
                        {
                            Console.WriteLine("Account Name {0}", item.AccountName);
                        }
                    }


                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error {0} --> {1}", ex.Message, ex.InnerException.Message);
                }
                Console.ReadKey();
            }

        }
    }
}

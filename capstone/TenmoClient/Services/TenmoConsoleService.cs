using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            PrintTenmoWelcomeBanner();
            //Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("\t1: Login");
            Console.WriteLine("\t2: Register");
            Console.WriteLine("\t0: Exit");
            Console.WriteLine("----------------------------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            PrintTenmoBanner();
            Console.WriteLine($" Hello, {username}!");
            Console.WriteLine(" 1: View your current balance");
            Console.WriteLine(" 2: View your past transfers");
            Console.WriteLine(" 3: View your pending requests");
            Console.WriteLine(" 4: Send TE bucks");
            Console.WriteLine(" 5: Request TE bucks");
            Console.WriteLine(" 6: Log out");
            Console.WriteLine(" 0: Exit");
            Console.WriteLine("----------------------------"); ;
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        // print out account balance
        public void PrintAccountBalance(decimal balance)
        {
            Console.WriteLine($"Your current account balance is {string.Format("{0:C}", balance)}");
        }

        public void PrintUsers(List<User> users)
        {
            Console.WriteLine(" Id    |  User");
            Console.WriteLine("===============");
            foreach(User user in users)
            {
                Console.WriteLine($" {user.UserId}  |  {user.Username}");
            }
        }

        public void PrintTransfers(List<Transfer> transfers, int currentUserId, int transferStatusId)
        {


            if (transferStatusId == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("==========================================================");
                Console.WriteLine("|Transfer Id    |From/To\t\t|Amount");
                Console.WriteLine("==========================================================");
                Console.ResetColor();

                //to print APPROVED Transactions
                foreach (Transfer transfer in transfers)
                {
                    if (transfer.TransferStatusId == 2)
                    {
                        if (currentUserId == transfer.ToUserId)
                        {
                            Console.WriteLine($"|{transfer.TransferId}           |From: {transfer.FromUsername}\t\t| {string.Format("{0:C}", transfer.Amount)}");
                        }
                        if (currentUserId == transfer.FromUserId)
                        {
                            Console.WriteLine($"|{transfer.TransferId}           |To:   {transfer.ToUsername}\t\t| {string.Format("{0:C}", transfer.Amount)}");
                        }
                    }
                }
            }
            else if (transferStatusId == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("==========================================================");
                Console.WriteLine("\t\tPending Transfers");
                Console.WriteLine("|Transfer Id    |To\t\t|Amount");
                Console.WriteLine("==========================================================");
                Console.ResetColor();
                //to print PENDING Transactions
                foreach (Transfer transfer in transfers)
                {
                    if (transfer.TransferStatusId == 1)
                    {
                        //if (currentUserId == transfer.ToUserId)
                        //{
                        //    Console.WriteLine($"|{transfer.TransferId}           |From: {transfer.FromUsername}\t\t| {string.Format("{0:C}", transfer.Amount)}");
                        //}
                        if (currentUserId == transfer.FromUserId)
                        {
                            Console.WriteLine($"|{transfer.TransferId}           | {transfer.ToUsername}\t\t| {string.Format("{0:C}", transfer.Amount)}");
                        }
                    }
                }
            }
            



        }

        public void PrintTransferDetails (Transfer transfer)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("==================");
            Console.WriteLine(" Transfer Details");
            Console.WriteLine("==================");
            Console.ResetColor();
            Console.WriteLine($"Id:\t{transfer.TransferId}");
            Console.WriteLine($"From:\t{transfer.FromUsername}");
            Console.WriteLine($"To:\t{transfer.ToUsername}");
            Console.WriteLine($"Type:\t{transfer.TransferTypeDesc}");
            Console.WriteLine($"Status:\t{transfer.TransferStatusDesc}");
            Console.WriteLine($"Amount:\t{string.Format("{0:C}", transfer.Amount)}");
            Console.WriteLine();

        }
         public void PrintTenmoBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("****************************");
            Console.Write("* ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ___  __ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("           _    *");
            Console.Write("*   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|  |_  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("|\\ | |\\/| / \\   *");
            Console.Write("*   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|  |__ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("| \\| |  | \\_/   *");
            Console.WriteLine("*                          *");
            Console.WriteLine("****************************");
            Console.ResetColor();

        }
        public void PrintTenmoWelcomeBanner()
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("****************************");
            Console.WriteLine("*        Welcome To        *");
            Console.Write("* ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ___  __ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("           _    *");
            Console.Write("*   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|  |_  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("|\\ | |\\/| / \\   *");
            Console.Write("*   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|  |__ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("| \\| |  | \\_/   *");
            Console.WriteLine("*                          *");
            Console.WriteLine("****************************");
            Console.ResetColor();

        }


    }
}

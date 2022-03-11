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
            Console.WriteLine($"Your current account balance is ${balance}");
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

        public void PrintTransfers(List<Transfer> transfers)
        {
            Console.WriteLine(" Transfer Id | From\t\t| To\t\t| Amount");
            Console.WriteLine("=============================");
            foreach(Transfer transfer in transfers)
            {
                Console.WriteLine($"     {transfer.TransferId}\t| {transfer.FromUsername}\t\t| {transfer.ToUsername}\t\t| {transfer.Amount}");
            }
        }

        public void PrintTransferDetails (Transfer transfer)
        {
            Console.WriteLine("Transfer Details");
            Console.WriteLine("================");
            Console.WriteLine($"Id: {transfer.TransferId}");
            Console.WriteLine($"From: {transfer.FromUsername}");
            Console.WriteLine($"To: {transfer.ToUsername}");
            Console.WriteLine($"Type: {transfer.TransferTypeDesc}");
            Console.WriteLine($"Status: {transfer.TransferStatusDesc}");
            Console.WriteLine($"Amount: ${transfer.Amount}");

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

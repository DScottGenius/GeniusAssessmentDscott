using GeniusAssessmentDscott.Commands;
using GeniusAssessmentDscott.CSV_Reader;
using GeniusAssessmentDscott.Entities;
using System;

namespace GeniusAssessmentDscott.Menus
{
    public class PaymentsMenu : Menu
    {
        
        public PaymentsMenu() : base("")
        {
            if (fileExists)
            {
                startParse(this.filePath);
            }
        }
        public PaymentsMenu(string fileIn, ICommandManager managerIn) : base(fileIn)
        {
            commandManager = managerIn;
            if (fileExists)
            {
                startParse(this.filePath);
            }
        }

        public ReadPaymentCSV readPaymentCSV;

        public override void startParse(string filepath)
        {
            readPaymentCSV = new ReadPaymentCSV(filepath);
            CountUsers getUserCount = new CountUsers();
            commandManager.InvokeCommand(getUserCount);
            if (getUserCount.getNumberOfUsers() < 1)
            {
                Console.WriteLine("Payments can not be entered until there as existing users in the system");
                return;
            }

           

            commandManager.InvokeCommand(new WritePaymentsToDB(readPaymentCSV.payments));

            ReadPaymentFromDB read = new ReadPaymentFromDB();
            commandManager.InvokeCommand(read);

            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
            }
            Console.WriteLine("Current payments in the database\n--------------------");
            foreach (Payment p in read.payments)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("\n--------------------");

            ReadUserFromDB readUserFromDB = new ReadUserFromDB();
            commandManager.InvokeCommand(readUserFromDB);
            ReadUserPayments readUserPayments = new ReadUserPayments(readUserFromDB.users);
            commandManager.InvokeCommand(readUserPayments);

            Console.WriteLine("Users and the payments they have made:\n");
            foreach (User u in readUserPayments.users)
            {
                string paymentDescription = $"User: {u.AccountName} Account Number: {u.AccountNumber}";
                foreach (Payment p in u.PaymentsMade)
                {
                    paymentDescription += $"\nAdept Reference: {p.AdeptReference} - Payment amount: {p.amount}, Date {p.date} \n";
                }
                if (u.PaymentsMade.Count == 0)
                {
                    paymentDescription += $"\nHas no associated payments";
                }
                Console.WriteLine($"{paymentDescription}\n-----------------------------");
            }


        }
    }
}

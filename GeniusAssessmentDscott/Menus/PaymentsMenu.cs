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
            //Read the payments info from the user specified file
            readPaymentCSV = new ReadPaymentCSV(filepath);
            
            //Query the database to check that users exist, if they do not then return to the menu in line of the requirement of not allowing payments to be entered without users existing
            CountUsers getUserCount = new CountUsers();
            commandManager.InvokeCommand(getUserCount);
            if (getUserCount.getNumberOfUsers() < 1)
            {
                Console.WriteLine("Payments can not be entered until there as existing users in the system");
                return;
            }

           
            //Write the payments to the db
            commandManager.InvokeCommand(new WritePaymentsToDB(readPaymentCSV.payments));

            //Query the database for a list of all the payments to display to the user.
            ReadPaymentFromDB read = new ReadPaymentFromDB();
            commandManager.InvokeCommand(read);

            //Clear console for convenience
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

            //Get users from the database
            ReadUserFromDB readUserFromDB = new ReadUserFromDB();
            commandManager.InvokeCommand(readUserFromDB);
            //Get payments from the database using the relations between users and their payments and then store these relations in the user objects
            ReadUserPayments readUserPayments = new ReadUserPayments(readUserFromDB.users);
            commandManager.InvokeCommand(readUserPayments);

            //Display this info
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

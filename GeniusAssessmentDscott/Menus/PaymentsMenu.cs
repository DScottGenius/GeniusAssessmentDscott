using GeniusAssessmentDscott.Data.Entities;
using GeniusAssessmentDscott.Logic;
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


        public override void startParse(string filepath)
        {

            PaymentLogic paymentLogic = new PaymentLogic(filePath);
            if (!paymentLogic.success)
            {
                Console.WriteLine("Payments can not be entered until there as existing users in the system");
                return;
            }


            //Clear console for convenience
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
            }
            Console.WriteLine("Current payments in the database\n--------------------");
            foreach (Payment p in paymentLogic.PaymentsFromDB)
            {
                Console.WriteLine($"{p.ToString()}\n");
            }
            Console.WriteLine("\n--------------------");



            //Display
            Console.WriteLine("Users and the payments they have made:\n");
            foreach (User u in paymentLogic.UsersWithPayments)
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

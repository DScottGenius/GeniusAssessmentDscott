using DSLogger;
using GeniusAssessmentDscott.Core.Commands;
using GeniusAssessmentDscott.Core.CSV_Reader;
using GeniusAssessmentDscott.Data.Entities;
using System.Collections.Generic;
using System.IO;

namespace GeniusAssessmentDscott.Logic
{
    public class PaymentLogic : ILogic
    {

        public List<Payment> DBPayments;
        public ReadPaymentCSV ReadPayment { get; private set; }
        public List<Payment> PaymentsFromDB { get; private set; }
        public List<User> UsersWithPayments { get; private set; }
        public bool success { get; private set; }
        public PaymentLogic(string fileIn)
        {
            Filepath = fileIn;
            commandManager = new CommandManager();
            processData();
        }
        public PaymentLogic(string fileIn, ICommandManager managerIn)
        {
            Filepath = fileIn;
            commandManager = managerIn;
            processData();
        }

        public override bool DidReadSucceed()
        {
            if (ReadPayment.payments.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void processData()
        {
            try
            {
                //Get the users from the CSV file the user puts in
                ReadPayment = new ReadPaymentCSV(Filepath);
            }
            catch (FileNotFoundException e)
            {
                Logger log = new Logger("", nameof(PaymentLogic));
                log.WriteToLog(e.Message);
                success = false;
                return;
            }

            success = DidReadSucceed();
            if (success)
            {
                UseDatabase();
            }
        }

        protected override void UseDatabase()
        {

            //Query the database to check that users exist, if they do not then return to the menu in line of the requirement of not allowing payments to be entered without users existing
            CountUsers getUserCount = new CountUsers();
            commandManager.InvokeCommand(getUserCount);

            //Write the payments to the db
            commandManager.InvokeCommand(new WritePaymentsToDB(ReadPayment.payments));

            //Query the database for a list of all the payments to display to the user.
            ReadPaymentFromDB read = new ReadPaymentFromDB();
            commandManager.InvokeCommand(read);
            PaymentsFromDB = read.payments;

            //Get users from the database
            ReadUserFromDB readUserFromDB = new ReadUserFromDB();
            commandManager.InvokeCommand(readUserFromDB);
            //Get payments from the database using the relations between users and their payments and then store these relations in the user objects
            ReadUserPayments readUserPayments = new ReadUserPayments(readUserFromDB.users);
            commandManager.InvokeCommand(readUserPayments);
            UsersWithPayments = readUserPayments.users;
        }
    }
}

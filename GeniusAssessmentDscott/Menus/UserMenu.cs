using GeniusAssessmentDscott.Commands;
using GeniusAssessmentDscott.CSV_Reader;
using GeniusAssessmentDscott.Entities;
using System;

namespace GeniusAssessmentDscott.Menus
{
    public class UserMenu : Menu
    {
        public UserMenu() : base("")
        {
            if (fileExists)
            {
                startParse(this.filePath);
            }
        }

        public UserMenu(string fileIn, ICommandManager managerIn) : base(fileIn)
        {
            commandManager = managerIn;
            if (fileExists)
            {
                startParse(this.filePath);
            }
        }
        public ReadUserCSV readUserCSV;

        public override void startParse(string filepath)
        {
            //Get the users from the CSV file the user puts in
            readUserCSV = new ReadUserCSV(filepath);

            //invoke the command to write the users from the csv into the database
            commandManager.InvokeCommand(new WriteUsersToDB(readUserCSV.Users));

            //Get a list of users directly from the database
            ReadUserFromDB read = new ReadUserFromDB();
            commandManager.InvokeCommand(read);

            //Clear the console for convenience (surrounded in a try catch for test instances where a console may not exist)
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
            }

            //Display the list of users in the database so that the user can confirm their data has been added
            Console.WriteLine("Current users in the database\n--------------------");
            foreach (User u in read.users)
            {
                Console.WriteLine(u.ToString());
            }
            Console.WriteLine("\n--------------------");

            CountUsers getUserCount = new CountUsers();
            commandManager.InvokeCommand(getUserCount);

            Console.WriteLine($"User count is {getUserCount.getNumberOfUsers()}");
        }
    }
}

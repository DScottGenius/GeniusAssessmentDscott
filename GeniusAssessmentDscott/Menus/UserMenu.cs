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
            readUserCSV = new ReadUserCSV(filepath);

            commandManager.InvokeCommand(new WriteUsersToDB(readUserCSV.Users));

            ReadUserFromDB read = new ReadUserFromDB();
            commandManager.InvokeCommand(read);

            
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
            }
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

using GeniusAssessmentDscott.Data.Entities;
using GeniusAssessmentDscott.Logic;
using System;

namespace GeniusAssessmentDscott.Menus
{
    public class UserMenu : Menu
    {
        UserLogic ULogic;
        public UserMenu() : base("")
        {
            if (fileExists)
            {
                startParse(this.filePath);
            }
        }


        public override void startParse(string filepath)
        {
            ULogic = new UserLogic(filePath);

            if (!ULogic.success)
            {
                Console.WriteLine($"No users were obtained from the file {filepath}. Please check the format of the file");
                return;
            }

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
            foreach (User u in ULogic.dbUsers)
            {
                Console.WriteLine(u.ToString());
            }
            Console.WriteLine("\n--------------------");

        }
    }
}

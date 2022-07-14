using GeniusAssessmentDscott.Core.Commands;
using GeniusAssessmentDscott.Core.CSV_Reader;
using GeniusAssessmentDscott.Data.Entities;
using System.Collections.Generic;
using System.IO;

namespace GeniusAssessmentDscott.Logic
{
    public class UserLogic : ILogic
    {
        public ReadUserCSV readUserCSV { get; private set; }

        public List<User> dbUsers { get; private set; }

        public bool success { get; private set; }


        public UserLogic(string fileIn)
        {
            Filepath = fileIn;

            commandManager = new CommandManager();

            processData();

        }
        public UserLogic(string fileIn, ICommandManager managerIn)
        {
            Filepath = fileIn;
            commandManager = managerIn;
            processData();
        }

        public override void processData()
        {
            //Get the users from the CSV file the user puts in
            try
            {
                readUserCSV = new ReadUserCSV(Filepath);
            }
            catch (FileNotFoundException)
            {
                success = false;
                return;
            }
            readUserCSV = new ReadUserCSV(Filepath);
            success = DidReadSucceed();
            if (success)
            {
                UseDatabase();
            }

        }

        protected override void UseDatabase()
        {
            //invoke the command to write the users from the csv into the database
            commandManager.InvokeCommand(new WriteUsersToDB(readUserCSV.Users));

            //Get a list of users directly from the database
            ReadUserFromDB read = new ReadUserFromDB();
            commandManager.InvokeCommand(read);
            dbUsers = read.users;
        }


        public override bool DidReadSucceed()
        {
            if (readUserCSV.Users.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

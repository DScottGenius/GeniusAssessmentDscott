using GeniusAssessmentDscott.Data.Database;
using System;
using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Core.Commands
{
    public class CountUsers : DatabaseAccessCommand, ICommand
    {
        private SqlConnection connection;
        private DatabaseConnect databaseConnect;

        int userCount;


        public CountUsers() : base()
        {
            databaseConnect = new DatabaseConnect(ConnectionString);
            connection = databaseConnect.connection;
            userCount = 0;

        }

        public bool CanExecute()
        {
            return databaseConnect.CheckConnection();
        }

        public void Execute()
        {
            string countQuery = "SELECT COUNT(*) FROM [MiniAdeptDB].[dbo].[user];";

            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(countQuery, connection))
                {
                    userCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

        }

        public void OnFail()
        {
            Console.WriteLine("Could not get the count of users in the database");
        }

        public int getNumberOfUsers()
        {
            return this.userCount;
        }

    }
}

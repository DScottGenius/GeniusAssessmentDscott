using GeniusAssessmentDscott.Data.Database;
using GeniusAssessmentDscott.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Core.Commands
{
    public class ReadUserPayments :DatabaseAccessCommand, ICommand
    {
        DatabaseConnect dbConnect;
        SqlConnection connection;
        public List<User> users
        {
            get;
        }

        public ReadUserPayments(List<User> userIn) : base()
        {
            dbConnect = new DatabaseConnect(ConnectionString);
            connection = dbConnect.connection;
            users = userIn;
        }

        public bool CanExecute()
        {
            return dbConnect.CheckConnection();
        }

        public void Execute()
        {
            string selectUsersPayments = "SELECT p.adept_ref, p.amount, p.effective_date, p.method, p.[source] FROM [MiniAdeptDB].[dbo].[payment] p JOIN[MiniAdeptDB].[dbo].[user] u ON p.adept_ref = u.adept_ref WHERE u.adept_ref = @adeptRef;";

            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectUsersPayments, connection))
                {
                    foreach (User u in users)
                    {
                        command.Parameters.AddWithValue("@adeptRef", u.AdeptReference);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                string adeptRef = reader.GetString(0);
                                double amount = decimal.ToDouble(reader.GetDecimal(1));
                                string date = reader.GetDateTime(2).ToString();
                                string source = reader.GetString(3);
                                string method = reader.GetString(4);


                                Payment p = new Payment(adeptRef, amount, date, source, method);

                                u.AddPayment(p);
                            }
                            catch (InvalidCastException)
                            {

                                continue;
                            }
                        }
                        command.Parameters.Clear();
                        reader.Close();
                    }

                }

                connection.Close();
            }

        }

        public void OnFail()
        {
            Console.WriteLine("Could not establish connection with the database");
        }
    }
}

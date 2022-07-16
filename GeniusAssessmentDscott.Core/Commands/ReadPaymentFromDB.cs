using GeniusAssessmentDscott.Data.Database;
using GeniusAssessmentDscott.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Core.Commands
{
    public class ReadPaymentFromDB : DatabaseAccessCommand, ICommand
    {

        DatabaseConnect dbConnect;
        SqlConnection connection;
        public List<Payment> payments
        {
            get;
        }


        public ReadPaymentFromDB() : base()
        {
            dbConnect = new DatabaseConnect(ConnectionString);
            connection = dbConnect.connection;
            payments = new List<Payment>();
        }

        public bool CanExecute()
        {
            return dbConnect.CheckConnection();
        }

        public void Execute()
        {
            string selectPayments = "SELECT p.adept_ref, p.amount, p.effective_date, p.method, p.[source], p.payment_id FROM [MiniAdeptDB].[dbo].[payment] p;";

            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectPayments, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string adeptRef = reader.GetString(0);
                        double amount = decimal.ToDouble(reader.GetDecimal(1));
                        string effDate = reader.GetDateTime(2).ToString();

                        string source = reader.GetString(3);
                        string method = reader.GetString(4);
                        Payment p = new Payment(adeptRef, amount, effDate, source, method);

                        p.id = reader.GetInt32(5);

                        payments.Add(p);
                    }

                    reader.Close();
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

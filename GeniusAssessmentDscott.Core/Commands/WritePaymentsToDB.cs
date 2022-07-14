using GeniusAssessmentDscott.Core.Commands;
using GeniusAssessmentDscott.Data.Database;
using GeniusAssessmentDscott.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Core.Commands
{
    class WritePaymentsToDB : ICommand
    {
        private SqlConnection connection;
        private DatabaseConnect databaseConnect;
        private List<Payment> payments;

        public WritePaymentsToDB(List<Payment> paymentsIn)
        {
            databaseConnect = new DatabaseConnect();
            connection = databaseConnect.connection;

            if (paymentsIn != null)
            {
                payments = paymentsIn;
            }
            else
            {
                payments = new List<Payment>();
            }
        }

        public bool CanExecute()
        {
            return databaseConnect.CheckConnection();
        }

        public void Execute()
        {

            string userSql = @"INSERT INTO [MiniAdeptDB].[dbo].[payment](adept_ref, amount, effective_date, [source], method) VALUES(@adeptRef, @amount, @date, @source, @method);";


            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(userSql, connection))
                {
                    foreach (Payment p in payments)
                    {
                        try
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@adeptRef", System.Data.SqlDbType.VarChar).Value = p.AdeptReference;
                            command.Parameters.Add("@amount", System.Data.SqlDbType.Decimal).Value = p.amount;
                            command.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = p.date;
                            command.Parameters.Add("@source", System.Data.SqlDbType.VarChar).Value = p.source;
                            command.Parameters.Add("@method", System.Data.SqlDbType.VarChar).Value = p.method;


                            command.ExecuteNonQuery();
                        }
                        catch (SqlException)
                        {

                            continue;
                        }
                    }
                }
                payments.Clear();
                connection.Close();
            }
        }


        public void OnFail()
        {
            Console.WriteLine("Couldn't access the database to write the payments to");
        }
    }
}

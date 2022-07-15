using GeniusAssessmentDscott.Data.Database;
using GeniusAssessmentDscott.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GeniusAssessmentDscott.Core.Commands
{
    class WriteUsersToDB :DatabaseAccessCommand, ICommand
    {
        private SqlConnection connection;
        private DatabaseConnect databaseConnect;
        private List<User> users;

        public WriteUsersToDB(List<User> usersIn) :base()
        {
            databaseConnect = new DatabaseConnect(ConnectionString);
            connection = databaseConnect.connection;

            if (usersIn != null)
            {
                users = usersIn;
            }
            else
            {
                users = new List<User>();
            }
        }

        public bool CanExecute()
        {
            return databaseConnect.CheckConnection();
        }

        public void Execute()
        {
            User user = users.ElementAt(0);
            string userSql = @"INSERT INTO [MiniAdeptDB].[dbo].[user](debt_type, account_number, account_name, date_of_birth, balance, adept_ref) VALUES(@debt, @accNum, @accName, @bday, @balance, @adeptref);";
            string emailSql = @"INSERT INTO [MiniAdeptDB].[dbo].[email](email_owner, email_address) VALUES(@emailOwner, @email);";
            string phoneSql = @"INSERT INTO [MiniAdeptDB].[dbo].[phone](phone_owner, phone_number) VALUES(@phoneOwner, @phone);";
            string addressSql = @"INSERT INTO [MiniAdeptDB].[dbo].[address](address_owner, line_1, line_2, city, postcode) VALUES(@addressOwner, @line1, @line2, @city, @postcode);";


            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(userSql, connection))
                {
                    foreach (User u in users)
                    {
                        try
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add("@debt", System.Data.SqlDbType.VarChar).Value = u.DebtType;
                            command.Parameters.Add("@accNum", System.Data.SqlDbType.VarChar).Value = u.AccountNumber;
                            command.Parameters.Add("@accName", System.Data.SqlDbType.VarChar).Value = u.AccountName;
                            command.Parameters.Add("@bday", System.Data.SqlDbType.Date).Value = u.Birthday;
                            command.Parameters.Add("@balance", System.Data.SqlDbType.Decimal).Value = u.Balance;
                            command.Parameters.Add("@adeptref", System.Data.SqlDbType.VarChar).Value = "";


                            command.ExecuteNonQuery();


                            foreach (string e in u.Emails)
                            {
                                using (SqlCommand commandEmail = new SqlCommand(emailSql, connection))
                                {
                                    commandEmail.Parameters.Add("@emailOwner", System.Data.SqlDbType.VarChar).Value = u.AccountNumber;
                                    commandEmail.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = e;
                                    commandEmail.ExecuteNonQuery();
                                }
                            }
                            foreach (string p in u.Phones)
                            {
                                using (SqlCommand commandPhone = new SqlCommand(phoneSql, connection))
                                {
                                    commandPhone.Parameters.Add("@phoneOwner", System.Data.SqlDbType.VarChar).Value = u.AccountNumber;
                                    commandPhone.Parameters.Add("@phone", System.Data.SqlDbType.VarChar).Value = p;
                                    commandPhone.ExecuteNonQuery();
                                }
                            }
                            foreach (Address a in u.Addresses)
                            {
                                using (SqlCommand commandAddress = new SqlCommand(addressSql, connection))
                                {
                                    commandAddress.Parameters.Add("@addressOwner", System.Data.SqlDbType.VarChar).Value = u.AccountNumber;
                                    commandAddress.Parameters.Add("@line1", System.Data.SqlDbType.VarChar).Value = a.AddressLine1;
                                    commandAddress.Parameters.Add("@line2", System.Data.SqlDbType.VarChar).Value = a.AddressLine2;
                                    commandAddress.Parameters.Add("@city", System.Data.SqlDbType.VarChar).Value = a.City;
                                    commandAddress.Parameters.Add("@postcode", System.Data.SqlDbType.VarChar).Value = a.Postcode;
                                    commandAddress.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (SqlException)
                        {

                            continue;
                        }
                    }
                }
                users.Clear();
                connection.Close();
            }
        }

        public void OnFail()
        {
            Console.WriteLine("Could not establish connection with the database");
        }
    }
}

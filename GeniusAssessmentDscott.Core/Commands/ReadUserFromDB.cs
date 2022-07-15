using GeniusAssessmentDscott.Data.Database;
using GeniusAssessmentDscott.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Core.Commands
{
    public class ReadUserFromDB : DatabaseAccessCommand, ICommand
    {

        DatabaseConnect dbConnect;
        SqlConnection connection;
        public List<User> users
        {
            get;
        }


        public ReadUserFromDB() :base()
        {
            dbConnect = new DatabaseConnect(ConnectionString);
            connection = dbConnect.connection;
            users = new List<User>();
        }

        public bool CanExecute()
        {
            return dbConnect.CheckConnection();
        }

        public void Execute()
        {
            string selectUsers = "SELECT p.debt_type, p.account_number, p.account_name, p.date_of_birth, p.balance, p.adept_ref  FROM [MiniAdeptDB].[dbo].[user] p;";
            string selectEmails = "SELECT e.email_address FROM [MiniAdeptDB].[dbo].[email] e LEFT JOIN [MiniAdeptDB].[dbo].[user] u ON u.account_number = e.email_owner WHERE u.account_number = @accnum;";
            string selectPhones = "SELECT p.phone_number FROM [MiniAdeptDB].[dbo].[phone] p LEFT JOIN [MiniAdeptDB].[dbo].[user] u ON u.account_number = p.phone_owner WHERE u.account_number = @accnum;";
            string selectAddresses = "SELECT a.line_1, a.line_2, a.city, a.postcode FROM [MiniAdeptDB].[dbo].[address] a LEFT JOIN [MiniAdeptDB].[dbo].[user] u ON u.account_number = a.address_owner WHERE u.account_number = @accnum;";
            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(selectUsers, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {
                        string debt = reader.GetString(0);
                        string accNum = reader.GetString(1);
                        string accName = reader.GetString(2);
                        string bday = reader.GetDateTime(3).ToString();
                        double balance = decimal.ToDouble(reader.GetDecimal(4));
                        string adeptRef = reader.GetString(5);
                        List<string> phones = new List<string>();
                        List<string> emails = new List<string>();
                        List<Address> addresses = new List<Address>();

                        using (SqlCommand commandEmails = new SqlCommand(selectEmails, connection))
                        {
                            commandEmails.Parameters.AddWithValue("@accnum", accNum);
                            SqlDataReader readerEmail = commandEmails.ExecuteReader();
                            while (readerEmail.Read())
                            {
                                emails.Add(readerEmail.GetString(0));
                            }

                        }
                        using (SqlCommand commandPhones = new SqlCommand(selectPhones, connection))
                        {
                            commandPhones.Parameters.AddWithValue("@accnum", accNum);
                            SqlDataReader readerPhones = commandPhones.ExecuteReader();
                            while (readerPhones.Read())
                            {
                                phones.Add(readerPhones.GetString(0));
                            }
                            readerPhones.Close();
                        }
                        using (SqlCommand commandPhone = new SqlCommand(selectAddresses, connection))
                        {
                            commandPhone.Parameters.AddWithValue("@accnum", accNum);
                            SqlDataReader readerAddress = commandPhone.ExecuteReader();
                            while (readerAddress.Read())
                            {
                                Address address = new Address(readerAddress.GetString(0), readerAddress.GetString(1), readerAddress.GetString(2), readerAddress.GetString(3));
                                addresses.Add(address);
                            }
                            readerAddress.Close();
                        }
                        User u = new User(debt, accNum, adeptRef, accName, bday, balance, emails, phones, addresses);
                        users.Add(u);
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

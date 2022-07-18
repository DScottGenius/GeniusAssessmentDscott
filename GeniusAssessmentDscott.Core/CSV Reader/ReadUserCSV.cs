using DSLogger;
using GeniusAssessmentDscott.Data.Entities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace GeniusAssessmentDscott.Core.CSV_Reader
{
    public class ReadUserCSV : ReadCSV
    {
        public List<User> Users
        {
            get;
        }

        public ReadUserCSV(string filePath) : base(filePath)
        {
            Users = new List<User>();
            this.StartParse(this.Filepath);
        }

        protected override void parseCSV(TextFieldParser parser)
        {
            //skip first line to skip the headings
            parser.ReadFields();

            while (!parser.EndOfData)
            {
                string[] line = parser.ReadFields();
                User user;
                string debtType = "";
                string accNum = "";
                string accName = "";
                string bDay = "";
                double balance = 0;
                string email = "";
                string phone = "";
                string address1 = "";
                string address2 = "";
                string city = "";
                string postcode = "";
                Address address = null;

                try
                {
                    for (int i = 0; i < line.Length; i++)
                    {

                        switch (i)
                        {
                            case 0:
                                debtType = line[i];
                                break;
                            case 1:
                                accNum = line[i];
                                break;
                            case 2:
                                accName = line[i];
                                break;
                            case 3:
                                bDay = CheckDate(line[i]);
                                break;
                            case 4:
                                balance = double.Parse(line[i]);
                                break;
                            case 5:
                                bool validEmail;

                                try
                                {
                                    MailAddress mail = new MailAddress(line[i]);
                                    validEmail = mail.Host.Contains('.');
                                }
                                catch (Exception)
                                {
                                    validEmail = false;
                                }

                                if (validEmail)
                                {
                                    email = line[i];
                                }
                                else
                                {
                                    email = "";
                                }
                                break;
                            case 6:
                                if (line[i].Length <= 10 && line[i].Length > 0)
                                {
                                    phone = line[i];
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                                break;
                            case 7:
                                address1 = line[i];
                                break;
                            case 8:
                                address2 = line[i];
                                break;
                            case 9:
                                city = line[i];
                                break;
                            case 10:
                                postcode = line[i];

                                address = new Address(address1, address2, city, postcode);
                                break;
                        }


                    }
                }

                catch (FormatException e)
                {
                    Logger log = new Logger("", nameof(ReadUserCSV));
                    log.WriteToLog($"{e.Message}");
                    continue;
                }

                if (address != null)
                {
                    user = new User(debtType, accNum, "", accName, bDay, balance, email, phone, address);
                    Users.Add(user);
                }
            }


        }

    }
}


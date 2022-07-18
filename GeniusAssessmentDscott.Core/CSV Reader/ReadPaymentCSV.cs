using DSLogger;
using GeniusAssessmentDscott.Data.Entities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace GeniusAssessmentDscott.Core.CSV_Reader
{
    public class ReadPaymentCSV : ReadCSV
    {
        public List<Payment> payments;

        public ReadPaymentCSV(string filePath) : base(filePath)
        {
            this.payments = new List<Payment>();
            StartParse(filePath);
        }

        protected override void parseCSV(TextFieldParser parser)
        {
            //skip first line to skip the headings
            parser.ReadFields();

            while (!parser.EndOfData)
            {
                string[] line = parser.ReadFields();
                string AdeptRef = "";
                double amount = 0;
                string date = "";
                string source = "";
                string method = "";


                try
                {
                    for (int i = 0; i < line.Length; i++)
                    {

                        switch (i)
                        {
                            //Adept Reference
                            case 0:
                                if (line[i].Length == 7)
                                {
                                    AdeptRef = line[i];
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                                break;
                            case 1:
                                amount = double.Parse(line[i]);
                                break;
                            case 2:
                                date = CheckDate(line[i]);
                                break;
                            case 3:
                                source = line[i];
                                break;
                            case 4:
                                method = line[i];
                                break;
                        }
                    }
                    Payment payment = new Payment(AdeptRef, amount, date, source, method);
                    payments.Add(payment);
                }
                catch (FormatException e)
                {
                    Logger log = new Logger("", nameof(ReadPaymentCSV));
                    log.WriteToLog(e.Message);
                    continue;
                }

            }
        }
    }
}

using GeniusAssessmentDscott.CSV_Reader;
using GeniusAssessmentDscott.Entities;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GeniusAssessment.Test.CSVReaderTests
{
    class PaymentCSVTest
    {
        private ReadPaymentCSV readPaymentCSV;

        



        [Test]
        [TestCase(@"CSVs\direct payments 11042022.csv", ExpectedResult = 5, TestName = "Test payments with valid file")] //2 entries should be disqualified from the file for invalid data
        [TestCase(@"CSVs\placement 211214.csv", ExpectedResult = 0, TestName = "Test payments added with invalid file")]
        public int CheckUsersAdded(string filename)
        {
            readPaymentCSV = new ReadPaymentCSV(filename);
            return readPaymentCSV.payments.Count;
        }

        [Test]
        public void CheckPaymentIntegrity()
        {
            readPaymentCSV = new ReadPaymentCSV(@"C:\Users\dscott\Documents\Training\Assessment\direct payments 11042022.csv");
            //Test the first entry added in list to the data manually read from the first payment in the file
            var sut = readPaymentCSV.payments.ElementAt(0);
            bool mismatch = true;

            if (sut.AdeptReference.ToLower()!= "a111631" || sut.amount != 20 || sut.date != "2022/4/11" || sut.source.ToLower() != "website realex" || sut.method.ToLower() != "website")
            {
                mismatch = true;
            }
            else
            {
                mismatch = false;
            }

            Assert.That(mismatch, Is.False);

        }
        [Test]
        public void CheckNoInvaidPayments()
        {
            readPaymentCSV = new ReadPaymentCSV(@"CSVs\direct payments 11042022.csv");

            var sut = readPaymentCSV.payments;
            bool InvalidData = false;
            foreach (Payment p in sut)
            {
                
                if (p.AdeptReference.Count() > 7)
                {
                    InvalidData = true;
                    break;
                }
            }

            Assert.That(InvalidData, Is.False);
        }


    }


}

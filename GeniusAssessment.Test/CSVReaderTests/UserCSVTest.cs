using GeniusAssessmentDscott.Core.CSV_Reader;
using GeniusAssessmentDscott.Data.Entities;
using NUnit.Framework;
using System.Linq;

namespace GeniusAssessment.Test.CSVReaderTests
{
    [TestFixture]
    class UserCSVTest
    {
        private ReadUserCSV readUserCSV;


        [Test]
        //2/4 of the users should not be added due to invalid data formats so our expected value here is 2.
        [TestCase(@"CSVs\placement 211214.csv", ExpectedResult = 2, TestName = "Test valid users added: valid file")]
        [TestCase(@"CSVs\direct payments 11042022.csv", ExpectedResult = 0, TestName = "Test users not added: invalid file")]
        public int CheckUsersAdded(string filename)
        {
            readUserCSV = new ReadUserCSV(filename);
            return readUserCSV.Users.Count;
        }

        [Test]
        public void CheckUsersIntegrity()
        {
            readUserCSV = new ReadUserCSV(@"CSVs\placement 211214.csv");
            //Test the first entry added in list to the data manually read from the first user in the file
            var sut = readUserCSV.Users.ElementAt(0);
            bool mismatch = true;

            if (sut.DebtType.ToLower() != "residential" || sut.AccountNumber != "921081" || sut.AccountName.ToLower() != "marina waskowski" || sut.Birthday != "1967/3/5" || sut.Balance != 571.98)
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
        public void CheckNoInvalidUser()
        {
            readUserCSV = new ReadUserCSV(@"CSVs\placement 211214.csv");

            var sut = readUserCSV.Users;
            bool InvalidData = false;
            foreach (User u in sut)
            {
                if (u.AdeptReference.Length > 7)
                {
                    InvalidData = true;
                    break;
                }
                if (u.AccountNumber.Length == 0)
                {
                    InvalidData = true;
                    break;
                }
                if (u.Phones.Count >= 1)
                {
                    foreach (string p in u.Phones)
                    {
                        if (p.Length > 10 || p.Length == 0)
                        {
                            InvalidData = true;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            Assert.That(InvalidData, Is.False);
        }
    }
}

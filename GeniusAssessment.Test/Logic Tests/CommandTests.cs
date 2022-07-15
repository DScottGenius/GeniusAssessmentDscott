using GeniusAssessmentDscott.Core.Commands;
using GeniusAssessmentDscott.Data.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeniusAssessment.Test.Logic_Tests
{



    [TestFixture]
    class CommandTests
    {
        public static IEnumerable<TestCaseData> IsConnectionStringCorrectTestCases
        {
            get
            {
                yield return new TestCaseData(new CountUsers()).SetName("CountUsers Connection String");
                yield return new TestCaseData(new ReadPaymentFromDB()).SetName("ReadPaymentFromDB Connection String");
                yield return new TestCaseData(new ReadUserFromDB()).SetName("ReadUserFromDB Connection String");
                yield return new TestCaseData(new ReadUserPayments(null)).SetName("ReadUserPayments Connection String");
                yield return new TestCaseData(new WritePaymentsToDB(null)).SetName("WritePaymentsToDB Connection String");
                yield return new TestCaseData(new WriteUsersToDB(null)).SetName("WriteUsersToDB Connection String");
            }
        }



        [TestCaseSource("IsConnectionStringCorrectTestCases")]
        public void IsConnectionStringCorrect(DatabaseAccessCommand command)
        {
            var ExpectedConnectionString = "Server=localhost\\SQLEXPRESS;Database=TestDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;";

            Assert.That(command.ConnectionString, Is.EqualTo(ExpectedConnectionString));
        }

    }
}

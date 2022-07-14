using GeniusAssessmentDscott.Core.Commands;
using GeniusAssessmentDscott.Logic;
using GeniusAssessmentDscott.Menus;
using NUnit.Framework;

namespace GeniusAssessment.Test.Menu_Tests
{
    public class dummyManager : ICommandManager
    {
        public void InvokeCommand(ICommand commandIn)
        {
            //do nothing 
        }
    }
    [TestFixture]
    class LogicTest
    {
        [Test]
        [TestCase(@"CSVs\placement 211214.csv", ExpectedResult = true, TestName = "System successfully got users from the file")]
        [TestCase(@"This is not a valid path", ExpectedResult = false, TestName = "System got no users from invalid files")]
        public bool TestUserMenu(string filepath)
        {
            UserLogic userLogic = new UserLogic(filepath, new dummyManager());
            return userLogic.success;
        }

        [Test]
        [TestCase(@"CSVs\direct payments 11042022.csv", ExpectedResult = true, TestName = "Valid File begins payment csv parse")]
        [TestCase(@"This is not a valid path", ExpectedResult = false, TestName = "System doesnt try to parse nonexistant payment file")]
        public bool TestPaymentMenu(string filepath)
        {
            PaymentLogic paymentLogic = new PaymentLogic(filepath, new dummyManager());
            return paymentLogic.success;
        }
    }


}

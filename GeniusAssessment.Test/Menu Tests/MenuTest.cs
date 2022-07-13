using GeniusAssessmentDscott.Commands;
using GeniusAssessmentDscott.Menus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
    class MenuTest
    {
        [Test]
        [TestCase(@"C:\Users\dscott\Documents\Training\Assessment\placement 211214.csv", ExpectedResult = true, TestName ="Valid File begins user csv parse")]
        [TestCase(@"This is not a valid path", ExpectedResult = false, TestName = "System doesnt try to parse nonexistant user file")]
        public bool TestUserMenu(string filepath)
        {
            bool isNotNull = false;
            UserMenu userMenu = new UserMenu(filepath, new dummyManager());
            if (userMenu.readUserCSV != null)
            {
                isNotNull = true;
            }
            return isNotNull;
        }

        [Test]
        [TestCase(@"C:\Users\dscott\Documents\Training\Assessment\direct payments 11042022.csv", ExpectedResult = true, TestName = "Valid File begins payment csv parse")]
        [TestCase(@"This is not a valid path", ExpectedResult = false, TestName = "System doesnt try to parse nonexistant payment file")]
        public bool TestPaymentMenu(string filepath)
        {
            bool isNotNull = false;
            PaymentsMenu paymentsMenu = new PaymentsMenu(filepath, new dummyManager());
            if (paymentsMenu.readPaymentCSV != null)
            {
                isNotNull = true;
            }
            return isNotNull;
        }
    }

    
}

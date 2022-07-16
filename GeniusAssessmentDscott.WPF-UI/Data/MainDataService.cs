using GeniusAssessmentDscott.Data.Entities;
using GeniusAssessmentDscott.Logic;
using System.Collections.Generic;

namespace GeniusAssessmentDscott.WPF_UI.Data
{
    class MainDataService : IDataService
    {
        private PaymentLogic paymentLogic;
        private UserLogic userLogic;

        public IEnumerable<User> ProcessUser(string filePath)
        {

            userLogic = new UserLogic(filePath);

            if (userLogic.success)
            {
                return userLogic.dbUsers;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Payment> ProcessPayment(string filePath)
        {

            paymentLogic = new PaymentLogic(filePath);



            if (paymentLogic.success)
            {
                return paymentLogic.PaymentsFromDB;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<User> ProcessUserPayments()
        {
            return paymentLogic.UsersWithPayments;
        }

    }
}

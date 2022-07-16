using GeniusAssessmentDscott.Data.Entities;
using System.Collections.Generic;

namespace GeniusAssessmentDscott.WPF_UI.Data
{
    public interface IDataService
    {
        IEnumerable<User> ProcessUser(string FileIn);
        IEnumerable<Payment> ProcessPayment(string FileIn);
        IEnumerable<User> ProcessUserPayments();
    }
}
using System.Collections.Generic;
using System.Linq;

namespace GeniusAssessmentDscott.Entities
{
    //The user class corresponds to any user to be stored or received from the database. Will hold associated data from address and phone tables as well
    public class User
    {
        public string DebtType
        {
            get;
        }
        public string AccountNumber
        {
            get;
        }
        public string AdeptReference
        {
            get;
        }
        public string AccountName
        {
            get;
        }
        public string Birthday
        {
            get;
        }
        public double Balance
        {
            get;
        }


        //Phones, emails and Addresses will be lists for any future implementations where any account could be associated with multiple addresses and/or phone numbers.
        public List<string> Emails
        {
            get;
        }
        public List<string> Phones
        {
            get;
        }
        public List<Address> Addresses
        {
            get;
        }

        //A list containing all payments this user has made, which will be stored when the 
        public List<Payment> PaymentsMade
        {
            get;
        }

        public User(string debtType, string accountNumber, string adeptReference, string accountName, string birthday, double balance, string email, string phone, Address address)
        {
            this.DebtType = debtType;
            this.AccountNumber = accountNumber;
            this.AdeptReference = adeptReference;
            this.AccountName = accountName;
            this.Birthday = birthday;
            this.Balance = balance;

            Phones = new List<string>();
            Phones.Add(phone);

            Emails = new List<string>();
            Emails.Add(email);

            Addresses = new List<Address>();
            Addresses.Add(address);

            PaymentsMade = new List<Payment>();

        }

        public User(string debtType, string accountNumber, string adeptReference, string accountName, string birthday, double balance, List<string> emails, List<string> phones, List<Address> addresses)
        {
            this.DebtType = debtType;
            this.AccountNumber = accountNumber;
            this.AdeptReference = adeptReference;
            this.AccountName = accountName;
            this.Birthday = birthday;
            this.Balance = balance;

            Phones = phones;
            Emails = emails;
            Addresses = addresses;

            PaymentsMade = new List<Payment>();
        }

        public void AddPhone(string phoneIn)
        {
            if (!Phones.Contains(phoneIn))
            {
                Phones.Add(phoneIn);
            }
            else
            {
                return;
            }
        }

        public void AddAddress(string line1, string line2, string city, string postcode)
        {
            Address address = new Address(line1, line2, city, postcode);
            AddAddress(address);
        }
        public void AddAddress(Address addressIn)
        {
            if (!Addresses.Contains(addressIn))
            {
                Addresses.Add(addressIn);
            }
            else
            {
                return;
            }
        }

        public override string ToString()
        {
            string emailAddresses = "";
            for (int i = 0; i < Emails.Count; i++)
            {
                emailAddresses += Emails.ElementAt(i);

                emailAddresses = formatStringList(emailAddresses, i, Emails.Count);
            }
            string addresses = "";
            for (int i = 0; i < Emails.Count; i++)
            {
                addresses += Addresses.ElementAt(i).ToString();

                addresses = formatStringList(addresses, i, Addresses.Count);
            }
            string phones = "";
            for (int i = 0; i < Emails.Count; i++)
            {
                phones += Phones.ElementAt(i);

                phones = formatStringList(phones, i, Phones.Count);
            }

            string result = $"Debt Type: {DebtType}, Account Number: {AccountNumber}, Name of Account: {AccountName}, Date of birth: {Birthday}, Email address(es): {emailAddresses}, Phone Numbers: {phones}, Addresses: {addresses}";

            return result;
        }

        public void AddPayment(Payment pIn)
        {
            if (pIn != null)
            {
                PaymentsMade.Add(pIn);
            }
        }

        private string formatStringList(string input, int listPosition, int listSize)
        {


            if (!(listPosition == listSize - 1))
            {
                input += ",";
            }

            return input;

        }
    }

}

namespace GeniusAssessmentDscott.Data.Entities
{
    //This Class corresponds to entities that will be in the 
    public class Payment
    {
        public string AdeptReference
        {
            get;
        }

        public double amount
        {
            get;
        }

        public string date
        {
            get;
        }

        public string source
        {
            get;
        }

        public string method
        {
            get;
        }

        public int id
        {
            get;
            set;
        }


        public Payment(string adeptReference, double amount, string date, string source, string method)
        {
            AdeptReference = adeptReference;
            this.amount = amount;
            this.date = date;
            this.source = source;
            this.method = method;
        }

        public override string ToString()
        {
            string result = $"ID: {id} Adept Reference: {AdeptReference}, Amount: {amount}, Effective Date: {date}, Source: {source}, Method: {method}";
            return result;
        }
    }
}

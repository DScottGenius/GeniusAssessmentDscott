namespace GeniusAssessmentDscott.Data.Entities
{
    //Address corresponds to entities from the address table
    public class Address
    {
        public string AddressLine1
        {
            get;
        }
        public string AddressLine2
        {
            get;
        }
        public string City
        {
            get;
        }
        public string Postcode
        {
            get;
        }

        public Address(string addressLine1, string addressLine2, string city, string postcode)
        {
            this.AddressLine1 = addressLine1.Trim();
            this.AddressLine2 = addressLine2.Trim();
            this.City = city.Trim();
            this.Postcode = postcode.Trim();
        }

        public override string ToString()
        {
            string optionalLine = "";
            if (AddressLine2 == "")
            {
                optionalLine = "N/A";
            }
            else
            {
                optionalLine = AddressLine2;
            }
            return $"Address Line 1: {AddressLine1}, Address Line 2: {optionalLine}, City: {City}, Postcode: {Postcode}";
        }
    }
}

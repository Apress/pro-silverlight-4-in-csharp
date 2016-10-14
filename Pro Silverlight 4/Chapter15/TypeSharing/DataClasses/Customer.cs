using System;
using System.Net;
using System.Runtime.Serialization;

namespace DataClasses
{
    [DataContract(Name = "Customer",
     Namespace = "http://www.prosetech.com/DataClasses/Customer")]
    public class Customer
    {
        private string firstName;
        private string lastName;

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        // Ordinarily, this method would not be available on the client.
        public string GetFullName()
        {
            return firstName + " " + lastName;
        }
    }

}

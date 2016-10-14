using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DataClasses;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class TestService
{
    [OperationContract]
    public Customer GetCustomer(int customerID)
    {
        Customer newCustomer = new Customer();
        
        newCustomer.FirstName = "Joe";
        newCustomer.LastName = "Tester";
        return newCustomer;
    }

}

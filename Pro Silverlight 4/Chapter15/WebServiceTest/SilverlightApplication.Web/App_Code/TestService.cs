using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using System.Web;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class TestService
{
    [OperationContract]
    public DateTime GetServerTime()
    {
        return DateTime.Now;
    }

    [OperationContract]
    public DateTime GetCachedServerTime()
    {
        // Check the cache.
        HttpContext context = HttpContext.Current;

        if (context.Cache["CurrentDateTime"] != null)
        {
            // Retrieve it from the cache
            return (DateTime)context.Cache["CurrentDateTime"];
        }
        else
        {
            // Regenerate it.
            DateTime now = DateTime.Now;

            // Now store it in the cache for 5 seconds.
            context.Cache.Insert("CurrentDateTime", now, null,
             DateTime.Now.AddSeconds(5), TimeSpan.Zero);

            return now;
        }
    }

    [OperationContract]
    public DateTime GetServerTimeSlow()
    {
        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(7));
        return DateTime.Now;
    }

}
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using System.Threading;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class AsyncTask : IAsyncTaskService 
{
    public void SubmitTask(TaskDescription taskDescription)
    {                
        // Simulate some work with a delay.
        Thread.Sleep(TimeSpan.FromSeconds(15));
        
        // Reverse the letters in string.
        char[] data = taskDescription.DataToProcess.ToCharArray();
        Array.Reverse(data);

        // Prepare the response.
        TaskResult result = new TaskResult();
        result.ProcessedData = new string(data);
     
        // Send the response to the client.
        try
        {
            IAsyncTaskClient client = OperationContext.Current.GetCallbackChannel<IAsyncTaskClient>();
            client.ReturnResult(result);
        }
        catch
        {
            // The client could not be contacted.
            // Clean up any resources here before the thread ends.
        }
        // Incidentally, you can call the client.ReturnResult() method mulitple times to
        // return different pieces of data at different times.
        // The connection remains available until the reference is released (when the method 
        // ends and the variable goes out of scope).
    }
}

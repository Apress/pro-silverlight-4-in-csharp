using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;


[ServiceContract(CallbackContract = typeof(IAsyncTaskClient))]
public interface IAsyncTaskService
{
    [OperationContract(IsOneWay = true)]
    void SubmitTask(TaskDescription task);
}

[ServiceContract]
public interface IAsyncTaskClient
{
    [OperationContract(IsOneWay = true)]
    void ReturnResult(TaskResult result);
}

[DataContract()]
public class TaskDescription
{
    [DataMember()]
    public string DataToProcess{ get; set; }
}

[DataContract()]
public class TaskResult
{
    [DataMember()]
    public string ProcessedData { get; set; }
}
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class FileService
{
    private string filePath;

    public FileService()
    {
        // Alternatively, you could read this from a web.config application setting.
        filePath = HttpContext.Current.Server.MapPath("Files");
    }

    [OperationContract]
    public string[] GetFileList()
    {
        string[] files = Directory.GetFiles(filePath);

        // Trim out path information.
        for (int i = 0; i < files.Count(); i++)
        {
            files[i] = Path.GetFileName(files[i]);
        }

        return files;
    }

    [OperationContract]
    public void UploadFile(string fileName, byte[] data)
    {
        // Make sure the filename has no path information.
        string file = Path.Combine(filePath, Path.GetFileName(fileName));
                
        using (FileStream fs = new FileStream(file, FileMode.Create))
        {
            fs.Write(data, 0, (int)data.Length);            
        }
    }

    [OperationContract]
    public byte[] DownloadFile(string fileName)
    {
        // Make sure the filename has no path information.
        string file = Path.Combine(filePath, Path.GetFileName(fileName));

        using (FileStream fs = new FileStream(file, FileMode.Open))
        {
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            return data;
        }
    }

}

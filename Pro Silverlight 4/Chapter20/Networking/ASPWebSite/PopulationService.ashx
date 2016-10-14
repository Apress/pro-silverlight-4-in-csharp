<%@ WebHandler Language="C#" Class="PopulationRestService" %>

using System;
using System.Web;

public class PopulationRestService : IHttpHandler
{  
    public void ProcessRequest (HttpContext context)
    {
        string year = context.Request.Form["year"];
        year = year.Replace(",", "");
        year = year.Trim();
        
        bool isBc = false;
        if (year.EndsWith("BC", StringComparison.OrdinalIgnoreCase))
        {
            isBc = true;
            year = year.Remove(year.IndexOf("BC", StringComparison.OrdinalIgnoreCase));
            year = year.Trim();
        }
        int yearNumber = Int32.Parse(year);
        
        context.Response.ContentType = "text/plain";
        context.Response.Write(GetPopulation(yearNumber, isBc));
    }
    private int GetPopulation(int year, bool isBc)
    {
        if (isBc)
        {
            if (year >= 70000) return 2;
            else if (year >= 10000) return 1000;
            else if (year >= 9000) return 3000;
            else if (year >= 8000) return 5000;
            else if (year >= 7000) return 7000;
            else if (year >= 6000) return 10000;
            else if (year >= 5000) return 15000;
            else if (year >= 4000) return 20000;
            else if (year >= 3000) return 25000;
            else if (year >= 2000) return 25000;
            else if (year >= 1000) return 50000;
            else if (year >= 500) return 100000;
            else return 200000;
        }
        else
        {
            if (year < 1000) return 200000;
            else if (year < 1750) return 310000;
            else if (year < 1800) return 790000;
            else if (year < 1850) return 978000;
            else if (year < 1900) return 1262000;
            else if (year < 1950) return 1650000;
            else if (year < 1955) return 2519000;
            else if (year < 1960) return 2756000;
            else if (year < 1965) return 2982000;
            else if (year < 1970) return 3349000;
            else if (year < 1975) return 3692000;
            else if (year < 1980) return 4068000;
            else if (year < 1985) return 4435000;
            else if (year < 1990) return 4831000;
            else if (year < 1995) return 5264000;
            else if (year < 2000) return 5764000;
            else if (year < 2005) return 6071000;
            else return 6452000;
        }
    }
 
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

}
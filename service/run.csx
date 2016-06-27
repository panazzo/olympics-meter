#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

public static bool Run(TimerInfo timer, CloudTable outTable, TraceWriter log)
{
    foreach(var c in outTable.CreateQuery<Country>().ToList())
    {
        // Change the Score number.
        c.Score = GetScore(c.Name);
        
        // Create the Replace TableOperation.
        TableOperation updateOperation = TableOperation.Merge(c);
    
        // Execute the operation.
        outTable.Execute(updateOperation);
    }
    
    return true;
}

public static string GetScore(String country)
{
    switch (country)
    {
        case "Brazil":
            return "90";
        default:
            return "70";
    }
}

public class Country : TableEntity
{
    public string Name { get; set; }
    public string Score { get; set; }
}
#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static bool Run(TimerInfo timer, CloudTable outTable, TraceWriter log)
{
    foreach(var c in outTable.CreateQuery<Country>().ToList())
    {
        // Change the Score number.
        c.Score = "80";
        
        // Create the Replace TableOperation.
        TableOperation updateOperation = TableOperation.Merge(c);
    
        // Execute the operation.
        outTable.Execute(updateOperation);
    }
    
    return true;
}

public class Country : TableEntity
{
    public string Name { get; set; }
    public string Score { get; set; }
}
#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static bool Run(TimerInfo timer, CloudTable outTable, TraceWriter log)
{
    foreach(var c in outTable.CreateQuery<Country>().ToList())
    {
        TableOperation retrieveOperation = TableOperation.Retrieve<Country>(c.PartitionKey, c.RowKey);
        TableResult retrievedResult = outTable.Execute(retrieveOperation);
        
        // Assign the result to a CustomerEntity object.
        Country updateEntity = (Country)retrievedResult.Result;
        
        if (updateEntity != null)
        {
           // Change the phone number.
           updateEntity.Score = "90";
           
           // Create the Replace TableOperation.
           TableOperation updateOperation = TableOperation.Merge(updateEntity);
        
           // Execute the operation.
           outTable.Execute(updateOperation);
           
           log.Info(updateEntity.PartitionKey + " | " + updateEntity.RowKey  + " | " + updateEntity.Name + " | " + updateEntity.Score);
        } 
    }
    
    return true;
}

public class Country : TableEntity
{
    public string Name { get; set; }
    public string Score { get; set; }
}
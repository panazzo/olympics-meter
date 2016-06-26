#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Country> inputTable, TraceWriter log)
{
    var query = from c in inputTable select new CountryResult() {Name = c.Name, Score = c.Score};
    
    return req.CreateResponse(HttpStatusCode.OK, query.ToList());
}

public class Country : TableEntity
{
    public string Name { get; set; }
    public float Score { get; set; }
}

public class CountryResult
{
    public string Name { get; set; }
    public float Score { get; set; }
}
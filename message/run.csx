#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage.Table;



public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Keys> inputTable, CloudTable outTable, TraceWriter log)
{
    //Var
    dynamic data = await req.Content.ReadAsAsync<object>();
    string name = data?.name;
    string message = data?.message;
    
    //Validation
    if (name == null || message == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass the parameters in the request body");
    }
    
    //Query for country
    TableOperation operation = TableOperation.Retrieve<Country>(name, name);
    TableResult result = outTable.Execute(operation);
    Country c = (Country)result.Result;
    
    //Get key for cognitive service
    var key = inputTable.Where(p => p.RowKey == "cognitive").FirstOrDefault();
    
    //Calc the score
    var score = Decimal.Parse(GetScore(message, key.key))*100;
    var previous = Decimal.Parse(c.Score);
    var total = Int32.Parse(c.Total);
    var newScore = (previous*total + score)/(total+1);
    c.Score = Convert.ToString(Math.Round(newScore, 1));
    c.Total = Convert.ToString(total+1);
    
    //Update the country
    operation = TableOperation.Replace(c);
    outTable.Execute(operation);
    
    //Response
    var resp = new HttpResponseMessage()
    {
        Content = new StringContent(Convert.ToString(Math.Round(newScore, 1)))
    };
    
    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
    return resp;
}

static string GetScore(String message, String accountKey)
{
    string BaseUrl = "https://westus.api.cognitive.microsoft.com/";

    using (var client = new HttpClient())
    {
        client.BaseAddress = new Uri(BaseUrl);

        // Request headers.
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", accountKey);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Request body.
        byte[] byteData = Encoding.UTF8.GetBytes("{\"documents\":[" +
            "{\"id\":\"Message\",\"text\":\"" + message + "\"}]}");

        // Detect sentiment:
        var uri = "text/analytics/v2.0/sentiment";
        var response = CallEndpoint(client, uri, byteData);

        dynamic data = JObject.Parse(response);
        return data.documents[0].score;
    }
}

static String CallEndpoint(HttpClient client, string uri, byte[] byteData)
{
    using (var content = new ByteArrayContent(byteData))
    {
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = client.PostAsync(uri, content).Result;
        return  response.Content.ReadAsStringAsync().Result;
    }
}

public class Country : TableEntity
{
    public string Name { get; set; }
    public string Score { get; set; }
    public string Total { get; set; }
}

public class Keys : TableEntity
{
    public string key { get; set; }
}


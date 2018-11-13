using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*----------------------------------------------
Author: SDK Support Group
Company: Paya
Contact: sdksupport@paya.com
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!! Samples intended for educational use only!!!
!!!        Not intended for production       !!!
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
-----------------------------------------------*/

class MainClass
{
    public static void Main(string[] args)
    {
        Trans().Wait();
    }

    static async Task Trans()
    {


        HttpClient client = new HttpClient();

        // TH - Test Data is provided when you register at
        // https://developer.sandbox.payaconnect.com/applicationrequest
        // If you have any questions feel free to reach out
        // to us directly at sdksupport@paya.com 
        var locationID = "Insert Location ID";
        var contactID = "Insert Contact ID";
        var productTransID = "Insert Product Transaction ID";

        // TH - The Developer ID is assigned when registering with the
        // Paya Connect developer portal listed above.
        var developerID = "Insert Developer ID";

        // TH - User credentials
        var userID = "Insert User ID";
        var userAPIKey = "Insert User API Key";

        // Build URL
        var endpoint = "/v2/transactions";
        var url = "https://api.sandbox.payaconnect.com" + endpoint;

        // Additional variables
        var verb = "POST";
        var contentType = "application/json";
        // TH - 20170304 - Added the equivalent of vbCrLf in vb.net
        var nl = Environment.NewLine;

        // Console output for debugging.
        Console.WriteLine("EXECUTING THE FOLLOWING:");
        Console.WriteLine(nl);
        Console.WriteLine("URL: " + url);
        Console.WriteLine(nl);
        Console.WriteLine("Verb: " + verb);
        Console.WriteLine(nl);
        
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // It is possible submit PCI-sensitive card data to our
        // RESTful Direct API. This will place your solution in-scope for PCI
        // You will be required to provide your PCI certification from an
        // Approved Scanning Vendor listed at the link below.
        // https://www.pcisecuritystandards.org/assessors_and_solutions/approved_scanning_vendors
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        // Use JSON request from file.
        // This will submit a sale using card data, return the result and a vault token.
        StreamReader sr = new StreamReader("sale.json");
        string jsonReq = sr.ReadToEnd();
        // Replace "@" placeholders in the JSON file with variables.
        string request = jsonReq.Replace("@locationID", locationID).Replace("@contactID", contactID).Replace("@productTransID", productTransID);
        Console.WriteLine("Request:");
        Console.WriteLine(request);
        Console.WriteLine(nl);
        sr.Close();

        // Headers
        client.DefaultRequestHeaders.Add("developer-id", developerID);
        client.DefaultRequestHeaders.Add("user-api-key", userAPIKey);
        client.DefaultRequestHeaders.Add("user-id", userID);
        
        // Construct an HttpContent from a StringContent
        HttpContent payload = new StringContent(request.ToString());
        // and add the header to this object instance
        // optional: add a formatter option to it as well
        payload.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        // Send the request.
        var response = await client.PostAsync(url, payload);
        Console.WriteLine("<<<<<<<<Connection Opened>>>>>>>>");
        Console.WriteLine(nl);

        // Gather the response and display
        // >>Note: I have not included any response logic or error handling.
        // >>This will need to be included for production implementations.
        var responseString = await response.Content.ReadAsStringAsync();
        var respStatDesc = response.StatusCode.ToString();
        var respStatCode = (int)response.StatusCode;
        Console.WriteLine("Response Status Desc: " + respStatDesc);
        Console.WriteLine(nl);
        Console.WriteLine("Response Status Code: " + respStatCode);
        Console.WriteLine(nl);
        Console.WriteLine("Response:");
        Console.WriteLine(responseString);
        Console.WriteLine(nl);

        response.Dispose();
        Console.WriteLine("<<<<<<<<Connection Closed>>>>>>>>");
        Console.WriteLine(nl);
        Console.WriteLine("Transaction Ended");
        Console.WriteLine(nl);
        Console.WriteLine("Press Enter to exit:");
        Console.ReadLine();
    }

}

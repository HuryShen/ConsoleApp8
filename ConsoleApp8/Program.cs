using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;

namespace ConsoleApp8
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create("clientId")
            .WithTenantId("tenantId")
            .WithClientSecret("clientSecret")
            .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var result = await graphClient.Users
                    .Request()
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

            foreach (var user in result.CurrentPage)
            {
                Console.WriteLine(JsonConvert.SerializeObject(user));
            }
            Console.ReadLine();  // add this line or use "Ctrl + F5" to run the code
        }
    }
}
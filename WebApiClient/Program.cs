using System;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to authorize");

            Client client = new Client();
            var token = client.Authorize();
            //token.Wait();

            var resource = client.GetResource();
            //resource.Wait();

            Console.WriteLine(resource.Result);




        }
    }
}

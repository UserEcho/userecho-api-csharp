using System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Replace your_access_token with token, you can get it at https://[your_project_alias].userecho.com/settings/features/api/
            const string UE_ACCESS_TOKEN = "your_access_token";

            UserEcho.Client client = new UserEcho.Client(UE_ACCESS_TOKEN);
            
            //List forums
            //var response = client.Get("forums");

            //Create private forum (requires token with manage priviledge)
            var response = client.Post("forums",new {name = "New private forum", type="PRIVATE"});
            
            Console.WriteLine(string.Format("Response = \"{0}\"", response));
            Console.ReadKey();
        }
    }
}

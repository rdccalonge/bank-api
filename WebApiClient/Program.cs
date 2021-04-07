using System;
using System.Net.Http ;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        //static async Task Main(string[] args)
        //{

        //    //Console.WriteLine("Hello World!");
        //    //using(HttpClient client = new HttpClient())
        //    //{
        //    //    var response = await client.GetAsync("http://localhost:46198/values");
        //    //    response.EnsureSuccessStatusCode();
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        string message = await response.Content.ReadAsStringAsync();
        //    //        Console.WriteLine(message);
        //    //    }
        //    //    else
        //    //    {
        //    //        Console.WriteLine($"response error code: { response.StatusCode}");
        //    //    }
        //    //}
        //}
        public static void Main(string[] args)
        {
      
            // show home screen
            new Main().Execute();
        }
    }
}

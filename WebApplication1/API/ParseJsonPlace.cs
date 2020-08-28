using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Model;

namespace WebApplication1.API
{
    public class ParseJsonPlace
    {


        public IEnumerable<Location> ReadUrlAsync(String Url, String urlParameter)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = client.GetAsync(urlParameter).Result;
            

            if (responseMessage.IsSuccessStatusCode)
            {

               
                var data = responseMessage.Content.ReadAsAsync<IEnumerable<Location>>().Result;

                return data;

                //Make sure to add a reference to System.Net.Http.Formatting.dll

            }
            else
            {
                return null;
            }

        }
    }
}

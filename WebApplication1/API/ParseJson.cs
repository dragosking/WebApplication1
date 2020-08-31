using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Model;
using Newtonsoft.Json;

namespace WebApplication1.API
{
    public class ParseJson
    {

        public Rootobject ParseUrl(String Url, String UrlParameters)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = client.GetAsync(UrlParameters).Result;


            if (responseMessage.IsSuccessStatusCode)
            {


                var data = responseMessage.Content.ReadAsStringAsync().Result;
                var answer = JsonConvert.DeserializeObject<Rootobject>(data);

                //return cleanData(answer);
                return answer;
                

                //Make sure to add a reference to System.Net.Http.Formatting.dll

            }
            else
            {
                return null;
            }

        }

        private Rootobject cleanData(Rootobject first)
        {
            Rootobject after=new Rootobject();
            after.approvedTime = first.approvedTime;
            DateTime st = after.approvedTime;
            for(int i=0; i < first.timeSeries.Length; i++)
            {
                DateTime apa = first.timeSeries[i].validTime;
                for(int j=0; j < first.timeSeries[i].parameters.Length; j++ )
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        after.timeSeries[i].parameters[0].values = first.timeSeries[i].parameters[j].values;
                    }
                }
            }

            return after;

        }

    

    }
}

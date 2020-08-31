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

                return cleanData(answer);
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
            Rootobject after;
            Timesery[] seriesTemp=new Timesery[first.timeSeries.Length];
            Parameter[] parasTemp=new Parameter[1];

        
            DateTime appTemp = first.approvedTime;
            for (int i = 0; i < first.timeSeries.Length; i++)
            {
                DateTime validTemp = first.timeSeries[i].validTime;

                for(int j=0; j < first.timeSeries[i].parameters.Length; j++ )
                {
                    if (first.timeSeries[i].parameters[j].name.Equals("t"))
                    {
                        parasTemp[0] = first.timeSeries[i].parameters[j];
                    }

                }

                seriesTemp[i] = new Timesery
                {
                    validTime = validTemp,
                    parameters = parasTemp
                };
         
            }


            after = new Rootobject
            {
                approvedTime = appTemp,
                timeSeries=seriesTemp
            };

            return after;

        }

    

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Device.Location;

namespace RoutingServer
{
    internal class RESTClient
    {
        private string currentContract = "Paris";
        private string apiKey = "12345678";

        public RESTClient()
        {
            currentContract = "Paris";
        }

        public RESTClient(string apiKey) 
        {
            this.apiKey = apiKey;
            currentContract = "Paris";
        }
        public List<JCDContract> getAllContracts()
        {
            string query;
            string url;
            string response;
            // 1.1: Retrieve all contracts.
            query = "apiKey=" + apiKey;
            url = "https://api.jcdecaux.com/vls/v3/contracts";
            response = JCDecauxAPICall(url, query).Result;
            List<JCDContract> allContracts = JsonSerializer.Deserialize<List<JCDContract>>(response);
            return allContracts;
        }

        public List<JCDStation> getAllStations(string contract)
        {
            string query;
            string url;
            string response;
            url = "https://api.jcdecaux.com/vls/v3/stations";
            query = "contract=" + contract + "&apiKey=" + apiKey;
            response = JCDecauxAPICall(url, query).Result;
            List<JCDStation> allStations = JsonSerializer.Deserialize<List<JCDStation>>(response);
            return allStations;
        }

        public JCDStation getClosestStation(Position pos)
        {
            GeoCoordinate posCoordinates = new GeoCoordinate(pos.latitude, pos.longitude);

            Double minDistance = -1;
            List<JCDStation> allStations = getAllStations(currentContract);
            JCDStation closestStation = allStations.First<JCDStation>();
            foreach (JCDStation item in allStations)
            {
                // Find the current station's position.
                GeoCoordinate candidatePos = new GeoCoordinate(item.Location.Latitude, item.Location.Longitude);
                // And compare its distance to the chosen one to see if it is closer than the current closest.
                Double distanceToCandidate = posCoordinates.GetDistanceTo(candidatePos);

                if (distanceToCandidate != 0 && (minDistance == -1 || distanceToCandidate < minDistance))
                {
                    closestStation = item;
                    minDistance = distanceToCandidate;
                }
            }
            return closestStation;
        }



        private static async Task<string> JCDecauxAPICall(string url, string query)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "?" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    } 
    public class JCDContract
    {
        public string name { get; set; }
    }

    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}


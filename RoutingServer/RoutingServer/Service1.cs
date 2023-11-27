using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        public Itinerary GetItinerary(GeoCoordinate origin, GeoCoordinate destination)
        {
            Itinerary itinerary = new Itinerary
            {
                Origin = origin,
                Destination = destination,
                Station1 = new JCDStation()
            };

            return itinerary;
        }

        public Itinerary GetNewItinerary(Itinerary itinerary, GeoCoordinate position)
        {
            if (itinerary == null)
            {
                throw new ArgumentNullException("itinerary");
            }
            return itinerary;
            
        }
    }
}

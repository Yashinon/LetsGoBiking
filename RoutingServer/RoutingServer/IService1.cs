using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Itinerary GetItinerary(GeoCoordinate origin, GeoCoordinate destination);

        [OperationContract]
        Itinerary GetNewItinerary(Itinerary itinerary, GeoCoordinate position);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "RoutingServer.ContractType".
    [DataContract]
    public class Itinerary
    {
        GeoCoordinate origin;
        JCDStation station1;
        JCDStation station2;
        GeoCoordinate destination;

        [DataMember]
        public JCDStation Station1
        {
            get { return station1; }
            set { station1 = value; }
        }

        [DataMember]

        public JCDStation Station2
        {
            get { return station2; }
            set { station2 = value; }
        }

        [DataMember]

        public GeoCoordinate Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        [DataMember]

        public GeoCoordinate Destination
        {
            get { return destination; }
            set { destination = value; }
        }
    }

    [DataContract]

    public class JCDStation
    {
        string name;
        GeoCoordinate location;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public GeoCoordinate Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InvasionHotline
{
    [DataContract]
    public class District
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Population { get; set; }
    }

    [DataContract]
    public class DistrictRequest
    {
        [DataMember]
        public long lastUpdated { get; set; }

        [DataMember]
        public int totalPopulation { get; set; }

        [DataMember]
        public JsonObject populationByDistrict { get; set; }
    }
}

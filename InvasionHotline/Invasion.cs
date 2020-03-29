using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InvasionHotline
{
    /// <summary>
    /// DataModel for TTRR Invasions
    /// </summary>
    [DataContract]
    public class Invasion
    {
        /// <summary> The District/Server where the invasion is happening </summary>
        [DataMember]
        public string District { get; set; }

        /// <summary> Name of the Cog that we're hunting </summary>
        [DataMember]
        public string Cog { get; set; }

        [IgnoreDataMember]
        public PackIconKind CogLogo { get; set; }

        /// <summary> Timestamp given as an integer-64 value </summary>
        [DataMember]
        public long Ticks { get; set; }

        /// <summary> # of Cogs killed / # of Cogs left </summary>
        [DataMember]
        public string Progress { get; set; }

        /// <summary>
        /// Blank Constructor
        /// </summary>
        public Invasion() { }
    }

    [DataContract]
    public class ToonHQInvasionRequest
    {
        [DataMember]
        public JsonArray invasions { get; set; }
    }

    [DataContract]
    public class ToonHQInvasionData
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public float defeat_rate { get; set; }

        [DataMember]
        public long start_time { get; set; }

        [DataMember]
        public int defeated { get; set; }

        [DataMember]
        public int total { get; set; }

        [DataMember]
        public long as_of { get; set; }

        [DataMember]
        public bool manual { get; set; }

        [DataMember]
        public int reports { get; set; }

        [DataMember]
        public string district { get; set; }

        [DataMember]
        public string cog { get; set; }
    }
}

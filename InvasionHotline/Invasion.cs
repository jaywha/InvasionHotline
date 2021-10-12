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
        public JsonArray Invasions { get; set; }
    }

    [DataContract]
    public class ToonHQInvasionData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public float Defeat_rate { get; set; }

        [DataMember]
        public long Start_time { get; set; }

        [DataMember]
        public int Defeated { get; set; }

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public long As_of { get; set; }

        [DataMember]
        public bool Manual { get; set; }

        [DataMember]
        public int Reports { get; set; }

        [DataMember]
        public string District { get; set; }

        [DataMember]
        public string Cog { get; set; }
    }
}

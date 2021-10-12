using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InvasionHotline
{
    public class SillyMeter
    {
        public static readonly string User_Agent_String = "Invasion_Hotline";
    }

    [DataContract]
    public class SillyMeterRequest
    {
        [DataMember]
        public JsonArray Rewards;

        [DataMember]
        public JsonArray RewardDescriptions;

        [DataMember]
        public JsonArray RewardPoints;

        [DataMember]
        public string Winner;

        [DataMember]
        public string State;

        [DataMember]
        public long NextUpdateTimestamp;

        [DataMember]
        public long HP;

        [DataMember]
        public long AsOf;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionHotline
{
    public class Street
    {
        #region Properties
        /// <summary>
        /// Street Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Originating Playground
        /// </summary>
        public Playgrounds HomePlayground { get; set; }

        /// <summary>
        /// Neighboring Street
        /// </summary>
        public Street ConnectedStreet { get; set; }

        /// <summary>
        /// Percentage of bot population that are bossbots
        /// </summary>        
        public int BossbotPercent { get; set; }

        /// <summary>
        /// Percentage of bot population that are Lawbots
        /// </summary>
        public int LawbotPercent { get; set; }

        /// <summary>
        /// Percentage of bot population that are Cashbots
        /// </summary>
        public int CashbotPercent { get; set; }

        /// <summary>
        /// Percentage of bot population that are Sellbots
        /// </summary>
        public int SellbotPercent { get; set; }

        /// <summary>
        /// Avg. number of steps it takes to go from end-to-end
        /// </summary>
        public int NumberOfSteps { get; set; }
        #endregion

        public Street(string name, Playgrounds homePlayground = Playgrounds.Toontown_Central,
            int bossPercent = 0, int lawPercent = 0, int cashPercent = 0, int sellPercent = 0)
        {
            Name = name;
            HomePlayground = homePlayground;

            BossbotPercent = bossPercent;
            LawbotPercent = lawPercent;
            CashbotPercent = cashPercent;
            SellbotPercent = sellPercent;
        }

        public override string ToString() => Name;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLS
{
    class AllAccessories
    {
        public static void UpdateBuffsPost(Terraria.Player player)
        {
            player.accCalendar = true;
            player.accCompass = 1;
            player.accCritterGuide = true;
            player.accDepthMeter = 1;
            player.accDivingHelm = true;
            player.accDreamCatcher = true;
            player.accFishFinder = true;
            player.accFishingLine = true;
            player.accFlipper = true;
            player.accJarOfSouls = true;
            player.accMerman = true;
            player.accOreFinder = true;
            //player.accRunSpeed += 6.75f;
            player.accStopwatch = true;
            player.accTackleBox = true;
            player.accThirdEye = true;
            player.accWatch = 3;
            player.accWeatherRadio = true;
        }
    }
}

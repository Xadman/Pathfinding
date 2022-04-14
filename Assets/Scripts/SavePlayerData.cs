using System;
namespace Com.KevinNipper.Pathfinding
{


    [Serializable]
    public class SavePlayerData
    {
        public int level;
        public int xpPoints;
        public int coins;
        public float currentPositionX, currentPositionY, currentPositionZ;


        public SavePlayerData()
        {
            coins = 0;
            level = 1;
            xpPoints = 0;
            currentPositionX = 0;
            currentPositionY = 0;
            currentPositionZ = 0;
        }

        public SavePlayerData(Player player)
        {
            coins = player.coins;
            level = player.level;
            xpPoints = player.xpPoints;
            currentPositionX = player.currentPosition.x;
            currentPositionY = player.currentPosition.y;
            currentPositionZ = player.currentPosition.z;
        }




    }
}
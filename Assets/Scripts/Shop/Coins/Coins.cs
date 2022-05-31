using UnityEngine;

namespace Shop.Coins
{
    public class Coins : MonoBehaviour
    {
        private static int coin = 0;

        public static int GETCoin()
        {
            return coin;
        }

        public static void PlusCoin(int value)
        {
            coin += value;
        }

        private static bool CheckPrice(int value)
        {
            if((coin-value) < 0)
                return false;
            
            return true;
        }

        public static void MinusCoin(int value)
        {
            if (CheckPrice(value))
                coin -= value;
        }
    }
}

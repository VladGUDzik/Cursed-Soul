using UnityEngine;

namespace Shop.Coins
{
    public class Coins : MonoBehaviour
    {
        private static int _coin;

        public static int GetCoin()
        {
            return _coin;
        }

        public static void PlusCoin(int value)
        {
            _coin += value;
        }

        private static bool CheckPrice(int value)
        {
            return (_coin-value) >= 0;
        }

        public static void MinusCoin(int value)
        {
            if (CheckPrice(value))
                _coin -= value;
        }
    }
}


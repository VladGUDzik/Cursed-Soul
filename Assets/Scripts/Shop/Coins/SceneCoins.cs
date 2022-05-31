using UnityEngine;
using UnityEngine.UI;

namespace Shop.Coins
{
    public class SceneCoins : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        private void Update()
        {
            _text.text = Coins.GETCoin().ToString();
        }
    }
}

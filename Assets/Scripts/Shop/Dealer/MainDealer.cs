using UnityEngine;

namespace Shop.Dealer
{
    public class MainDealer : MonoBehaviour
    {
        public GameObject startsentence;
        public GameObject coins;
        public GameObject dialog;

        private void Start()
        {
            startsentence.SetActive(false);
            coins.SetActive(false);
            dialog.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                startsentence.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                startsentence.SetActive(false);
                coins.SetActive(false);
                dialog.SetActive(false);     
            }
        }

        public void OnCoinsClicked()
        {
            startsentence.SetActive(false);
            coins.SetActive(true);
        }

        public void OnDialogClicked()
        {
            startsentence.SetActive(false);
            dialog.SetActive(true);
        }

        public void OnExitClicked()
        {
            dialog.SetActive(false);
            coins.SetActive(false);
            startsentence.SetActive(true);
        }

        public void BuyCoin()
        {
            Coins.Coins.PlusCoin(20);
        }

        public void SellCoins()
        {
            Coins.Coins.MinusCoin(20);
        }
    }
}

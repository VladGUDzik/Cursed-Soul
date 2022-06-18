using System.Collections.Generic;
using UnityEngine;

namespace Shop.Scroll_Menu
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ItemView template;
        [SerializeField] private Transform content;
        [SerializeField] private ShopScroll shopScroll;
        [SerializeField] private List<Item> items;

        private void Awake()
        {
            List<ItemView> spawnedItems = new List<ItemView>();

            foreach (var item in items)
            {
                var spawnedItem = Instantiate(template, content);

                spawnedItem.Initialize(item);
                spawnedItems.Add(spawnedItem);
            }

            shopScroll.Initialize(spawnedItems);
        }
    }
}

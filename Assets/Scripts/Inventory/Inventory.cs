using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public DB data;
    
        public List<ItemInventory> items = new List<ItemInventory>();

        public GameObject gameObjShow;

        public GameObject inventoryMainObject;
        public int maxCount;

        public UnityEngine.Camera cam;
        public EventSystem es;

        public int currentID;
        public ItemInventory currentItem;

        public RectTransform movingObj;
        public Vector3 offset;

        public void AddItem(int id, Item item, int count)
        {
            items[id].id = item.id;
            items[id].count = count;
            items[id].itemGameObject.GetComponent<Image>().sprite = item.image;
        
            if(count > 1 && item.id != 0)
            {
                items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
            }
            else
            {
                items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
        }
    
        public void AddInventoryItem(int id, ItemInventory invItem)
        {
            items[id].id = invItem.id;
            items[id].count = invItem.count;
            items[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].image;
            
            if(invItem.count > 1 && invItem.id != 0)
            {
                items[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
            }
            else
            {
                items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
        }

        public void AddGraphics()
        {
            for (int i = 0; i < maxCount; i++)
            {
                GameObject newItem = Instantiate(gameObjShow,inventoryMainObject.transform) as GameObject;
            
                newItem.name = i.ToString();

                ItemInventory ii = new ItemInventory();
                ii.itemGameObject = newItem;

                RectTransform rt = newItem.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(0, 0, 0);
                rt.localScale = new Vector3(1, 1, 1);
                newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

                Button tempButton = newItem.GetComponent<Button>();
            
                tempButton.onClick.AddListener(delegate { SelectObject(); });
            
                items.Add(ii);
            }
        }

        public void UpdateInventory()
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id != 0 && items[i].count > 1)
                {
                    items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
                }
                else
                {
                    items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
                }

                items[i].itemGameObject.GetComponentInChildren<Image>().sprite = data.items[items[i].id].image;
            }
        }
    
        public void SelectObject()
        {
            if (currentID == -1)
            {
                currentID = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentID]);
                movingObj.gameObject.SetActive(true);
                movingObj.GetComponent<Image>().sprite = data.items[currentItem.id].image;
            
                AddItem(currentID,data.items[0],0);
            }
            else
            {
                AddInventoryItem(currentID, items[int.Parse(es.currentSelectedGameObject.name)]);
            
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name),currentItem);
                currentID = -1;
                movingObj.gameObject.SetActive(false);
            }
        }

        public void MoveObject()
        {
            Vector3 pos = Input.mousePosition + offset;
            pos.z = inventoryMainObject.GetComponent<RectTransform>().position.z;
            movingObj.position = cam.ScreenToWorldPoint(pos);
        }

        public ItemInventory CopyInventoryItem(ItemInventory old)
        {
            ItemInventory New = new ItemInventory();
        
            New.id = old.id;
            New.itemGameObject = old.itemGameObject;
            New.count = old.count;
        
            return New;
        }
    }

    [System.Serializable]

    public class ItemInventory
    {
        public int id;
        public GameObject itemGameObject;

        public int count;
    }
}
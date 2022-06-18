using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB : MonoBehaviour
{
   public List<Shop.Scroll_Menu.Item> items = new List<Shop.Scroll_Menu.Item>();
}

[System.Serializable]

public partial class Item
{
    public int id;
    public string name;
    public Sprite image;
}
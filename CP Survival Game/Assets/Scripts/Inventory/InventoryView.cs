using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public GameObject inventoryItem;
    public List<GameObject> ItemsAdded;
    public Image panel;
    public bool openclosed;
    public GameObject items;
    public GameObject controls;
    public static InventoryView Instance
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        ItemsAdded = new List<GameObject>();
        Instance = this;
        for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
        {
            GameObject item = Instantiate(inventoryItem, transform);
            item.SetActive(true);
            item.GetComponent<Image>().sprite = Inventory.Instance.inventory[i].icon;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!openclosed)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                var tempColor = panel.color;
                tempColor.a = 100f;
                panel.color = tempColor;
                openclosed = true;
                Time.timeScale = 1;
            }

            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                var tempColor = panel.color;
                tempColor.a = 0f;
                panel.color = tempColor;
                openclosed = false;
                Time.timeScale = 0;
            }
        }   
    }
    public void AddItemInInventory(InventoryItemData itemData)
    {

        foreach(InventoryItemQuantity item in transform.GetComponentsInChildren<InventoryItemQuantity>(true))
         {
                if(item.ItemName==itemData.name)
                {
                    item.IncreaseQuantity();
                    return;
                }
        }
        
      //  else
        {
            GameObject item = Instantiate(inventoryItem, transform);
            item.name = itemData.name;
            item.SetActive(true);
            item.GetComponent<Image>().sprite = itemData.icon;
            ItemsAdded.Add(item);
            item.GetComponent<InventoryItemQuantity>().ItemName = itemData.name;
            item.transform.GetChild(0).GetComponent<Text>().text = itemData.name;
        }
    }

    public void OpenPanel()
    {
        if (!openclosed)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            var tempColor = panel.color;
            tempColor.a = 100f;
            panel.color = tempColor;
            openclosed = true;
            controls.SetActive(false);
            
        }

        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            var tempColor = panel.color;
            tempColor.a = 0f;
            panel.color = tempColor;
            openclosed = false;
            controls.SetActive(true);
            
        }
    }

}

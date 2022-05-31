using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    private static Inventory instance;
    public List<InventoryItemData> inventory;
    public InventoryItemData[] items;
    public int[] count;
    public bool load;

    private void Start()
    {
        Instance = this;
        inventory = new List<InventoryItemData>();

        count = new int[items.Length];
        if(load == true)
        {
            int[] j = PlayerDataSaver.LoadGame().quantity;

            for(int i = 0; i <items.Length; i++)
            {
                if(j[i] > 0)
                {
                    Debug.Log("storing");
                    for(int k = 0; k < j[i]; k++)
                    {
                        Store(items[i]);
                        Debug.Log("stored");
                    }
                }
            }
        }
        
    }
    public static Inventory Instance
    {
        get;
        private set;
        
    }
    #endregion


    /*public static InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }

    public Inventory(InventoryItemData itemData)
    {
        data = itemData;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }*/

    //public Interactable currentSelected;



    public void Store(InventoryItemData itemData)
    {
        //store
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].id == itemData.id)
            {
                count[i]++;
            }
        }
        if(!inventory.Contains(itemData))
        {
            inventory.Add(itemData);
            InventoryView.Instance.AddItemInInventory(itemData);
        }      
        else
        {
            InventoryView.Instance.UpdateItemInInventory(itemData);
        }
        
    }

    public void Use(InventoryItemData itemData)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == itemData.id)
            {
                count[i]--;
            }
        }
    }


    public void Bag()
    {
        Debug.Log("Bag Opened");
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if(Input.GetKey(KeyCode.L))
        {
            PlayerDataSaver.SaveGame(this);
        }
    }

    public void SaveInventory()
    {
        PlayerDataSaver.SaveGame(this);
    }

}

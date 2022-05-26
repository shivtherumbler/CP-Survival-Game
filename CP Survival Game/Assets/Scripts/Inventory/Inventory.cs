using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    private static Inventory instance;
    public List<InventoryItemData> inventory;
    private void Start()
    {
        inventory = new List<InventoryItemData>();
        Instance = this;
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
        inventory.Add(itemData);
        InventoryView.Instance.AddItemInInventory(itemData);
    }


    public void Bag()
    {
        Debug.Log("Bag Opened");
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}

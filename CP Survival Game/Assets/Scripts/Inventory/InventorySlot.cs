using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    /*#region Singleton
    private static InventorySlot instance;

    public static InventorySlot Instance
    {
        get
        {
            if (instance == null) instance = new InventorySlot();
            return instance;
        }
    }
    #endregion
    

    public Dictionary<InventoryItemData, Inventory> itemDictionary;
    public List<Inventory> inventory { get; private set; }

    private void Awake()
    {
        inventory = new List<Inventory>();
        itemDictionary = new Dictionary<InventoryItemData, Inventory>();
    }

    public Inventory Get(InventoryItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out Inventory value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out Inventory value))
        {
            value.AddToStack();
        }
        else
        {
            Inventory newItem = new Inventory(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void Remove(InventoryItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out Inventory value))
        {
            value.RemoveFromStack();

            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(itemData);
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}


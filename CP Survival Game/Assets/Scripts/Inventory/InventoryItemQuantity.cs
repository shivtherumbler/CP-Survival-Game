using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemQuantity : MonoBehaviour
{
    public string ItemName;
    public Text AvailableQuantity;
    public int Quantity;
    public Text Name;
    public Button use;
    public Button remove;
    public Animator anim;
    public GameObject player;
    public PlayerHealthManager manager;
    public InventoryItemData itemData;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        manager = player.GetComponent<PlayerHealthManager>();
    }

    public void IncreaseQuantity()
    {
        Quantity++;
        AvailableQuantity.text = "x" + Quantity.ToString();

    }

    public void UseItem()
    {
        if (Name.text == "Medkit")
        {
            Debug.Log("Health Increased");
            manager.health = manager.maxhealth;
        }
        else if (Name.text == "Beer")
        {
            
            Debug.Log("You are drunk!");
            if(manager.health < manager.maxhealth)
            {
                manager.health++;
            }
        }

        if (Quantity > 1)
        {
            
            Quantity--;
            AvailableQuantity.text = "x" + Quantity.ToString();
            Inventory.Instance.Use(itemData);
        }
        else 
        {
            Quantity--;
            AvailableQuantity.text = "x" + Quantity.ToString();
            Inventory.Instance.Use(itemData);
            Destroy(gameObject, 0.1f);
        }

        
    }

    public void DropItem()
    {
        Debug.Log("Item Dropped");

        Destroy(gameObject, 0.1f);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public bool canInteract;
    public bool isCollectible;
    public InventoryItemData itemData;
    public float radius = 3f;
    public bool pickup;
    public GameObject canvas;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnInteract()
    {
        //FindObjectOfType<StarterAssets.StarterAssetsInputs>().EnableCollection(true);
        //Inventory.Instance.currentSelected = this;
        canInteract = false;
    }

    public void OnExitInteraction()
    {
        canInteract = true;
    }

    public void Collect()
    {

        //FindObjectOfType<StarterAssets.StarterAssetsInputs>().EnableCollection(true);
        gameObject.SetActive(false);
        //Inventory.Instance.Store(itemData);


    }

    private void Update()
    {
        PickupPC();
        
    }

    public void Pickup()
    {
        if (pickup == true)
        {
                
                //FinStarterAssets.StarterAssetsInputs.Instance.EnableCollection(true);
                StarterAssets.StarterAssetsInputs.Instance.DisableCollection(true);
                //GetComponent<ItemPickup>().OnPickupItem();
                if (itemData != null)
                    Inventory.Instance.Store(itemData);
                gameObject.SetActive(false);
            
        }
    }

#if !UNITY_IOS || !UNITY_ANDROID
    public void PickupPC()
    {
        if (pickup == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //FinStarterAssets.StarterAssetsInputs.Instance.EnableCollection(true);
                StarterAssets.StarterAssetsInputs.Instance.DisableCollection(true);
                //GetComponent<ItemPickup>().OnPickupItem();
                if (itemData != null)
                    Inventory.Instance.Store(itemData);
                gameObject.SetActive(false);
            }
        }
    }
#endif
}

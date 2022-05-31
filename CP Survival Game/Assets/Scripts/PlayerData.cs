using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] quantity;
    public PlayerData(Inventory inventory)
    {
        quantity = new int[inventory.count.Length];
        for (int i = 0; i < quantity.Length; i++)
        {
            quantity[i] = inventory.count[i];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    //public GameObject SMG;
    //public GameObject GrenadeLauncher;
    //public GameObject FlameThrower;
    public static int WeaponID;
    public static int currentWeaponID;
    public static string WeaponName;
    public static float SMGAmmo;
    public static float GrenadeAmmo;
    public static float FlameAmmo;
    public static float AmmoAmt;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        WeaponID = 1;
        AmmoAmt = 20f;
        WeaponName = "Pistol";
        
        currentWeaponID = WeaponID;
}

    // Update is called once per frame
    void Update()
    {
        

        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            WeaponID = 0;
        }

       if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponID = 1;
        }
        if (SMG.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                WeaponID = 2;
            }
        }
        if (GrenadeLauncher.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                WeaponID = 3;
            }
        }
        if (FlameThrower.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                WeaponID = 4;
            }
        }      */  

    }
}

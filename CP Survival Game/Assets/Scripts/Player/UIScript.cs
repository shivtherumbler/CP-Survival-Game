using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //[SerializeField] Text WeaponType;
    [SerializeField] Text Ammo;
    //[SerializeField] Text AmmoLabel;
    public int gunno;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WeaponType.text = SaveScript.WeaponName;

        if(gunno == 1)
        {
            Ammo.text = "Ammo: " + SaveScript.AmmoAmt.ToString();
        }

        if(gunno == 2 )
        {
            Ammo.text = "Ammo: " + SaveScript.SMGAmmo.ToString();
        }

        if (gunno == 3)
        {
            Ammo.text = "Ammo: " + SaveScript.GrenadeAmmo.ToString();
        }

        if (gunno == 4)
        {
            //AmmoLabel.text = "Fuel";
            Ammo.text = "Fuel: " + (Mathf.Round(SaveScript.FlameAmmo).ToString());
        }
    }
}

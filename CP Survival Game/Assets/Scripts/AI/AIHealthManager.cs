using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthManager : MonoBehaviour
{
    public int health = 3;
    public int maxhealth = 4;

    [SerializeField] float DestroyTime = 10f;

    public void SwitchOff()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        Invoke("SwitchOff", DestroyTime);
    }
}

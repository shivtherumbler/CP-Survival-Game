using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float DestroyTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, DestroyTime);
    }

    private void OnEnable()
    {
        Invoke("SwitchOff", DestroyTime);
        //gameObject.SetActive(false);
    }

    public void SwitchOff()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

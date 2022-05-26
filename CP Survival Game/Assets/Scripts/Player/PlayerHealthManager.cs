using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 3;
    public int maxhealth = 4;
    public GameObject normalhealth;
    public GameObject dangerhealth;
    public GameObject youdied;
    public CinemachineVirtualCamera cam;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health >= 2)
        {
            normalhealth.SetActive(false);
            dangerhealth.SetActive(false);
            youdied.SetActive(false);
        }
        else if(health == 1)
        {
            normalhealth.SetActive(true);
            dangerhealth.SetActive(false);
            youdied.SetActive(false);
        }
        else if(health == 0)
        {
            normalhealth.SetActive(false);
            dangerhealth.SetActive(true);
            youdied.SetActive(false);
        }
        else
        {
            anim.SetBool("Death", true);
            cam.Priority = 12;
            youdied.SetActive(true);
        }
    }
}

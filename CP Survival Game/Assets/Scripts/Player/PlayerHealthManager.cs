using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHealthManager : MonoBehaviour
{
    public float health = 3;
    public float maxhealth = 4;
    public GameObject normalhealth;
    public GameObject dangerhealth;
    public GameObject youdied;
    public CinemachineVirtualCamera cam;
    public Text healthtext;
    public Text status;

    public bool invincible;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthtext.text = "Health: " + ((health / maxhealth)*100f) + "%";

        if(health >= 2)
        {
            normalhealth.SetActive(false);
            dangerhealth.SetActive(false);
            youdied.SetActive(false);
            status.text = "Good";
            status.color = Color.green;
        }
        else if(health == 1)
        {
            normalhealth.SetActive(true);
            dangerhealth.SetActive(false);
            youdied.SetActive(false);
            status.text = "Okay";
            status.color = Color.yellow;
        }
        else if(health == 0)
        {
            normalhealth.SetActive(false);
            dangerhealth.SetActive(true);
            youdied.SetActive(false);
            status.text = "Danger";
            status.color = Color.red;
        }
        else
        {
            anim.SetBool("Death", true);
            cam.Priority = 12;
            youdied.SetActive(true);
        }

        if(invincible == true)
        {
            health = maxhealth;
        }

    }

    public void HealthMax()
    {
        invincible = true;
    }

    public void NormalHealth()
    {
        invincible = false; 
    }

    public void PauseHealth()
    {
        invincible = !invincible;
    }
}

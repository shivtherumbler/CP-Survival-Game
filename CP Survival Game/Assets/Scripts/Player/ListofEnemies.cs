using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListofEnemies : MonoBehaviour
{
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
       enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(!enemies.Contains(other.gameObject))
            enemies.Add(other.gameObject);
        }
        
    }

    public void Kick()
    {
        for(int i = 0; i <enemies.Count; i++)
        {
            //enemies[i].transform.rotation = Quaternion.Slerp(enemies[i].transform.rotation, Quaternion.LookRotation(new Vector3(enemies[i].transform.position.x, transform.position.y, enemies[i].transform.position.z)), 1);
                //(new Vector3(enemies[i].transform.position.x, transform.position.y, enemies[i].transform.position.z));
               
            enemies[i].GetComponent<Animator>().SetTrigger("Kick");
            enemies[i].GetComponent<AIHealthManager>().health--;

            if (enemies[i].GetComponent<AIHealthManager>().health == 0)
            {
                enemies[i].GetComponent<Animator>().SetBool("Death", true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (enemies.Contains(other.gameObject))
                enemies.Remove(other.gameObject);
        }

    }
}

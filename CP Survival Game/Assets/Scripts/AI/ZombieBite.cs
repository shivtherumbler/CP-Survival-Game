using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBite : MonoBehaviour
{

    public GameObject player;
    public RaycastHit hitinfo;
    [SerializeField] float TimeToBite;
    [SerializeField] float TimeInTrigger;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position + transform.up, player.transform.position - transform.position, out hitinfo, 10))
        {
            Debug.Log(hitinfo.transform.gameObject.name);
            if (hitinfo.transform.gameObject == player)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 1)
                {
                    TimeInTrigger += Time.deltaTime;
                    if (TimeInTrigger >= TimeToBite)
                    {
                        //StartCoroutine(MovePlayerTowardsTrigger(1.25f));
                        //player.GetComponent<Animator>().SetTrigger("ZombieBite");
                    }
                    //StartCoroutine(MovePlayerTowardsTrigger(1.25f));
                    
                }
                else
                {
                    TimeInTrigger = 0;
                    anim.ResetTrigger("ZombieBite");
                    //StopCoroutine(MovePlayerTowardsTrigger(1.25f));
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Roar"))
            {
                TimeInTrigger += Time.deltaTime;
                if (TimeInTrigger >= TimeToBite)
                {
                    StartCoroutine(MovePlayerTowardsTrigger(1.25f));
                    //player.GetComponent<Animator>().SetTrigger("ZombieBite");
                }
            }
            
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            TimeInTrigger = 0;
            //StopCoroutine(MovePlayerTowardsTrigger(1.25f));

        }
    }
    IEnumerator MovePlayerTowardsTrigger(float Duration)
    {
        float t = 0;
        
        Vector3 MovePosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        while (t < Duration)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, MovePosition, t / Duration);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(player.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y  + 180, player.transform.rotation.eulerAngles.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        anim.SetTrigger("ZombieBite");
        //player.GetComponent<Animator>().SetTrigger("ZombieBite");

    }
}

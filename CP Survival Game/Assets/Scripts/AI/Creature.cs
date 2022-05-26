using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] points;
    public Transform player;
    public Animator anim;
    public Transform bite;
    public bool gothit;
    public bool idleattack;

    public GameObject[] goalLocations;
    private float sppedMult;
    private float detectionRadius = 25;
    private float fleeRadius = 15;
    public RaycastHit hitinfo;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
        ResetAgent();
    }
    private void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void ResetAgent()
    {
        //anim.SetFloat("wOffset", Random.Range(0,1));
        //anim.SetTrigger("isWalking");
        float sppedMult = Random.Range(0.35f, 1.5f);
        //anim.SetFloat("speedMult", sppedMult);
        agent.speed *= sppedMult;
        agent.angularSpeed = 120;
        agent.ResetPath();
        if (goalLocations.Length > 1)
        {
            anim.SetBool("walk", true);
            anim.SetBool("eat", false);

        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("eat", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToTarget();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roar") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roar1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roar2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roar3") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roar4") || anim.GetCurrentAnimatorStateInfo(0).IsName("Stand") || anim.GetCurrentAnimatorStateInfo(0).IsName("RunBack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Death") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Eat") || anim.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = 3.5f;
        }

        if(idleattack == false)
        {
            if (agent.destination != player.transform.position)
            {
                if (goalLocations.Length > 1)
                {
                    if (agent.remainingDistance < 2f)
                    {
                        ResetAgent();
                        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

                    }
                    else
                    {
                        anim.SetBool("walk", true);

                    }
                }
                else
                {
                    anim.SetBool("eat", true);
                    //anim.SetBool("walk", false);
                }
            }
            else
            {
                anim.SetBool("eat", false);
            }

            if (Physics.Raycast(transform.position + transform.up, player.transform.position - transform.position, out hitinfo, 10))
            {
                Debug.Log(hitinfo.transform.gameObject.name);
                if (hitinfo.transform.gameObject == player.gameObject)
                {

                    anim.SetBool("walk", false);
                    //agent.SetDestination(player.transform.position);
                    idleattack = true;

                }
               
                anim.SetBool("eat", false);
            }
            else
            {
                //anim.SetBool("run", false);
                //anim.SetBool("walk", true);
                //anim.SetBool("attack", false);
                //anim.SetBool("eat", false);
                //CancelInvoke("MoveToTarget");
                agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
                /*player.GetComponent<Player>().animator.SetBool("Battle", false);
                //player.GetComponent<Player>().weapon.SetActive(false);
                gameObject.GetComponent<LineOfSight>().alert.SetActive(false);
                //gameObject.GetComponent<LineOfSight>().enabled = false;
                gameObject.GetComponent<LineOfSight>().gun.GetComponent<Weapons>().enabled = false;
                //gameObject.GetComponent<LineOfSight>().enabled = false;
                player.GetComponent<MoveToTarget>().Targets.Remove(transform);
                addedinlist = false;*/
                //player.GetComponent<MoveToTarget>().enabled = false;
            }
        }
        else
        {
            MoveToTarget();
        }
       
    }

    public void MoveToTarget()
    {
        agent.SetDestination(player.position);

        float distanceToTarget = Vector3.Distance(transform.position, player.position);
        if(distanceToTarget > 15)
        {
            idleattack = false;
        }
        if(gothit == false)
        {
            if (distanceToTarget <= agent.stoppingDistance)
            {
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    RotateToTarget();
                }
                
                anim.SetTrigger("crawl");
                anim.SetBool("reached", true);
                anim.SetBool("eat", false);
                anim.SetBool("walk", false);
            }
            else
            {
                anim.SetBool("reached", false);
                anim.SetBool("eat", false);
                anim.SetBool("walk", false);
            }

        }
        else
        {
            anim.SetBool("reached", true);
            anim.SetBool("eat", false);
            anim.SetBool("walk", false);
            if (Vector3.Distance(transform.position, points[0].position) > 12)
            {
                anim.SetBool("turn", true);
                gothit = false;
            }
            else
            {
                anim.SetBool("turn", false);
            }
        }

    }

    private void RotateToTarget()
    {
        //transform.LookAt(target);

        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Player")
        //StartCoroutine(GoToEnemy(bite.position, bite.position, 1f));

        if(other.tag == "Bullet")
        {
            anim.SetTrigger("GetHit");
            gothit = true;
            
        }
    }

    public void HitPlayer()
    {
        //transform.parent = target;
        //agent.stoppingDistance = 0f;
        if(Vector3.Distance(transform.position, player.position) < 2.5f)
        player.GetComponent<Animator>().SetTrigger("GetHit");
        player.GetComponent<PlayerHealthManager>().health--;
        //StartCoroutine(GoToEnemy(bite.localPosition, bite.localPosition, 1f));
        //transform.position = new Vector3(0.00384f, -0.00443f, 0.01437f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(8, -163.8f, -3),1);
    }

    public void StopHitPlayer()
    {
        //target.GetComponent<Animator>().SetBool("GetHit", false);
        //agent.stoppingDistance = 2.5f;
        //transform.parent = null;
    }

    IEnumerator GoToEnemy(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {

            player.position = Vector3.Lerp(new Vector3(start.x, player.position.y, start.z), new Vector3(end.x, player.position.y, end.z), t / Duration);
            //target.rotation = Quaternion.Slerp(Quaternion.Euler(start.x, target.rotation.y, start.z), Quaternion.Euler(end.x, target.rotation.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);

                /*if (gameObject.name == "liam")
                {
                    print("corners are : " + path.corners.Length);
                }*/

                //anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }
}

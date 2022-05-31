using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class HumanRun : MonoBehaviour
{
    public GameObject[] goalLocations;
    NavMeshAgent agent;
    public Animator anim;
    private float sppedMult;
    private float detectionRadius = 25;
    private float fleeRadius = 15;
    public GameObject[] Enemy;

    void Start()
    {
        //goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();

        ResetAgent();
    }

    private void ResetAgent()
    {
        anim.SetFloat("Offset", Random.Range(0.0f, 1.0f));
        //anim.SetFloat("wOffset", Random.Range(0,1));
        //anim.SetTrigger("isWalking");
        float sppedMult = Random.Range(1, 3f);
        //anim.SetFloat("speedMult", sppedMult);
        agent.speed *= sppedMult;
        agent.angularSpeed = 120;
        agent.ResetPath();
        if (goalLocations.Length > 1)
        {
            anim.SetBool("walk", true);

        }
        else
        {
            anim.SetBool("walk", false);
            agent.speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

        }
        else
        {
            anim.SetBool("walk", true);
        }

        agent.speed = 0.1f;

        for (int i = 0; i < Enemy.Length; i++)
        {
            if (Enemy[i].activeInHierarchy)
            {
                anim.SetBool("run", true);
                agent.speed = 3f;
            }
            else
            {
                anim.SetBool("run", false);
                agent.speed = 1;
            }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(other.transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            var lookPos = transform.position - other.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
    }
}

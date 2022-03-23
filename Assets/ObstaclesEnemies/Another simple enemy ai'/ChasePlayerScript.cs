using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerScript : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float maxDistance = 6;
    public float minDistance = 3;
    public NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         transform.LookAt(player.transform.position);

         if (Vector3.Distance(transform.position, player.transform.position) >= minDistance)
        {
            
            _agent.isStopped = false;
            _agent.SetDestination(player.transform.position);
        }

         else if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            
            _agent.isStopped = true;
            Debug.Log("get attacked lmao gottem");
            //this is for projectile or whatever
        }
         else
        {
           
        }
    }
}

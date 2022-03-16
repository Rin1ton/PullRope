using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{

    public GameObject Player;

    public bool attackState;
    //true = enemy will start to attack player

    public float distance;
    //This will be used for enemy to check distance from player

    public NavMeshAgent _agent;
    //NavMeshAgent is component that helps characters from colliding with one another while moving towards goal

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);
        //the this refers to the enemy base class
    }

    // Update is called once per frame
    void Update()
    {
        if (distance <= 1f)
        {
            attackState = true;
            //if the enemy is 5 meters or closer, the enemy will enter attack state.
        }

        if (distance > 1f)
        {
            attackState = false;
        }

        if (attackState)
        {
            _agent.isStopped = false;
            _agent.SetDestination(Player.transform.position);
        }

        if (!attackState)
        {
            _agent.isStopped = true;
        }
    }
}

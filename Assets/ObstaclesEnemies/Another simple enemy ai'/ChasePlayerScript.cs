using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerScript : MonoBehaviour
{
    public GameObject player;
    float distance;
    float maxDistance = 6;
    float minDistance = 3;
    float MoveSpeed = 1;
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
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }

         if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            //this is for projectile or whatever
        }
    }
}

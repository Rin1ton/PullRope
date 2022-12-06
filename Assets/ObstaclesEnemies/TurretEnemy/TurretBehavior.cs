using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    GameObject player;
    public float time;
    public float distance;
    public float maxDistance;
    public float minDistance;
    public GameObject bullet;
    public GameObject bulletspawnpoint;
    bool time2Shoot;

    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        player = References.thePlayer;
        time2Shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Vector3.Distance(transform.position, player.transform.position) >= minDistance) && (Vector3.Distance(transform.position, player.transform.position) <= maxDistance))
        {
            transform.LookAt(player.transform.position);
            time2Shoot = true;  
        }
        else
        {
            time2Shoot = false;
        }

        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            if (time2Shoot)
            {
                Instantiate(bullet, bulletspawnpoint.transform.position, transform.rotation);
                if (shootSound != null)
                    AudioSource.PlayClipAtPoint(shootSound, transform.position, 1);
            }
        }
    }

}

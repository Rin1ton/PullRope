using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed;
    Vector3 goalpoint;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = References.thePlayer;
        goalpoint = player.transform.position;
        transform.LookAt(goalpoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

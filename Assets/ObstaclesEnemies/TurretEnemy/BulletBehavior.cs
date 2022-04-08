using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed;
    Vector3 goalpoint;
    GameObject player;
    Vector3 knockbackVelocity;
    public float forceMultiplier;
    public float lifespan;

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

        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        knockbackVelocity = new Vector3((transform.position.x - player.transform.position.x) * forceMultiplier, 0, (transform.position.z - player.transform.position.z) * forceMultiplier);
        player.GetComponent<Rigidbody>().velocity = -knockbackVelocity;
        Destroy(this.gameObject);
    }
}

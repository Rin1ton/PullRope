using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    public float forceMultiplier;
    GameObject player;
    Vector3 knockbackVelocity;
    public AudioClip bumpSound;

    private void OnCollisionEnter(Collision collision)
    {
        
        knockbackVelocity = new Vector3((transform.position.x - player.transform.position.x) * forceMultiplier, (transform.position.y - player.transform.position.y) * forceMultiplier, (transform.position.z - player.transform.position.z) * forceMultiplier);
        player.GetComponent<Rigidbody>().velocity = -knockbackVelocity;
        if (bumpSound != null)
            AudioSource.PlayClipAtPoint(bumpSound, player.transform.position, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = References.thePlayer;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

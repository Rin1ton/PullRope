using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinLogic : MonoBehaviour
{
    GameObject player;
    
    void Start()
    {
        player = References.thePlayer;
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

   
    void Update()
    {
        transform.Rotate(0, (float).45, 0);
    }
}

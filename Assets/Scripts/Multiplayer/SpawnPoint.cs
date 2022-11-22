using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameLogic.Singleton.SpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

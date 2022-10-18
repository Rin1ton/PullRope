using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SkinLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public Material skinToLoad;
    public GameObject Object;
    void Start()
    {
        Object.GetComponent<MeshRenderer>().material = skinToLoad;
    }
}

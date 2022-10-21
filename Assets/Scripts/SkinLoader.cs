using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SkinLoader : MonoBehaviour
{
    // Start is called before the first frame update

    private Material skinToLoad;
    [SerializeField] private Material skin1;
    [SerializeField] private Material skin2;
    [SerializeField] private Material skin3;
    [SerializeField] private Material skin4;
    [SerializeField] private Material skin5;
    [SerializeField] private Material skin6;
    [SerializeField] private Material skin7;
    [SerializeField] private Material skin8;
    [SerializeField] private GameObject Object;
    void Start()
    {
        Object.GetComponent<MeshRenderer>().material = skinToLoad;
    }
}

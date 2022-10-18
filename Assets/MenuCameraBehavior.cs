using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraBehavior : MonoBehaviour
{

    readonly float verticalSpin = 4;
    readonly float horizontalSpin = 14;
    readonly float vertEntropy = .039f;
    readonly float horEntropy = .057f;
    float timeSinceStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceStart += Random.value * 100;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;

        float thisHorSpin = horizontalSpin * Mathf.Sin(timeSinceStart * horEntropy);
        float thisVertSpin = verticalSpin * Mathf.Sin(timeSinceStart * vertEntropy);

        transform.Rotate(new Vector3(thisVertSpin, thisHorSpin, 0) * Time.deltaTime);
    }
}

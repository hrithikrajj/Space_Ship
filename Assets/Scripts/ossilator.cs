using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ossilator : MonoBehaviour
{
    Vector3 startingPosition ;
    [SerializeField] Vector3 movementVector;
    float movemenFactor = 0.1f;
    [SerializeField] float period = 1f;
    const float tau = Mathf.PI * 2;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon)
        {
            return;
        }
        float cycle = Time.time / period;
        float rawSinewave = Mathf.Sin(cycle * tau);
        movemenFactor = (rawSinewave + 1f) / 2f;
        transform.position = startingPosition + Vector3.up * movemenFactor * 3f;
        
    }
}

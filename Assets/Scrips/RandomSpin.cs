using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour
{
    public float time;
    public float min;
    public float randMax;
    public float speed;
    public float losFaktor;

    void Start()
    {
        StartSpin();
    }
    void Update()
    {
        if(speed >= 0)
        {
            speed -= Time.deltaTime * losFaktor;
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
    private void StartSpinByForce()
    {
        if (speed > 0) return;
        float _spinRand = Random.Range(0.0f, randMax);
        speed = min + _spinRand;
    }

    private void SpinConst()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    private void StartSpin()
    {
        if (time != 0) return;
        float _spinRand = Random.Range(0.0f, randMax);
        time = min + _spinRand; 
    }
}

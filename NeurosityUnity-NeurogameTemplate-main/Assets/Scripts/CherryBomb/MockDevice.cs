using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockDevice : MonoBehaviour
{
    public float value = 0f; // Current value
    public float incrementSpeed = 0.2f; // Speed at which the value increases

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        // Increment the value, and loop back to 0 when it exceeds 1
        value += incrementSpeed * Time.deltaTime;
        if (value > 1f)
        {
            value = 0f;
        }
        GetValue();
    }

    public float GetValue()
    {
        //Debug.Log(value);
        return value;
    }
}

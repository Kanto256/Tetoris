using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class moving : MonoBehaviour
{
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position + new Vector3(0,(float)Math.Abs(Math.Sin(Time.time * 3.14)),0);
    }
}

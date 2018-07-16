using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(new Vector3(0, 1 * speed, 0));
    }
}

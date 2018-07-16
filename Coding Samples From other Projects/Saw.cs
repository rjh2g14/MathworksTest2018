using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saw : MonoBehaviour {


    private Rigidbody saw;
    private int count;

    public float speed;
    public Text countText;

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    // Use this for initialization
    void Start ()
    {
        saw = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float movementVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(0, 0, movementVertical);

        saw.AddForce(-1 * movement * speed);
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

    public Vector3 originalPosition;
    LineRenderer rend;
    public float speed = 10f;
    Rigidbody rb;
    Vector3 eye;
    public bool slash = false;
    // Use this for initialization
    void Start () {
        rend = GetComponent<LineRenderer>();
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        eye = GameObject.FindGameObjectWithTag("eye").transform.forward;
    }

	// Update is called once per frame
	void Update () {
		if (slash == true)
        {
            eye = GameObject.FindGameObjectWithTag("eye").transform.forward;
            transform.Translate(eye *-1 * Time.deltaTime);
        }
   

	}

    public void returnBack ()
    {
        slash = false;
        //Return to the original position
        gameObject.transform.position = originalPosition;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //gameObject.SetActive(false);
    }
}

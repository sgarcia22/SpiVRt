using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballController : MonoBehaviour {

    Vector3 eye;

	// Use this for initialization
	void Start () {
    
        eye = GameObject.FindGameObjectWithTag("eye").transform.forward;
        
        StartCoroutine("DestroyObj");
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(eye * Time.deltaTime);
	}

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy") { 
           Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }
}

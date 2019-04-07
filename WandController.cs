using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WandController : MonoBehaviour {

    public GameObject orangeFire, blueFire, spawnEye, eye, tipObj, lineRend;
    public Material orangeFireMaterial, blueFireMaterial, orangeTrail, blueTrail;
    public TrailRenderer tip;
    public SteamVR_Action_Pose rightHand;
    public LineRenderer rend;
    public Slash slasher;
    public bool shoot = false;
    private float countdown = 0f;

    public bool orange, blue;
    public AudioSource shootSound;
    // Use this for initialization
    void Start () {
        rend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        countdown += Time.deltaTime;
        Vector3 eyeLocation = eye.transform.position;
        //Trigger Press for Wand
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && countdown >= 4f)
        {
            Debug.Log("Shoot");
            //Spawn Orange Projectile
            if (orangeFire.activeSelf == true)
            {
                Debug.Log("Entering");
                GameObject eyeOrange = Instantiate(spawnEye, eyeLocation, Quaternion.identity);
                eyeOrange.transform.localScale = new Vector3(.05f, .05f, .05f);
                eyeOrange.transform.GetChild(0).GetComponent<Renderer>().material = orangeFireMaterial;
                //  eyeOrange.transform.SetParent(gameObject.transform.parent.transform.parent.transform.parent.transform, false);
                eyeOrange.SetActive(true);
                shootSound.Play();
            }   
            //Spawn Blue Projectile
            else if (blueFire.activeSelf == true)
            {
                GameObject eyeBlue = Instantiate(spawnEye, eyeLocation, Quaternion.identity);
                eyeBlue.transform.localScale = new Vector3(.05f, .05f, .05f);
                eyeBlue.transform.GetChild(0).GetComponent<Renderer>().material = blueFireMaterial;
                //  eyeBlue.transform.SetParent(gameObject.transform.parent.transform.parent.transform.parent.transform, false);
                eyeBlue.SetActive(true);
                shootSound.Play();
            }

            countdown = 0f;
        }

        //Slash
        //Debug.Log(rightHand.GetLocalPosition(SteamVR_Input_Sources.RightHand));
        /*if (rightHand.velocity.magnitude > .3f && shoot == false)
        {
            //Tip - position 
            if (blueFire.activeSelf == true)
            {
                slasherFunction(tipObj.transform.position, rightHand.GetLocalPosition(SteamVR_Input_Sources.RightHand), true);
            }
            else if (orangeFire.activeSelf == true)
            {
                slasherFunction(tipObj.transform.position, rightHand.GetLocalPosition(SteamVR_Input_Sources.RightHand), false);
            }
            
        }
        */
    }

    private void slasherFunction (Vector3 tip, Vector3 position, bool blue)
    {
        shoot = true;
        slasher.slash = true;
        StartCoroutine("ShootProjectile");
        /*if (blue)
        { 
            rend.material = blueTrail;
        }
        else
            rend.material = orangeTrail;
        rend.startWidth = .05f;
        rend.endWidth = .05f;
        rend.positionCount = 3;
        //Start
        rend.SetPosition(0, tip);
        //Midpoint
        rend.SetPosition(1, (tip + position) / 2);
        //End
        rend.SetPosition(2, position);

        rend.enabled = true;
        */
    }

    IEnumerator ShootProjectile ()
    {
        lineRend.SetActive(true);
        yield return new WaitForSeconds(3f);
        slasher.returnBack();
        shoot = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Orange")
        {
            Debug.Log("Orange");
            blueFire.SetActive(false);
            orangeFire.SetActive(true);
            tip.material = orangeTrail;
            orange = true;
            blue = false;
        }
        if (collision.collider.tag == "Blue")
        {
            Debug.Log("Blue");
            orangeFire.SetActive(false);
            blueFire.SetActive(true);
            tip.material = blueTrail;
            blue = true;
            orange = false;
        }
    }

}

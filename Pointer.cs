using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=3mRI1hu9Y3w&t=196s
public class Pointer : MonoBehaviour {

    public float defaultLength = 5f;
    public GameObject dot;
    public VRInputModule inputModule;

    private LineRenderer lineRend = null;
    private bool pressing = false;
	// Use this for initialization
	void Awake () {
        lineRend = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	private void Update () {
        UpdateLine();
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            pressing = true;
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
            pressing = false;
    }

    private void UpdateLine()
    {
        //Default distance
        float targetLength = defaultLength;
        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);
        //Default
        Vector3 endPos = transform.position + (transform.forward * targetLength);
        //Or based on hit
        if (hit.collider != null)
            endPos = hit.point;
        if (hit.collider != null && hit.collider.tag == "Button" && pressing)
        {
            Debug.Log("Hitting");
            BossController.bossHealth = 100;
            BossController.playerHealth = 50;
            SceneManager.LoadScene(0);
        }
        //Set position of the dot
        dot.transform.position = endPos;
        //Set line renderer
        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, endPos);
    }

    private RaycastHit CreateRaycast (float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);
        return hit;

    }
}

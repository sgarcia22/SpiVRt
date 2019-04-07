using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;
//https://www.youtube.com/watch?v=h_BMXDWv10I&t=2s
public class VRInputModule : BaseInputModule {

    public Camera camera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObj = null;
    private PointerEventData data = null;

    protected override void Awake()
    {
        base.Awake();
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //Reset data, set camera
        data.Reset();
        data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);
        //Raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObj = data.pointerCurrentRaycast.gameObject;
        //Clear
        m_RaycastResultCache.Clear();
        //Hover
        HandlePointerExitAndEnter(data, currentObj);
        //Press
        //if (clickAction.GetStateDown(targetSource))
            //ProcessPress(data);
        //Release

    }

    public PointerEventData getData ()
    {
        return data;
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class jump : MonoBehaviour
{
    private bool isFalling,fast,moving;
    public float jumping;
    private ClickedEventArgs walk, run;
    private float posi, pressedposi;
    public SteamVR_TrackedController Lhand;
    public SteamVR_TrackedController Rhand;
    public SteamVR_TrackedObject cam;
    private VRControllerState_t controllerState;
    public bool onGround;
    public Image NRJbar;
    // Start is called before the first frame update
    void Start()
    {
        Lhand.PadClicked += onTrigclic;
        Rhand.PadTouched += onTrigTouch;
        Rhand.PadUntouched += offTrigTouch;
        Rhand.PadClicked += onTrigclicR;
        Rhand.PadUnclicked += offtrigclicR;
        moving = false;
        fast = false;
        isFalling = false;
        onGround = true;

        //Lhand.PadUnclicked += offTrigclic;
    }

    public void offtrigclicR(object sender, ClickedEventArgs e)
    {
        fast = false;
    }

    public void offTrigTouch(object sender, ClickedEventArgs e)
    {
        moving = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFalling)
        {
            var system = OpenVR.System;
            if (system != null && system.GetControllerState(Rhand.controllerIndex, ref controllerState, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(VRControllerState_t))))
            {
                if (moving)
                {

                    posi = controllerState.rAxis0.y;
                    Move();
                }

                if (fast)
                {
                    pressedposi = controllerState.rAxis0.y;
                    MoveFast();

                }
            }
                
        }
        if (Mathf.Abs(this.gameObject.GetComponent<Rigidbody>().velocity.y) < 0.0001f)
            isFalling = false;
    }

    public void onTrigclic(object sender, ClickedEventArgs e)
    {
        isFalling = false;
        Jump();
    }

    public void onTrigTouch(object sender, ClickedEventArgs e)
    {
        moving = true;
        fast = false;
        posi = e.padY;
    }

    public void onTrigclicR(object sender, ClickedEventArgs e)
    {
        fast = true;
        moving = false;
        pressedposi = e.padY;
    }

    public void Jump()
    {  
        if (isFalling == false)
        {
            if (NRJbar.rectTransform.offsetMax.x > -130 && onGround)
            {
                if (fast)
                    this.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0f, jumping, 0f);
                else if (moving)
                    this.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0f, jumping, 0f);
                else
                    this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, jumping, 0f);
                isFalling = true;

                //NRJbar.rectTransform.offsetMax -= new Vector2(20f, 0);
            }
        }

    }
    public void Move()
    {
            if(onGround)
                 this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(5f *posi *cam.gameObject.transform.forward.x,0f, 5f * posi * cam.gameObject.transform.forward.z);
            
    }
    public void MoveFast()
    {
        if (onGround)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(15f * pressedposi * cam.gameObject.transform.forward.x, 0f, 15f * pressedposi * cam.gameObject.transform.forward.z);
            NRJbar.rectTransform.offsetMax -= new Vector2(0.5f, 0);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "plane")
        {
            onGround = true;
        }

    }
    void OnCollisionExit(Collision collision)
    { 
        if (collision.gameObject.tag == "plane")
        {
            onGround = false;
        }
    }
}

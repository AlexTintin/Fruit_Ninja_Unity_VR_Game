using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hook : MonoBehaviour
{
    public LineRenderer lr;
    private GameObject moster;
    private Vector3 grapplepoint;
    public float distM = 10f;
    public SteamVR_TrackedController Rhand;
    private bool throwhook,connected,pressed;
    private RaycastHit hit;
    public GameObject coo,sword;
    private GameObject hookb;
    
    // Start is called before the first frame update
    void Start()
    {
        Rhand.TriggerClicked += onT;
        Rhand.TriggerUnclicked += offT;
        throwhook = false;
        connected = false;
        
    }

    public void onT(object sender, ClickedEventArgs e)
    {
        throwhook = true;
        pressed = true;
    }

    public void offT(object sender, ClickedEventArgs e)
    {
        pressed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if(hit.collider==null && connected)
         {
             throwhook = false;
             connected = false;
             lr.enabled =false;
         }*/
        var device = SteamVR_Controller.Input((int)Rhand.controllerIndex);
        if (pressed)
        {
            if (throwhook)
            {
                this.gameObject.transform.parent = null;
                transform.position += sword.transform.up * 15f * Time.smoothDeltaTime;
                Drawrope();
            }
            if (connected)
            {
                if(this.gameObject.transform.parent != Rhand.gameObject)
                    this.gameObject.transform.parent = Rhand.gameObject.transform;
                Drawrope();
                
            }
            if (moster == null && connected)
            {
                connected = false;
                lr.enabled = false;
                this.gameObject.transform.parent = Rhand.gameObject.transform;
                hookb = GameObject.Instantiate(this.gameObject, coo.transform) as GameObject;
                hookb.transform.parent = Rhand.gameObject.transform;
                GameObject.Destroy(this.gameObject);
            }

            if (Vector3.Distance(transform.position, Rhand.gameObject.transform.position) > distM)
            {
                throwhook = false;
                lr.enabled = false;
                this.gameObject.transform.parent = Rhand.gameObject.transform;
                hookb = GameObject.Instantiate(this.gameObject, coo.transform) as GameObject;
                hookb.transform.parent = Rhand.gameObject.transform;
                GameObject.Destroy(this.gameObject);

                //this.gameObject.transform.position = inittra.position;
                //this.gameObject.transform.rotation = inittra.rotation;
            }
        }
        else
        {
            if (connected)
            {
                moster.GetComponent<Rigidbody>().velocity = Vector3.Distance(moster.transform.position,Rhand.transform.position) *device.velocity;
                moster.transform.parent = null;
                moster.gameObject.GetComponent<Rigidbody>().useGravity = true;
                
            }
            throwhook = false;
            connected = false;
            lr.enabled = false;
            this.gameObject.transform.parent = Rhand.gameObject.transform;
            hookb = GameObject.Instantiate(this.gameObject, coo.transform) as GameObject;
            hookb.transform.parent = Rhand.gameObject.transform;
            GameObject.Destroy(moster,10f);
            GameObject.Destroy(this.gameObject);

        }
       // if(hit.collider.gameObject.tag == "minimonster" && !connected && !throwhook)
       // {
            //hit.collider.gameObject.tag == "minimonster";
     }

        
    
    

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        //Debug.Log("before collided");
        if (other.tag == "minimonster")
        {
            connected = true;
            throwhook = false;
            other.gameObject.transform.parent = Rhand.gameObject.transform;
            other.gameObject.tag = "weapon";
            other.gameObject.GetComponent<run_to_player>().enabled = false;
            other.gameObject.GetComponent<weaponmoster>().enabled = true;
            other.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            moster = other.gameObject;
        }
        

    }

    public void Drawrope()
    {
        lr.enabled = true;
        lr.SetPosition(0, this.gameObject.transform.position);
        lr.SetPosition(1, Rhand.gameObject.transform.position);
    }
}

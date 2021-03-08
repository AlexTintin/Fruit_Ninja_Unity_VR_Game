using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class throwninja : MonoBehaviour
{
    public SteamVR_TrackedController hand;
    private List<Vector3> data;
    private bool record, hit, init, pressedonce, initiated;
    public float thresholdmovement = 0.05f;
    private Vector3 vecinterest;
    public GameObject explosion,ninjax2;

    private int comp;
    // Start is called before the first frame update
    void Start()
    {
        hand.TriggerClicked += onTrigclic;
        hand.TriggerUnclicked += offTrigclic;
        record = false;
        vecinterest = Vector3.zero;
        comp = 0;
        init = true;
        pressedonce = false;
        initiated = false;
    }

    public void onTrigclic(object sender, ClickedEventArgs e)
    {
        record = true;
    }

    public void offTrigclic(object sender, ClickedEventArgs e)
    {
        record = false;
    }
    void Initmov()
    {
        data = new List<Vector3>();
        data.Add(hand.transform.position);
        initiated = true;
    }

    void UpdateMov()
    {
        Vector3 currentpos = hand.transform.position;
        Vector3 lastpos = data[data.Count - 1];
        if (Vector3.Distance(currentpos, lastpos) > thresholdmovement)
        {
            data.Add(hand.transform.position);
        }
    }

    void EndMov()
    {

       if (data.Count > 4)
        {
            vecinterest = Vector3.Normalize(data[data.Count - 1] - data[0]);
            data = new List<Vector3>();
            init = false;
        }
        else
        {
            pressedonce = false;
            Debug.Log("NO forcemovement saved");
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (record && !initiated && init)
            Initmov();

        if (record && initiated &&  init)
        {
            pressedonce = true;
            UpdateMov();
        }
        if (!record && pressedonce  && init)
            EndMov();
 
        if (!hit && !init)
        {
            if(this.gameObject.transform.parent != null)
                this.gameObject.transform.parent = null;
            this.gameObject.transform.position += vecinterest * 10.0f * Time.smoothDeltaTime;
            this.gameObject.transform.Rotate(new Vector3(0, 10, 0) * 100.0f * Time.smoothDeltaTime);
            comp++;
        }
        if (comp > 1000 || hit)
        {
            if(ninjax2 !=null)
                ninjax2.SetActive(true);
            if(!hit)
                GameObject.Destroy(this.gameObject);

        }


        

    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.name);
        //Debug.Log("before collided");
        if (other.tag == "minimonster")
        {
            //other.gameObject.SetActive(false);
            GameObject.Destroy(GameObject.Instantiate(explosion, other.gameObject.transform.position, other.gameObject.transform.rotation), 3);
            GameObject.Destroy(other.gameObject);
            //ViveInput.TriggerHapticPulse(HandRole.RightHand, intensity);
            //if (ninjax2 != null)
            //    ninjax2.SetActive(true);
            //GameObject.Destroy(this.gameObject);

        }
        if (other.gameObject.tag == "bigmonster")
        {
            hit = true;
            this.gameObject.transform.parent = other.gameObject.transform;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fly : MonoBehaviour
{
    public SteamVR_TrackedController Lhand;
    public SteamVR_TrackedController Rhand;
    public SteamVR_TrackedObject cam;
    public Image NRJbar;
    public float frottement, coefforwad;
    private bool flyL, flyR, forceadded, first;

    // Start is called before the first frame update
    void Start()
    {
        Lhand.Gripped += onGripL;
        Lhand.Ungripped += offGripL;
        Rhand.Gripped += onGripR;
        Rhand.Ungripped += offGripR;
        flyL = false;
        flyR = false;
        forceadded = false;
        first = true;
    }

    public void onGripL(object sender, ClickedEventArgs e)
    {
        flyL = true;
    }

    public void onGripR(object sender, ClickedEventArgs e)
    {
        flyR = true;
    }

    public void offGripL(object sender, ClickedEventArgs e)
    {
        flyL = false;
        first = true;
    }

    public void offGripR(object sender, ClickedEventArgs e)
    {
        flyR = false;
        first = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (flyR && flyL)
        {
            //if (first)
            //{
             //   this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
             //   first = false;
           // }
            DepleteNRJ();
            if (!forceadded)
            {
                
                Flying();
                Turning();
            }
            
        }
        //if ((!flyR || !flyL) && forceadded)
        //{
        //    StopFlying();
//}
    }
    public void Turning()
    {
        Vector3 pL = Lhand.gameObject.transform.position;
        Vector3 pR = Rhand.gameObject.transform.position;
        Vector3 line1 = pR - pL;
        Vector3 line2 = cam.gameObject.transform.forward;
        Vector3 line3 = cam.gameObject.transform.position - pL;
        Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line3));
        //if(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude>1f)
        this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude*Vector3.Normalize(line2-line1.y*(new Vector3(line1.x,0f,line1.z)));
            //this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude * normal.y*line2;
    }

    public void Flying()
    {

        Vector3 pL = Lhand.gameObject.transform.position;
        Vector3 pR = Rhand.gameObject.transform.position;
        Vector3 pCam = cam.gameObject.transform.position;
        Vector3 line1 = pR - pL;
        Vector3 line2 = pCam - pL;
        Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line2));
        float area = Vector3.Distance(pL, pR);

        float vel = Mathf.Max(Mathf.Abs(Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up)), 0.001f);
        if (Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up) < 0)
            this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up*area * frottement * vel);
        this.gameObject.GetComponent<Rigidbody>().AddForce(coefforwad * normal.y * cam.gameObject.transform.forward * Mathf.Max(Mathf.Abs((this.gameObject.GetComponent<Rigidbody>().velocity-Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up)* this.gameObject.transform.up).magnitude-15f), 0.00001f));
    }
    public void DepleteNRJ()
    {
        if (NRJbar.rectTransform.offsetMax.x > -139)
                forceadded = false;
        NRJbar.rectTransform.offsetMax -= new Vector2(0.5f, 0);
        if (NRJbar.rectTransform.offsetMax.x <= -140)
            forceadded = true;

    }
        public void StopFlying()
    {
         Vector3 pL = Lhand.gameObject.transform.position;
         Vector3 pR = Rhand.gameObject.transform.position;
         Vector3 pCam = cam.gameObject.transform.position;
         Vector3 line1 = pR - pL;
         Vector3 line2 = pCam - pL;
         Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line2));
         float area = Vector3.Distance(pL, pR) * 2f;

         float vel = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
         this.gameObject.GetComponent<Rigidbody>().AddForce(-this.gameObject.transform.up * area * frottement);
         Debug.Log(area * frottement * vel * vel);
       // forceadded = false;
        //this.gameObject.GetComponent<Rigidbody>().mass = 80f;
        // forceadded = false;



    }
    /*
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    public SteamVR_TrackedController Lhand;
    public SteamVR_TrackedController Rhand;
    public SteamVR_TrackedObject cam;
    public float frottement, coefforwad;
    private bool flyL, flyR, forceadded;

    // Start is called before the first frame update
    void Start()
    {
        Lhand.Gripped += onGripL;
        Lhand.Ungripped += offGripL;
        Rhand.Gripped += onGripR;
        Rhand.Ungripped += offGripR;
        flyL = false;
        flyR = false;
        forceadded = false;
    }

    public void onGripL(object sender, ClickedEventArgs e)
    {
        flyL = true;
    }

    public void onGripR(object sender, ClickedEventArgs e)
    {
        flyR = true;
    }

    public void offGripL(object sender, ClickedEventArgs e)
    {
        flyL = false;
    }

    public void offGripR(object sender, ClickedEventArgs e)
    {
        flyR = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (flyR && flyL && !forceadded)
        {
            Flying();
            Turning();
        }
        //if ((!flyR || !flyL) && forceadded)
        //{
        //    StopFlying();
        //}
    }
    public void Turning()
    {
        Vector3 pL = Lhand.gameObject.transform.position;
        Vector3 pR = Rhand.gameObject.transform.position;
        Vector3 line1 = pR - pL;
        Vector3 line2 = cam.transform.forward;
        Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line2));
        Vector3 velsansy = new Vector3(this.gameObject.GetComponent<Rigidbody>().velocity.x, 0f, this.gameObject.GetComponent<Rigidbody>().velocity.z);
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3((velsansy.magnitude * normal.y * Vector3.Normalize(line2 - line1.y * (new Vector3(line1.x, 0f, 0f)))).x, this.gameObject.GetComponent<Rigidbody>().velocity.y, normal.y * (velsansy.magnitude * Vector3.Normalize(line2 - line1.y * (new Vector3(line1.x, 0f, 0f)))).z);
        //this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3((velsansy.magnitude * Vector3.Normalize(line2)* normal.y).x, this.gameObject.GetComponent<Rigidbody>().velocity.y, (velsansy.magnitude * Vector3.Normalize(line2) * normal.y).z);
    }

    public void Flying()
    {

        Vector3 pL = Lhand.gameObject.transform.position;
        Vector3 pR = Rhand.gameObject.transform.position;
        Vector3 pCam = cam.gameObject.transform.position;
        Vector3 line1 = pR - pL;
        Vector3 line2 = pCam - pL;
        Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line2));
        float area = Vector3.Distance(pL, pR);

        float vel = Mathf.Max(Mathf.Abs(Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up)), 0.001f);
        if (Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up) < 0)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.up * area * 0.5f * frottement * vel);
        }
        this.gameObject.GetComponent<Rigidbody>().AddForce(coefforwad * normal.y * cam.gameObject.transform.forward * Mathf.Max(Mathf.Abs((this.gameObject.GetComponent<Rigidbody>().velocity - Vector3.Dot(this.gameObject.GetComponent<Rigidbody>().velocity, this.gameObject.transform.up) * this.gameObject.transform.up).magnitude - 15f), 0.00001f));

    }
    public void StopFlying()
    {
        Vector3 pL = Lhand.gameObject.transform.position;
        Vector3 pR = Rhand.gameObject.transform.position;
        Vector3 pCam = cam.gameObject.transform.position;
        Vector3 line1 = pR - pL;
        Vector3 line2 = pCam - pL;
        Vector3 normal = Vector3.Normalize(Vector3.Cross(line1, line2));
        float area = Vector3.Distance(pL, pR) * 2f;

        float vel = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        this.gameObject.GetComponent<Rigidbody>().AddForce(-this.gameObject.transform.up * area * frottement);
        Debug.Log(area * frottement * vel * vel);
        // forceadded = false;
        //this.gameObject.GetComponent<Rigidbody>().mass = 80f;
        // forceadded = false;



    }
}
*/
}

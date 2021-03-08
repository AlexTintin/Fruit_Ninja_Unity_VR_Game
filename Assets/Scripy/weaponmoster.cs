using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponmoster : MonoBehaviour
{
    public SteamVR_TrackedObject sabercontrol;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.tag == "minimonster")
        {
            //other.gameObject.SetActive(false);
            GameObject.Destroy(GameObject.Instantiate(explosion, other.gameObject.transform.position, other.gameObject.transform.rotation), 3);
            GameObject.Destroy(other.gameObject);
            SteamVR_Controller.Input((int)sabercontrol.index).TriggerHapticPulse(5000);
            GameObject.Destroy(GameObject.Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            GameObject.Destroy(this.gameObject);

        }

        if (other.tag == "bigmonster")
        {
            SteamVR_Controller.Input((int)sabercontrol.index).TriggerHapticPulse(5000);
            GameObject.Destroy(GameObject.Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            GameObject.Destroy(this.gameObject);

        }


    }
}

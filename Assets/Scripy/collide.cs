using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collide : MonoBehaviour
{
    public GameObject energyeffect, healtheffect;
    public Image lifebar,NRJbar;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.tag == "food")
        {

            other.gameObject.SetActive(false);
            GameObject.Destroy(GameObject.Instantiate(energyeffect, this.gameObject.transform.position, this.gameObject.transform.rotation),3);
            NRJbar.rectTransform.offsetMax += new Vector2(20f, 0);

        }
        if (other.tag == "potion")
        {

            other.gameObject.SetActive(false);
            GameObject.Destroy(GameObject.Instantiate(healtheffect, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            GameObject.Destroy(GameObject.Instantiate(healtheffect, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            GameObject.Destroy(GameObject.Instantiate(healtheffect, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            GameObject.Destroy(GameObject.Instantiate(healtheffect, this.gameObject.transform.position, this.gameObject.transform.rotation), 3);
            lifebar.rectTransform.offsetMax += new Vector2(20f, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

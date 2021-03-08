using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameplay : MonoBehaviour
{
    public GameObject cam;
    public Image lifebar;
    public GameObject panelover,startga;
    private int comp;
    // Start is called before the first frame update
    void Start()
    {
        comp = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        comp++;
        if(comp>900 && startga.activeSelf)
        {
            startga.SetActive(false);
        }
        if(lifebar.rectTransform.offsetMax.x <-150)
        {
            Debug.Log("die");
            Gameover();
        }
        if (cam.transform.position.y< -80f)
        {
            //Debug.Log(Vector3.Dot(cam.transform.up, cam.transform.position));
            Debug.Log(cam.transform.position.y);
            Gameover();
        }
    }
    void Gameover()
    {
        cam.transform.position = Vector3.zero;
        panelover.SetActive(true);
        cam.GetComponent<jump>().enabled = false;
        cam.GetComponent<fly>().enabled = false;
    }
}

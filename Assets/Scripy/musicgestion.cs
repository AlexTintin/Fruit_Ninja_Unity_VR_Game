using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicgestion : MonoBehaviour
{
    public AudioClip normal, kiwi,banana;
    public GameObject kiwiG;// bananaG;
    private bool donekiwi, donenormal;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().clip = normal ;
        this.gameObject.GetComponent<AudioSource>().Play();
        donekiwi = false;
        donenormal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (kiwiG != null )
        {
            if (Vector3.Distance(this.gameObject.transform.position, kiwiG.transform.position) < 50f)
            {
                if (!donekiwi)
                {
                    this.gameObject.GetComponent<AudioSource>().clip = kiwi;
                    this.gameObject.GetComponent<AudioSource>().Play();
                    donekiwi = true;
                    donenormal = false;
                }
            }

            else
            {
                if (!donenormal)
                {
                    this.gameObject.GetComponent<AudioSource>().clip = normal;
                    this.gameObject.GetComponent<AudioSource>().Play();
                    donekiwi = false;
                    donenormal = true;
                }
            }

        }
        else
        {
            if (!donenormal)
            {
                this.gameObject.GetComponent<AudioSource>().clip = normal;
                this.gameObject.GetComponent<AudioSource>().Play();
                donekiwi = false;
                donenormal = true;
            }
        }
    }
}

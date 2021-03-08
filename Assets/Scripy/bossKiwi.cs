using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossKiwi : MonoBehaviour
{
     public GameObject Cam;
     public Image lifebar, lifebarplayer;
     public GameObject Maxiexplosion,superattack;
     public GameObject hit;
     public Vector2 lifemin = new Vector2(-140, 0);
     public AudioClip explose,death;
     public AudioClip hurt;
     private Vector3 tra;
     

    // Start is called before the first frame update
    public GameObject minimonster;
    private GameObject clone, attack;
    public Transform starting_points1, starting_points2, starting_points3;
    private List<GameObject> robotClone;
    private int counteur, counteursuper, k, rand;
    private float count;
    private bool destroyed, superattacked;
    Transform startpos;

    // Start is called before the first frame update
    void Start()
    {
        counteur = 0;
        counteursuper = 0;
        robotClone = new List<GameObject>();
        destroyed = false;
        superattacked = false;
        k = 0;
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.gameObject.transform.position, Cam.transform.position) < 50f)
        {
            counteursuper++;
            counteur++;
            if (counteur > 100)
            {
                rand = Random.Range(0, 3);
                startpos = (rand < 1 ? starting_points3 : (rand < 2 ? starting_points1 : starting_points2));
                clone = GameObject.Instantiate(minimonster, startpos.position, startpos.rotation) as GameObject;
                clone.transform.parent = this.gameObject.transform;
                clone.SetActive(true);
                robotClone.Add(clone);
                counteur = 0;
            }
            if (counteursuper == 900)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("walk");
                
            }
            if (counteursuper > 1000)
            {
                if (Vector3.Distance(this.gameObject.transform.position, Cam.transform.position) < 13f && !superattacked)
                {
                    superattacked = true;
                    attack = GameObject.Instantiate(superattack, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
                    attack.transform.LookAt(Cam.transform);
                    attack.SetActive(true);
                    GameObject.Destroy(attack, 2f);
                    this.gameObject.GetComponent<Animator>().SetTrigger("idle");
                    tra = Cam.transform.position;
                    count = counteursuper;
                }
                if (superattacked)
                {
                    if (counteursuper > count + 200)
                    {
                        Debug.Log(tra);
                        Debug.Log(Cam.transform.position);
                        
                        if (Vector3.Distance(tra, Cam.transform.position) < 1f)
                        {
                            lifebarplayer.rectTransform.offsetMax -= new Vector2(50, 0);
                        }
                        superattacked = false;
                        counteursuper = 0;

                    }
                }

            }
            
            

            /* if (counteursuper == 850)
             {
                 this.gameObject.GetComponent<Animator>().SetTrigger("walk");
                 tra = Cam.transform;
             }
             if (counteursuper > 1000)
             {

                 //GameObject.Destroy(GameObject.Instantiate(superattack, new Vector3(Cam.transform.position.x, this.gameObject.transform.position.y, Cam.transform.position.x), Quaternion.identity), 3);
                 attack = GameObject.Instantiate(superattack, tra.position, tra.rotation) as GameObject;
                 attack.transform.localScale = new Vector3(2f,2f,2f);
                 attack.SetActive(true);
                 if(Vector3.Distance(tra.position, Cam.transform.position)<3f)
                 {
                     lifebarplayer.rectTransform.offsetMax -= new Vector2(50, 0);
                 }
                 //attack.transform.parent = Cam.transform;
                 //attack.transform.position = Vector3.zero;
                 GameObject.Destroy(attack, 3f);
                 counteursuper = 0;
                 this.gameObject.GetComponent<Animator>().SetTrigger("idle");
             }*/
        }
        if (destroyed)
        {
            k++;
        }
        if(k>200)
        {
            this.gameObject.GetComponent<AudioSource>().clip = explose;
            this.gameObject.GetComponent<AudioSource>().Play();
            GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
            GameObject.Destroy(this.gameObject);
            destroyed = false;
            k = 0;
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("before collided");
        if (other.tag == "weapon")
        {
            //Debug.Log(other.name);
            if (lifebar.rectTransform.offsetMax.x < lifemin.x)
            {
                this.gameObject.GetComponent<AudioSource>().clip = death;
                this.gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.GetComponent<Animator>().SetTrigger("death");
                destroyed = true;

            }
            else
            {
                lifebar.rectTransform.offsetMax -= new Vector2(20, 0);
                //GameObject.Destroy(GameObject.Instantiate(hit, other.gameObject.transform.position, other.gameObject.transform.rotation), 5);
                this.gameObject.GetComponent<AudioSource>().clip = hurt;
                this.gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.GetComponent<Animator>().SetTrigger("damaged");
            }


        }

        /*
        else
        {
            if (lifebar.rectTransform.offsetMax.x < lifemin.x)
            {
                this.gameObject.GetComponent<AudioSource>().clip = explose;
                this.gameObject.GetComponent<AudioSource>().Play();
                GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
                GameObject.Destroy(this.gameObject.transform.parent.gameObject);
            }
            else
            {
                lifebar.rectTransform.offsetMax -= new Vector2(1, 0);
                GameObject.Destroy(GameObject.Instantiate(hit, other.gameObject.transform.position, other.gameObject.transform.rotation), 5);
                this.gameObject.GetComponent<AudioSource>().clip = hurt;
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        */




        // }
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "weapon")
        {
            //Check for a match with the specified name on any GameObject that collides with your GameObject
            if (lifebar.rectTransform.offsetMax.x < lifemin.x)
            {
                this.gameObject.GetComponent<AudioSource>().clip = death;
                this.gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.GetComponent<Animator>().SetTrigger("death");
                destroyed = true;

            }
            else
            {
                lifebar.rectTransform.offsetMax -= new Vector2(20, 0);
                //GameObject.Destroy(GameObject.Instantiate(hit, other.gameObject.transform.position, other.gameObject.transform.rotation), 5);
                this.gameObject.GetComponent<AudioSource>().clip = hurt;
                this.gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.GetComponent<Animator>().SetTrigger("damaged");
            }
        }

    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bosslang : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cam,rig;
    private int counteur, counteursuper, k, rand;
    private Vector3 posss;
    private bool destroyed, superattacked;
    public Vector2 lifemin = new Vector2(-140, 0);
    public Image lifebar, lifebarplayer;
    public AudioClip explose, death;
    public AudioClip hurt;
    public GameObject Maxiexplosion, superattack;
    public GameObject hit;
    private GameObject clone1, clone2, attack;
    public GameObject bullets1, bullets2;
    /*

    

    private Vector3 tra;


    // Start is called before the first frame update
    public GameObject minimonster;
    private GameObject clone, attack;
    public Transform starting_points1, starting_points2, starting_points3;
    private List<GameObject> robotClone;
    
    private float count;
    
    Transform startpos;*/

    // Start is called before the first frame update
    void Start()
    {
        counteur = 0;
        counteursuper = 0;
        destroyed = false;
        /* robotClone = new List<GameObject>();
        
        superattacked = false;
        k = 0;
        count = 0;*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.gameObject.transform.position, rig.transform.position) < 50f)
        {
            //posss = rig.transform.position + (Cam.transform.position - rig.transform.position) - Vector3.Dot(rig.transform.up, Cam.transform.position - rig.transform.position) * rig.transform.up;
            this.gameObject.transform.LookAt(Cam.transform);
            this.gameObject.transform.Rotate(new Vector3(0f, 90f, 0f));
            counteur++;
            if (counteur > 300)
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("attack");
                clone1 = GameObject.Instantiate(bullets1, bullets1.transform.position, bullets1.transform.rotation) as GameObject;
                clone2 = GameObject.Instantiate(bullets2, bullets2.transform.position, bullets2.transform.rotation) as GameObject;
                clone1.transform.parent = this.gameObject.transform;
                clone2.transform.parent = this.gameObject.transform;
                //clone1.SetActive(true);
                counteur = 0;
            }
        }
        
            /*if (counteursuper == 900)
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




        }
        if (destroyed)
        {
            k++;
        }
        if (k > 200)
        {
            this.gameObject.GetComponent<AudioSource>().clip = explose;
            this.gameObject.GetComponent<AudioSource>().Play();
            GameObject.Destroy(GameObject.Instantiate(Maxiexplosion, this.gameObject.transform.position, this.gameObject.transform.rotation), 5);
            GameObject.Destroy(this.gameObject);
            destroyed = false;
            k = 0;
        }

    */
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



    }



   // }


}

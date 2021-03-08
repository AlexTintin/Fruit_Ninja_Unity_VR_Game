using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class run_to_player : MonoBehaviour
{
    public GameObject Cam,rig;
    public float speed;
    public Animator animator;
    private bool attacking;
    private int comp;
    public Image lifebar;
    private Vector3 posss;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(rig.transform);
        animator.SetTrigger("walk");
        attacking = false;
        comp = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        comp++;
        posss = rig.transform.position + (Cam.transform.position - rig.transform.position) - Vector3.Dot(rig.transform.up, Cam.transform.position - rig.transform.position) * rig.transform.up;
        if (Vector3.Distance(transform.position, posss) < 50f && Vector3.Distance(transform.position, posss) > 0.8f)
        {

            //animator.SetTrigger("walk");
            transform.LookAt(posss);
            transform.position += transform.forward * speed * Time.smoothDeltaTime;
        }
        if (Vector3.Distance(transform.position, posss) < .9f && !attacking)
        {
                animator.SetTrigger("attack");
                attacking = true;
                comp = 0;
                lifebar.rectTransform.offsetMax -= new Vector2(10f, 0);
            //animator.SetTrigger("walf");
        }
        if(comp>100)
         {
                attacking = false;
        }
        
    }
}

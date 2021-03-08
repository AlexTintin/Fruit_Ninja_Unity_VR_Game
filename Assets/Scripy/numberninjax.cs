using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberninjax : MonoBehaviour
{
    public GameObject hand;
    private int start;
    // Start is called before the first frame update
    void Start()
    {
        start = 6;

    }

    // Update is called once per frame
    void Update()
    {
        if (start != hand.transform.childCount - 2)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + (hand.transform.childCount - 2).ToString();
            start = hand.transform.childCount - 2;
            
        }
    }
}

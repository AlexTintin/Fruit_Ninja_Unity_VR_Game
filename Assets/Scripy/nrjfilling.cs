using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nrjfilling : MonoBehaviour
{
    public Image NRJbar;
    private int max;
    // Start is called before the first frame update
    void Start()
    {
        max = -5;
    }

    // Update is called once per frame
    void Update()
    {
        if (NRJbar.rectTransform.offsetMax.x < max)
             NRJbar.rectTransform.offsetMax += new Vector2(0.25f, 0);
    }
}

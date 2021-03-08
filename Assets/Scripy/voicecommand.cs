using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows.Speech;

public class voicecommand : MonoBehaviour
{
    private string dev;
    private KeywordRecognizer reco;
    private List<string> act = new List<string>();
    private bool recognized;
    private int compteur;
    // Start is called before the first frame update
    void Start()
    {
        act.Add("ninja");
        recognized = false;
        //dev = Microphone.devices[0].ToString();
        //this.gameObject.GetComponent<AudioSource>().clip = Microphone.Start(dev, true, 10, AudioSettings.outputSampleRate);
        reco = new KeywordRecognizer(act.ToArray());
        reco.OnPhraseRecognized += Todowhenrecognize;
        reco.Start();
        compteur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(recognized)
        {
            compteur++;
        }
        if(compteur>1000)
        {
            recognized = false;
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
                if (this.gameObject.transform.GetChild(i).gameObject.tag == "minimonster")
                {
                    this.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetTrigger("walk");
                    this.gameObject.transform.GetChild(i).gameObject.GetComponent<run_to_player>().enabled = true;
                }
            compteur = 0;
        }
    }

    private void Todowhenrecognize(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        recognized = true;
        for(int i=0; i<this.gameObject.transform.childCount;i++)
            if (this.gameObject.transform.GetChild(i).gameObject.tag=="minimonster")
            {
                this.gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>().SetTrigger("death");
                this.gameObject.transform.GetChild(i).gameObject.GetComponent<run_to_player>().enabled = false;
            }
    }

}

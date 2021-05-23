using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToChange;
    [SerializeField] GameObject trigger1;
    [SerializeField] GameObject trigger2;
    [SerializeField] GameObject trigger3;
    [SerializeField] GameObject trigger4;
    // Start is called before the first frame update
    void Start()
    {
        textToChange.text = "Greetings! My name is H.A.L.P.E.R, your automated ranger guide here to halp you document your findings on this desert safari.";
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator changeText(string changeableText, int secondsWaited)
    {
        textToChange.text = changeableText;
        yield return new WaitForSeconds(secondsWaited);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public Button manualButton;
    [SerializeField] public Button automaticButton;
    [SerializeField] bool halpCheck;
    [SerializeField] bool firstScreen;
    [SerializeField] TextMeshProUGUI textToChange;
    [SerializeField] TextMeshProUGUI textDialogue;
    [SerializeField] TextMeshProUGUI autoText;
    [SerializeField] TextMeshProUGUI manText;
    [SerializeField] bool dialogueEnd = false;
    private GameSession gameSession;
    bool selectCheck = false;
    int dialogueCounter = 0;

    [SerializeField] string[] dialogueDict;
    [SerializeField] string textstyle;
    int i = 0;
    // Start is called before the first frame update
    //void Start()
    //{
    //    autoText.GetComponent<TextMeshProUGUI>().color = Color.clear;
    //    manText.GetComponent<TextMeshProUGUI>().color = Color.clear;
    //    gameSession = FindObjectOfType<GameSession>();
    //    Button btn = button.GetComponent<Button>();
    //    btn.onClick.AddListener(LoadScene);
       
    //    if (firstScreen == false)
    //    {
    //       // StartCoroutine(HalpTalk());
    //    }
    //}


    // Update is called once per frame
    //void Update()
    //{
    //    if (halpCheck)
    //    {
    //        textToChange.GetComponent<TextMeshProUGUI>().color = Color.clear;
    //    }
    //    else if (!halpCheck)
    //    {
    //        textToChange.GetComponent<TextMeshProUGUI>().color = Color.white;
    //    }
        
    //    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        textDialogue.text = dialogueDict[dialogueCounter];
    //        if (dialogueCounter == dialogueDict.Length - 1)
    //        {
    //            dialogueEnd = true;
    //        }
    //        else
    //        {
    //            dialogueCounter++;
    //        }
    //    }
    //    if (dialogueEnd)
    //    {
    //        autoText.GetComponent<TextMeshProUGUI>().color = Color.white;
    //        manText.GetComponent<TextMeshProUGUI>().color = Color.white;
    //        Button manbtn = manualButton.GetComponent<Button>();
    //        Button autobtn = automaticButton.GetComponent<Button>();
    //        manbtn.onClick.AddListener(setStyleMan);
    //        autobtn.onClick.AddListener(setStyleAuto);

    //        if (selectCheck == true)
    //        {
    //            dialogueEnd = false;
    //            Debug.Log(textstyle);
    //            textDialogue.text = "Great! And now, on to the trip.";
    //            halpCheck = false;
    //        }
    //    }
    //}

    public void setStyleAuto()
    {
        textstyle = "automatic";
        autoText.GetComponent<TextMeshProUGUI>().color = Color.clear;
        manText.GetComponent<TextMeshProUGUI>().color = Color.clear;
        gameSession.SetGameState(GameState.Automatic);
        selectCheck = true;
    }

    public string getStyle()
    {
        return textstyle;
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Science_Project");
    }

    public void setStyleMan()
    {
        textstyle = "manual";
        autoText.GetComponent<TextMeshProUGUI>().color = Color.clear;
        manText.GetComponent<TextMeshProUGUI>().color = Color.clear;
        gameSession.SetGameState(GameState.Manual);
        selectCheck = true;
    }

    public void LoadScene()
    {
        if (halpCheck == false && firstScreen == false)
        {
            SceneManager.LoadScene("Science_Project");
        }
        if (firstScreen == true)
        {
            SceneManager.LoadScene("Science_Project");
        }
    }

    IEnumerator FadeItIn()
    {
        yield return null;
        GetComponent<TextMeshPro>().color = Color.clear;
    }

    IEnumerator HalpTalk()
    {
        for (i = 0; i < dialogueDict.Length; i += 1)
        {
            i = dialogueCounter;
            textDialogue.text = dialogueDict[dialogueCounter];
            yield return new WaitForSeconds(5);
            dialogueCounter += 1;
        }
        dialogueEnd = true;
        //textDialogue.text = "Hello! I am H.A.L.P.E.R, your automated guide to halp you document your findings on this research trip!";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "You're here to record data on the organisms of this desert. ";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "Remember to keep your arms off the local animals and plants, as they may be deadly.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "If you do end up dying, you'll just plop back up at the start of the desert.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "Lastly, you can change your dialogue settings - you can have them be manual or automatic.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "Manual means that whenever my new dialogue pops up, your game will freeze until you press A to progress the dialogue.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "Automatic means that I'll automatically progress the dialogue whenever any new dialogue pops up.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "This means that you'll have to pay attention to that and the game simultaneously.";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "Make your choice wisely, as it can't be changed!";
        //yield return new WaitForSeconds(6);
        //textDialogue.text = "We recommend automatic, but manual works, too! Press the manual button for manual and automatic button for automatic.";
        //yield return new WaitForSeconds(6);
        //dialogueEnd = true;
        //Debug.Log(Input.getkey);
        //if (textstyle == "manual" || textstyle == "automatic")
        //{
        //    textDialogue.text = "Great! And now, on to the trip.";
        //    halpCheck = false;
        //}
    }
}

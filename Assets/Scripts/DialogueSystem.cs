using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public ELEMENTS elements;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Start function WaitAndPrint as a coroutine.

        //coroutine = Speaking("WEEEEEE.", "MC");
        //speaking = StartCoroutine(coroutine);
    }

    // say and show in box
    public void Say(string speech, string speaker)
    {
        StopSpeaking();
        coroutine = Speaking(speech, speaker);
        speaking = StartCoroutine(coroutine);
    }

    // shoosh
    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(coroutine);
        }

        speaking = null;
    }


    public bool isSpeaking { get { return speaking != null; } }
    [HideInInspector] public bool isWaitingForUserInput = false;
    Coroutine speaking = null;

    IEnumerator Speaking(string targetSpeech, string speaker)
    {
        speechPanel.SetActive(true);
        speechText.text = "";
        speakerNameText.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false;

        while (speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }
        // text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerNameText.text;
        if (s != speakerNameText.text)
            retVal = (s.ToLower().Contains("narrator")) ? "" : s;

        return retVal;
    }

    public void Close()
    {
        StopSpeaking();
        speechPanel.SetActive(false);
    }

    [System.Serializable]
    public class ELEMENTS
    {
        // main panel containing all dialogue related elements on UI
        public GameObject speechPanel;
        public Text speakerNameText;
        public Text speechText;
    }
    public GameObject speechPanel { get { return elements.speechPanel; } }
    public Text speakerNameText { get { return elements.speakerNameText; } }
    public Text speechText { get { return elements.speechText; } }
}

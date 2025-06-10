using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//you need to add these to the top of any script using tmpro and ink stuff
using TMPro;
using Ink.Runtime;


public class DialogueManager : MonoBehaviour
{
    //global variables ink file
    [SerializeField] private TextAsset loadGlobalsJSON;

    //input for dialogue UI
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    //handles the UI to make choices
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextMeshProUGUI[] choicesText;

    //Keeps track of which ink file is being used
    private Story currentStory;

    private bool dialogueIsPlaying;


    // sets up this script as a single instance
    private static DialogueManager instance;

    //lets us look at variables across multiple ink files
    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //makes sure everything is disabled on start 
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //handles choice logic
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update()
    {
        //ends it if there is no dialogue playing 
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetButtonDown("Continue"))
        {
            ContinueStory();
        }
    }

    //this is the function that runs through dialogue in the stories
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    //exits story when it is done
    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        dialogueVariables.StopListening(currentStory);
    }

    //plays through the story
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    //shows choice UI
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;

        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        
    }
    
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        //Debug.Log("choice made");
        ContinueStory();

    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableName == null)
        {
            Debug.Log("variable name null");
        }
        return variableValue;
    }
  
}

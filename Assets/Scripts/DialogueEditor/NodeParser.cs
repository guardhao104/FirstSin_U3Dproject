using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph[] graph;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public int g;

    public GameObject dialoguePanel;
    public GameObject buttonPrefab;
    public GameObject buttonContainer;
    public GameObject player;

    public Transform buttonParent;
    private string answer;

    private ChoiceDialogueNode activeSegment;
    Coroutine _parser;

    // public Image speakerImage;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            foreach (BaseNode b in graph[g].nodes)
            {
                if (b.GetString() == "Start")
                {
                    graph[g].current = b;
                    break;
                }
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogError("ERROR: DialogueGraphs are not there");
        }
        _parser = StartCoroutine(ParseNode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Choices button is pressed
    public void AnswerClicked(int clickedIndex)
    {
        buttonContainer.SetActive(false);
        BaseNode b = graph[g].current;
        var port = activeSegment.GetPort("Answers " + clickedIndex);

        if (port.IsConnected)
        {
            graph[g].current = port.Connection.node as BaseNode;
            _parser = StartCoroutine(ParseNode());
        }
        else
        {
            dialoguePanel.SetActive(false);
            NextNode("input");
            Debug.LogError("ERROR: ChoiceDialogue port is not connected");
        }
    }

    private void UpdateDialogue(ChoiceDialogueNode newSegment)
    {
        activeSegment = newSegment;
        dialogue.text = newSegment.DialogueText;
        speaker.text = newSegment.speakerName;
        int answerIndex = 0;
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var answer in newSegment.Answers)
        {
            var btn = Instantiate(buttonPrefab, buttonParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = answer;

            var index = answerIndex;
            btn.GetComponentInChildren<Button>().onClick.AddListener((() => { AnswerClicked(index);}));
            answerIndex++;
        }
    }

    // Node logic
    IEnumerator ParseNode()
    {
        BaseNode b = graph[g].current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');

        speaker.text = "";
        dialogue.text = "";

        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        if (dataParts[0] == "Start")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            speaker.text ="";
            dialogue.text = "";
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }

        if (dataParts[0] == "ChoiceDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            buttonContainer.SetActive(true);
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];

            UpdateDialogue(b as ChoiceDialogueNode); //Instantiates the buttons 

            if(speaker.text == ""){
                Debug.LogError("ERROR: Speaker text for ChoiceDialogueNode is empty");
            }
            if(dialogue.text == ""){
                Debug.LogError("ERROR: Dialogue text for ChoiceDialogueNode is empty");
            }
        }

        if (dataParts[0] == "DialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            speaker.text = dataParts[1];
            dialogue.text = dataParts[2];

            if(speaker.text == ""){
                Debug.LogError("ERROR: Speaker text for DialogueNode is empty");
            }
            if(dialogue.text == ""){
                Debug.LogError("ERROR: Dialogue text for DialogueNode is empty");
            }
            
            yield return new WaitUntil(() => (dialoguePanel.activeSelf)); 
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //waits for left mouse click input then goes to next node
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }

        if (dataParts[0] == "CloseDialogue_ExitNode")
        {
            // Player.GetComponent<InteractionInstigator>().enabled = true;
            // Player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            dialoguePanel.SetActive(false);
            graph[g].Start(); //loops back to the start node
            speaker.text ="";
            dialogue.text = "";
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }

        if (dataParts[0] == "CloseDialogue_ExitNode_NoLoop_toStart")
        {
            // Player.GetComponent<InteractionInstigator>().enabled = true;
            // Player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            dialoguePanel.SetActive(false);
            speaker.text ="";
            dialogue.text = "";
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }
    }

    public void NextNode(string fieldName)
    {
        speaker.text ="";
        dialogue.text = "";
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        if (_parser != null)
        {
            StopCoroutine(_parser); 
            _parser = null;
        }

        try
        {
            foreach (NodePort p in graph[g].current.Ports)
            {
                try
                {
                    if (p.fieldName == fieldName)
                    {
                        graph[g].current = p.Connection.node as BaseNode;
                        break;
                    }
                }
                catch (NullReferenceException)
                {
                    Debug.LogError("ERROR: Port is not connected");
                }
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogError("ERROR: One of the elements on the Graph array at NodeParser is empty. Or, the Dialogue Graph is empty");
        }

        _parser = StartCoroutine(ParseNode());
    }
}

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
    [Header("Player")]
    public GameObject player;
    public Sprite playerImage;
    private string playerName = "";
    public Sprite libraryImage;
    private string libraryName = "The Great Library";

    [Header("UI Component")]
    public GameObject dialoguePanel;
    public Image headImage;
    public Image headImageBorder;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI descriptionText;
    public GameObject buttonContainer;
    public Transform buttonParent;
    public GameObject buttonPrefab;

    [Header("Text Display")]
    public float textDisplaySpeed;

    private bool textFinished;

    private ChoiceDialogueNode activeSegment;
    Coroutine _parser;

    // pass from NPC script
    private DialogueGraph graph;
    private Sprite npcImage;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialogue(DialogueGraph dg, Sprite image)
    {
        graph = dg;
        npcImage = image;
        try
        {
            foreach (BaseNode b in graph.nodes)
            {
                if (b.GetString() == "Start")
                {
                    graph.current = b;
                    break;
                }
            }
        }
        catch (NullReferenceException)
        {
            Debug.LogError("ERROR: DialogueGraphs are not there");
        }
        dialoguePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.player.Interactive = false;
        NextNode("exit");  //makes sure that StartNode is not activated automatically    
    }

    // Choices button is pressed
    public void AnswerClicked(int clickedIndex)
    {
        buttonContainer.SetActive(false);
        BaseNode b = graph.current;
        var port = activeSegment.GetPort("Answers " + clickedIndex);

        if (port.IsConnected)
        {
            graph.current = port.Connection.node as BaseNode;
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
        BaseNode b = graph.current;
        string data = b.GetString();
        string[] dataParts = data.Split('/');

        speakerNameText.text = "";
        dialogueText.text = "";
        descriptionText.text = "";
        headImage.sprite = null;
        headImage.enabled = false;
        headImageBorder.enabled = false;

        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        if (dataParts[0] == "Start")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            speakerNameText.text = "";
            dialogueText.text = "";
            descriptionText.text = "";
            headImage.sprite = null;
            headImage.enabled = false;
            headImageBorder.enabled = false;
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }

        if (dataParts[0] == "SetFlagNode")
        {
            GameManager.player.SetFlag(dataParts[1], dataParts[2] == "True");
            NextNode("exit");
        }

        if (dataParts[0] == "GetFlagNode")
        {
            bool flag = GameManager.player.GetFlag(dataParts[1]);
            if (flag)
            {
                NextNode("trueExit");
            }
            else
            {
                NextNode("falseExit");
            }
        }

        if (dataParts[0] == "SetClueNode")
        {
            GameManager.player.UpdateClue(int.Parse(dataParts[1]), int.Parse(dataParts[2]));
            NextNode("exit");
        }

        if (dataParts[0] == "NPCChoiceDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            buttonContainer.SetActive(true);
            headImage.sprite = npcImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = dataParts[1];
            StartCoroutine(setTextUI(dataParts[2]));

            yield return new WaitUntil(() => (textFinished));
            UpdateDialogue(b as ChoiceDialogueNode); //Instantiates the buttons 

            if(speakerNameText.text == ""){
                Debug.LogError("ERROR: Speaker text for ChoiceDialogueNode is empty");
            }
            if(dialogueText.text == ""){
                Debug.LogError("ERROR: Dialogue text for ChoiceDialogueNode is empty");
            }
        }

        if (dataParts[0] == "PlayerChoiceDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            buttonContainer.SetActive(true);
            headImage.sprite = playerImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = playerName;
            StartCoroutine(setTextUI(dataParts[2]));

            yield return new WaitUntil(() => (textFinished));
            UpdateDialogue(b as ChoiceDialogueNode); //Instantiates the buttons 

            if(dialogueText.text == ""){
                Debug.LogError("ERROR: Dialogue text for ChoiceDialogueNode is empty");
            }
        }

        if (dataParts[0] == "LibraryChoiceDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            buttonContainer.SetActive(true);
            headImage.sprite = libraryImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = libraryName;
            StartCoroutine(setTextUI(dataParts[2]));

            yield return new WaitUntil(() => (textFinished));
            UpdateDialogue(b as ChoiceDialogueNode); //Instantiates the buttons 

            if(dialogueText.text == ""){
                Debug.LogError("ERROR: Dialogue text for ChoiceDialogueNode is empty");
            }
        }

        if (dataParts[0] == "DescriptionChoiceDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            buttonContainer.SetActive(true);
            headImage.enabled = false;
            headImageBorder.enabled = false;
            speakerNameText.text = "";
            StartCoroutine(setDescriptionTextUI(dataParts[2]));

            yield return new WaitUntil(() => (textFinished));
            UpdateDialogue(b as ChoiceDialogueNode); //Instantiates the buttons 

            if (descriptionText.text == "")
            {
                Debug.LogError("ERROR: Description text for DescriptionNode is empty");
            }
        }

        if (dataParts[0] == "DialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            headImage.sprite = npcImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = dataParts[1];
            StartCoroutine(setTextUI(dataParts[2]));
            

            if(speakerNameText.text == ""){
                Debug.LogError("ERROR: Speaker text for DialogueNode is empty");
            }
            if(dialogueText.text == ""){
                Debug.LogError("ERROR: Dialogue text for DialogueNode is empty");
            }
            
            yield return new WaitUntil(() => (dialoguePanel.activeSelf));
            yield return new WaitUntil(() => (textFinished));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //waits for left mouse click input then goes to next node
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }

        if (dataParts[0] == "PlayerDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            headImage.sprite = playerImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = playerName;
            StartCoroutine(setTextUI(dataParts[1]));

            if (dialogueText.text == "")
            {
                Debug.LogError("ERROR: Dialogue text for PlayerDialogueNode is empty");
            }

            yield return new WaitUntil(() => (dialoguePanel.activeSelf));
            yield return new WaitUntil(() => (textFinished));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //waits for left mouse click input then goes to next node
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }

        if (dataParts[0] == "LibraryDialogueNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            headImage.sprite = libraryImage;
            headImage.enabled = true;
            headImageBorder.enabled = true;
            speakerNameText.text = libraryName;
            StartCoroutine(setTextUI(dataParts[1]));

            if (dialogueText.text == "")
            {
                Debug.LogError("ERROR: Dialogue text for LibraryDialogueNode is empty");
            }

            yield return new WaitUntil(() => (dialoguePanel.activeSelf));
            yield return new WaitUntil(() => (textFinished));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //waits for left mouse click input then goes to next node
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }

        if (dataParts[0] == "DescriptionNode")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            headImage.enabled = false;
            headImageBorder.enabled = false;
            speakerNameText.text = "";
            StartCoroutine(setDescriptionTextUI(dataParts[1]));

            if (descriptionText.text == "")
            {
                Debug.LogError("ERROR: Description text for DescriptionNode is empty");
            }

            yield return new WaitUntil(() => (dialoguePanel.activeSelf));
            yield return new WaitUntil(() => (textFinished));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //waits for left mouse click input then goes to next node
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            NextNode("exit");
        }

        if (dataParts[0] == "CloseDialogue_ExitNode")
        {
            dialoguePanel.SetActive(false);
            GameManager.player.Interactive = true;
            graph.Start(); //loops back to the start node
            speakerNameText.text ="";
            dialogueText.text = "";
            descriptionText.text = "";
            headImage.sprite = null;
            headImage.enabled = false;
            headImageBorder.enabled = false;
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }

        if (dataParts[0] == "CloseDialogue_ExitNode_NoLoop_toStart")
        {
            dialoguePanel.SetActive(false);
            GameManager.player.Interactive = true;
            speakerNameText.text ="";
            dialogueText.text = "";
            descriptionText.text = "";
            headImage.sprite = null;
            headImage.enabled = false;
            headImageBorder.enabled = false;
            foreach (Transform child in buttonParent){
                Destroy(child.gameObject);
            }
        }
    }

    public void NextNode(string fieldName)
    {
        speakerNameText.text ="";
        dialogueText.text = "";
        descriptionText.text = "";
        headImage.sprite = null;
        headImage.enabled = false;
        headImageBorder.enabled = false;
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
            foreach (NodePort p in graph.current.Ports)
            {
                try
                {
                    if (p.fieldName == fieldName)
                    {
                        graph.current = p.Connection.node as BaseNode;
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

    IEnumerator setTextUI(string text)
    {
        textFinished = false;
        dialogueText.text = "";

        int word = 0;
        while (word < text.Length - 1)
        {
            if (Input.GetMouseButton(0))
            {
                break;
            }
            dialogueText.text += text[word];
            word++;
            yield return new WaitForSeconds(textDisplaySpeed);
        }

        dialogueText.text = text;
        textFinished = true;
    }

    IEnumerator setDescriptionTextUI(string text)
    {
        textFinished = false;
        descriptionText.text = "";

        int word = 0;
        while (word < text.Length - 1)
        {
            if (Input.GetMouseButton(0))
            {
                break;
            }
            descriptionText.text += text[word];
            word++;
            yield return new WaitForSeconds(textDisplaySpeed);
        }

        descriptionText.text = text;
        textFinished = true;
    }
}

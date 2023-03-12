using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTrigger : MonoBehaviour
{
    public Image icon;

    public static bool newDialoge = true;

    public Sprite headImage;
    public Sprite newDialogeIcon;
    public Sprite repeatDialogeIcon;

    public int graphNumber = 0;
    public DialogueGraph[] graph;

    private GameObject player;
    private GameObject dialoguePanel;

    private void Awake()
    {
        player = GameObject.Find("Player");
        dialoguePanel = GameObject.Find("DialoguePanel");

        if(newDialoge)
        {
            icon.sprite = newDialogeIcon;
        }
        else
        {
            icon.sprite = repeatDialogeIcon;
        }

        icon.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           icon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            icon.enabled = false;
        }
    }

    void Update()
    {
        if (icon.enabled && dialoguePanel.activeSelf == false && Input.GetKeyDown(KeyCode.E))
        {
            NodeParser dialogueNum = player.GetComponent<NodeParser>();
            dialogueNum.StartDialogue(graph[graphNumber], headImage);
            
            if(graph.Length == graphNumber + 1)
            {
                newDialoge = false;
                icon.sprite = repeatDialogeIcon;
            }
            else
            {
                graphNumber++;
                icon.sprite = newDialogeIcon;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject Button;

    public Sprite headImage;
    public int graphNumber = 0;
    public DialogueGraph[] graph;

    private GameObject player;
    private GameObject dialoguePanel;

    private void Awake()
    {
        player = GameObject.Find("Player");
        dialoguePanel = GameObject.Find("DialoguePanel");
        Button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);
        }
    }

    void Update()
    {
        if (Button.activeSelf && dialoguePanel.activeSelf == false && Input.GetKeyDown(KeyCode.E))
        {
            NodeParser dialogueNum = player.GetComponent<NodeParser>();
            dialogueNum.StartDialogue(graph[graphNumber], headImage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject Button;

    public int graphNumber = 0;
    public DialogueGraph[] graph;

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);
            player = collision.gameObject;
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
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            NodeParser dialogueNum = player.GetComponent<NodeParser>();
            dialogueNum.StartDialogue(graph[graphNumber]);
        }
    }
}

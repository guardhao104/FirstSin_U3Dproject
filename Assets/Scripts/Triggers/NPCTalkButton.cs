using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    private void Awake()
    {
        Button.SetActive(false);
        talkUI.SetActive(false);
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
            talkUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Button.activeSelf && talkUI.activeSelf == false && Input.GetKeyDown(KeyCode.E))
        {
            talkUI.SetActive(true);
        }
    }
}

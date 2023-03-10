using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class EnterDoorTrigger : MonoBehaviour
{
    public GameObject Button;
    public string scenename;
    public string playerDestination;

    private void Awake()
    {
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
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            PlayerDestination.destination = playerDestination;
            SceneManager.LoadScene(scenename);
        }
    }
}

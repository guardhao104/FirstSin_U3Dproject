using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.player.GetFlag("scene1_telToQueen"))
        {
            gameObject.SetActive(false);
        }
    }
}

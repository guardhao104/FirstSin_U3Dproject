using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHookAppearTrigger : MonoBehaviour
{
    void Awake()
    {
        if (!GameManager.player.GetFlag("scene3_Merchant"))
        {
            gameObject.SetActive(false);
        }
    }
}

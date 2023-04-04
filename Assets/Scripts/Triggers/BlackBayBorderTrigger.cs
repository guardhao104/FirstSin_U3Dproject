using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBayBorderTrigger : MonoBehaviour
{
    private Vector3 pos;

    void Awake()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (GameManager.player.GetFlag("scene3_allowFlagship"))
        {
            transform.position = pos;
        }
    }
}

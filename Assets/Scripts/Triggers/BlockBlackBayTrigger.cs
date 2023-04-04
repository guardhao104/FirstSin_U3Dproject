using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBlackBayTrigger : MonoBehaviour
{
    public GameObject soldier;
    public GameObject leftBorder;
    public GameObject rightBorder;

    void Awake()
    {
        if (GameManager.player.GetFlag("scene3_blockBlackBay"))
        {
            gameObject.SetActive(true);
            soldier.transform.rotation = Vector3.zero;
            leftBorder.transform.position = soldier.transform.position;
        }
        else
        {
            gameObject.SetActive(false);
            if (!GameManager.player.GetFlag("scene3_allowFlagship"))
            {
                rightBorder.transform.position = soldier.transform.position;
            }
        }
    }
}

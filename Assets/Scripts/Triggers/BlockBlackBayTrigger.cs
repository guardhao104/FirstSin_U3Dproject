using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBlackBayTrigger : MonoBehaviour
{
    public GameObject leftSoldier;
    public GameObject rightSoldier;
    public GameObject leftBorder;
    public GameObject rightBorder;

    void Awake()
    {
        if (GameManager.player.GetFlag("scene3_blockBlackBay"))
        {
            gameObject.SetActive(true);
            rightSoldier.transform.position = leftSoldier.transform.position + new Vector3(2, 0, 0);
            leftBorder.transform.position = rightSoldier.transform.position;
        }
        else
        {
            gameObject.SetActive(false);
            rightBorder.transform.position = leftSoldier.transform.position;
        }
    }
}

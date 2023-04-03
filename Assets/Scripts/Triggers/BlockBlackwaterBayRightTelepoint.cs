using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBlackwaterBayRightTelepoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!CheckTPState())
        {
            BlockTP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckTPState())
        {
            UnblockTP();
        }
    }

    private bool CheckTPState()
    {
        return GameManager.player.GetFlag("scene3_blockBlackBay");
    }

    private void BlockTP()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void UnblockTP()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}

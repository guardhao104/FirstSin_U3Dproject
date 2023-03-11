using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LoadPlayerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerDestination.destination != null)
        {
            var posX = GameObject.Find(PlayerDestination.destination).transform.position.x;
            float posY = GameObject.Find("Ground").transform.position.y;

            transform.position = new Vector3(posX, posY + 0.1f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

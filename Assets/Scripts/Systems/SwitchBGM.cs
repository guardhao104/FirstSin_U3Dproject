using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBGM : MonoBehaviour
{
    public int bgmIndex;

    void Awake()
    {
        var bgmManager = GameObject.Find("BGMManager");
        if (bgmManager != null)
        {
            var ms = bgmManager.GetComponent<MusicSystem>();
            ms.Stop();
            ms.Play(bgmIndex);
        }
    }
}

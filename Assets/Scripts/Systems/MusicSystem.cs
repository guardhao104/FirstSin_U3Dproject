using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{

    public AudioSource[] audioSource;
    private int currentPlay = 0;
    // Start is called before the first frame update
    void Start()
    {
        Play(currentPlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int index)
    {
        currentPlay = index;
        audioSource[index].Play();
    }

    public void Stop()
    {
        audioSource[currentPlay].Stop();
    }
}

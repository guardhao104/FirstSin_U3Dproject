using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingSceneSystem : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public string switchSceneName;

    private ClueSystem clueSystem;

    private void Awake()
    {
        clueSystem = GameObject.Find("ClueManager").GetComponent<ClueSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.player.GetFlag("Ending"))
        {
            clueSystem.RemoveClues();
            PlayVideo();
        }
    }

    void PlayVideo()
    {
        GameManager.player.SetFlag("Ending", false);

        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = videoPlayer1;

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = true;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 1F;

        // Restart from beginning when done.
        videoPlayer.isLooping = false;

        // Each time we reach the end, we close the video.
        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        SceneManager.LoadScene(switchSceneName);
    }
}

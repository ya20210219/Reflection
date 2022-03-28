using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoopMovie : MonoBehaviour
{
    private float time;
    public VideoClip videoClip;
    public GameObject screen;
    public float limit;

    void Start()
    {
        time = 0.0f;

        var videoPlayer = screen.AddComponent<VideoPlayer>();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        videoPlayer.isLooping = true;
        videoPlayer.Stop();
    }


    void Update()
    {
        time += Time.deltaTime;
        var videoPlayer = GetComponent<VideoPlayer>();
        if (time > limit)
        {
            videoPlayer.Play();
        }
    }
}

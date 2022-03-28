using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FirstMovie : MonoBehaviour
{
    private float time;
    public VideoClip videoClip;
    public GameObject screen;
    public float limit;
    public bool titleFinish;

    void Start()
    {
        time = 0;
        titleFinish = false;

        var videoPlayer = screen.AddComponent<VideoPlayer>();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        videoPlayer.isLooping = false;

        videoPlayer.Play();
    }


    void Update()
    {
        time += Time.deltaTime;
        var videoPlayer = GetComponent<VideoPlayer>();
       
        if(time > limit)
        {
            gameObject.SetActive(false);
            titleFinish = true;
        }
    }
}

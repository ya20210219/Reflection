using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpMovie : MonoBehaviour
{
    public VideoClip videoClip;
    public GameObject screen;
    public float limit;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        var videoPlayer = screen.AddComponent<VideoPlayer>();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;

        videoPlayer.isLooping = false;

        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        var videoPlayer = GetComponent<VideoPlayer>();
        time += Time.deltaTime;
        if (videoPlayer.isPlaying == false && time >10)
        {
            SceneManager.LoadScene("1-1");
        }
    }
}

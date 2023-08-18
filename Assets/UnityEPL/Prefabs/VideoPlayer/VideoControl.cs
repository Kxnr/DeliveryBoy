﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public bool deactivateWhenFinished = true;
    public bool canSkip = false;

    void Update()
    {
        // TODO: JPB: (Hokua) Fix the video pause
        // Pause
        //if (Input.GetKeyDown(KeyCode.Space)) 
        //{
        //    if (videoPlayer.isPlaying)
        //        videoPlayer.Pause();
        //    else
        //        videoPlayer.Play();
        //}

        #if !UNITY_WEBGL // WebGL No Secret Key
            // Stop
            if (InputManager.GetButtonDown("Secret"))
            {
                videoPlayer.Stop();
                gameObject.SetActive(false);
            }

            if (InputManager.GetButtonDown("Continue") && canSkip)
            {
                videoPlayer.Stop();
                gameObject.SetActive(false);
            }

        // Video finished
        if (videoPlayer.time >= videoPlayer.clip.length)
            {
                Debug.Log("VideoControl end video");
                gameObject.SetActive(false);
            }
        #endif
    }


    public void StartVideo(bool replay)
    {
        canSkip = replay;

        Debug.Log("VideoControl start video");
        videoPlayer.loopPointReached += (VideoPlayer vp) => gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public bool IsPlaying()
    {
        return gameObject.activeSelf;
    }
}

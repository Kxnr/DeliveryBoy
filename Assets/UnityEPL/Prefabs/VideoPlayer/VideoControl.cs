﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class VideoControl : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public bool deactivateWhenFinished = true;

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
            if (InputManager.GetButtonDown("Secret") || InputManager.GetButtonDown("ExperimenterSecret"))
            {
                videoPlayer.Stop();
                gameObject.SetActive(false);
            }
        #endif

        // Video finished
        if (videoPlayer.time >= videoPlayer.clip.length)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartVideo()
    {
        Debug.Log("VideoControl start video");
        #if UNITY_WEBGL
            videoPlayer.loopPointReached += (VideoPlayer vp) => gameObject.SetActive(false);
        #endif
        gameObject.SetActive(true);
    }

    public bool IsPlaying()
    {
        return gameObject.activeSelf;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class VideoControl : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public bool deactivateWhenFinished = true;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) // Pause
        //{
        //    if (videoPlayer.isPlaying)
        //        videoPlayer.Pause();
        //    else
        //        videoPlayer.Play();
        //}
        // JPB: TODO: Change this
        
        if (InputManager.GetButtonDown("Secret")) // Stop
        {
            videoPlayer.Stop();
            gameObject.SetActive(false);
        }
        if (videoPlayer.time >= videoPlayer.clip.length)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartVideo()
    {
        Debug.Log("VideoControl");
        gameObject.SetActive(true);
    }

    public bool IsPlaying()
    {
        return gameObject.activeSelf;
    }
}

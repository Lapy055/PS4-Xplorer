using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.PS4;

public class PS4VideoPlaybackSample : MonoBehaviour
{
    public Image videoImage;
    
    PS4VideoPlayer video;
    PS4ImageStream lumaTex;
    PS4ImageStream chromaTex;
    PS4VideoPlayer.Looping isLooping = PS4VideoPlayer.Looping.None;

    public void OnDisable()
    {
        video.Stop();
    }

    void Start()
    {
        // In 5.3 this event is triggered automatically, as an optimization in 5.4 onwards you need to register the callback
        PS4VideoPlayer.OnMovieEvent += OnMovieEvent;

        video = new PS4VideoPlayer(); // This sets up a VideoDecoderType.DEFAULT system
        video.PerformanceLevel = PS4VideoPlayer.Performance.Optimal;
        video.demuxVideoBufferSize = 2 * 1024 * 1024; // Change the demux buffer from it's 1mb default
        video.numOutputVideoFrameBuffers = 2; // Increasing this can stop frame stuttering

        lumaTex = new PS4ImageStream();
        lumaTex.Create(1920, 1080, PS4ImageStream.Type.R8, 0);
        chromaTex = new PS4ImageStream();
        chromaTex.Create(1920 / 2, 1080 / 2, PS4ImageStream.Type.R8G8, 0);
        video.Init(lumaTex, chromaTex);

        // Apply video textures to the UI image
        videoImage.material.SetTexture("_MainTex", lumaTex.GetTexture());
        videoImage.material.SetTexture("_CromaTex", chromaTex.GetTexture());
    }

    void Update()
    {
        // Required to keep the video processing
        video.Update();

        if (Controlador.instancia.EnVideo)
        {
            Controlador.instancia.EnVideo = false;

            video.Play(Controlador.instancia.camino, isLooping);
            video.SetVolume(100);
        }

        CropVideo();
    }

    void CropVideo()
    {
        // The video player on the PS4 frequently generates video on larger textures than the video, and requires us to crop the video. This code calculates
        // the crop values and passes the data on to the TRANSFORM_TEX call in the shader, without it we get nasty black borders at the edge of video
        if (videoImage != null)
        {
            int cropleft, cropright, croptop, cropbottom, width, height;
            video.GetVideoCropValues(out cropleft, out cropright, out croptop, out cropbottom, out width, out height);
            float scalex = 1.0f;
            float scaley = 1.0f;
            float offx = 0.0f;
            float offy = 0.0f;

            if ((width > 0) && (height > 0))
            {
                int fullwidth = width + cropleft + cropright;
                scalex = (float)width / (float)fullwidth;
                offx = (float)cropleft / (float)fullwidth;
                int fullheight = height + croptop + cropbottom;
                scaley = (float)height / (float)fullheight;
                offy = (float)croptop / (float)fullheight;
            }

            // Typically we want to invert the Y on the video because thats how planes UV's are layed out
            videoImage.material.SetVector("_MainTex_ST", new Vector4(scalex, scaley * -1, offx, 1 - offy));
        }
    }

    void OnMovieEvent(int FMVevent)
    {
        ;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] ParticleSystem circle0;
    [SerializeField] ParticleSystem circle1;
    [SerializeField] Animator light;
    //[SerializeField] AudioSource sound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Play();
        else if (Input.GetKeyDown(KeyCode.T))
            PlayRed();
        else if (Input.GetKeyDown(KeyCode.Y))
            Stop();
    }

    #region ○ CICLES ○
    private void PlayCircles()
    {
        circle0.Play();
        circle1.Play();
    }
    #endregion
    #region • LIGHTS •
    public void Play()
    {
        Stop();
        //sound.Play();
        PlayCircles();
        light.SetBool("LightActived", true);
    }
    public void PlayRed()
    {
        Stop();
        PlayCircles();
        light.SetBool("LightActivedRed", true);
    }
    public void Stop()
    {
        circle0.Stop();
        circle1.Stop();
        light.SetBool("LightActived", false);
        light.SetBool("LightActivedRed", false);
    }
    #endregion
}

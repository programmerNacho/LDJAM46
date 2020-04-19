using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] ParticleSystem circle0;
    [SerializeField] ParticleSystem circle1;
    [SerializeField] Animator light;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Play();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Stop();
        }
    }

    public void Play()
    {
        circle0.Play();
        circle1.Play();
        light.SetBool("LightActived", true);
    }
    public void Stop()
    {
        circle0.Stop();
        circle1.Stop();
        light.SetBool("LightActived", false);
    }
}

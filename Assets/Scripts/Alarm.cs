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
            circle0.Play();
            circle1.Play();
            light.SetBool("LightActived", true);
            //animator.enabled=true;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            circle0.Stop();
            circle1.Stop();
            light.SetBool("LightActived", false);
            //animator.enabled=false;
        }
    }

}

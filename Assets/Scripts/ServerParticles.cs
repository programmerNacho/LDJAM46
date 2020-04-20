using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDJAM46
{
    public class ServerParticles : MonoBehaviour
    {
        [SerializeField]
        private Server server;
        [SerializeField]
        private ParticleSystem pL1;
        [SerializeField]
        private ParticleSystem pL2;
        [SerializeField]
        private ParticleSystem pL3;
        void Start()
        {
            server = GetComponent<Server>();
            DeActivateParticleSystem(pL1);
            DeActivateParticleSystem(pL2);
            DeActivateParticleSystem(pL3);
        }
        void Update()
        {
            if (server.Temperature>=50)
            {
                ActivateParticleSystem(pL1);
            }
            else
            {
                DeActivateParticleSystem(pL1);
            }
            if (server.Temperature >= 75)
            {
                ActivateParticleSystem(pL2);
            }
            else
            {
                DeActivateParticleSystem(pL2);
            }
            if (server.Temperature >= 90)
            {
                ActivateParticleSystem(pL3);
            }
            else
            {
                DeActivateParticleSystem(pL3);
            }
        }

        private void ActivateParticleSystem(ParticleSystem ptcSystem)
        {
            ptcSystem.gameObject.SetActive(true);
        }
        private void DeActivateParticleSystem(ParticleSystem ptcSystem)
        {
            ptcSystem.gameObject.SetActive(false);
        }
    }
}
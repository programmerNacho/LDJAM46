using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LDJAM46
{
    public class ServerAudioEvent : MonoBehaviour
    {
        public UnityEvent OnServerStartHeating = new UnityEvent();

        private Server[] servers;
        private bool[] serversHeated;

        private void Start()
        {
            servers = FindObjectsOfType<Server>();
            serversHeated = new bool[servers.Length];
        }

        private void Update()
        {
            for (int i = 0; i < servers.Length; i++)
            {
                if(servers[i].Temperature >= 80f && !serversHeated[i])
                {
                    serversHeated[i] = true;
                    OnServerStartHeating.Invoke();
                }
                else if(servers[i].Temperature < 80f)
                {
                    serversHeated[i] = false;
                }
            }
        }
    }
}

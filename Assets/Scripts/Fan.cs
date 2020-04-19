using UnityEngine;

namespace LDJAM46
{
    public class Fan : MonoBehaviour
    {
        [SerializeField]
        private ServerModification modification;
        [SerializeField]
        private Trigger coolingTrigger;

        private void Start()
        {
            coolingTrigger.OnEnter.AddListener(OnCoolingEnter);
            coolingTrigger.OnExit.AddListener(OnCoolingExit);
        }

        private void OnCoolingEnter(Collider other)
        {
            ServerModificationManager serverModificationManager = other.GetComponent<ServerModificationManager>();
            if (serverModificationManager != null)
            {
                serverModificationManager.AddModification(modification);
            }
        }

        private void OnCoolingExit(Collider other)
        {
            ServerModificationManager serverModificationManager = other.GetComponent<ServerModificationManager>();
            if (serverModificationManager != null)
            {
                serverModificationManager.RemoveModification(modification);
            }
        }
    }
}

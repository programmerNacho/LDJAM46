using UnityEngine;

namespace LDJAM46
{
    public class Fan : MonoBehaviour
    {
        [SerializeField]
        private ServerModification modification;
        [SerializeField]
        private Trigger coolingTrigger;
        [SerializeField]
        private ParticleSystem fanParticle;

        private void Start()
        {
            fanParticle.gameObject.SetActive(false);
            coolingTrigger.OnEnter.AddListener(OnCoolingEnter);
            coolingTrigger.OnExit.AddListener(OnCoolingExit);
        }

        private void OnCoolingEnter(Collider other)
        {
            ServerModificationManager serverModificationManager = other.GetComponent<ServerModificationManager>();
            if (serverModificationManager != null)
            {
                ActivateParticle(fanParticle);
                serverModificationManager.AddModification(modification);
            }
        }

        private void OnCoolingExit(Collider other)
        {
            ServerModificationManager serverModificationManager = other.GetComponent<ServerModificationManager>();
            if (serverModificationManager != null)
            {
                DeactivateParticle(fanParticle);
                serverModificationManager.RemoveModification(modification);
            }
        }
        private void ActivateParticle(ParticleSystem t_ptc)
        {
            t_ptc.gameObject.SetActive(true);
        }
        private void DeactivateParticle(ParticleSystem t_ptc)
        {
            t_ptc.gameObject.SetActive(false);
        }
    }
}

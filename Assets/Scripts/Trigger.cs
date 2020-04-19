using UnityEngine;
using UnityEngine.Events;

namespace LDJAM46
{
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider> { }

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Trigger : MonoBehaviour
    {
        public TriggerEvent OnEnter = new TriggerEvent();
        public TriggerEvent OnStay = new TriggerEvent();
        public TriggerEvent OnExit = new TriggerEvent();

        private new Collider collider;
        private new Rigidbody rigidbody;

        private void Start()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();

            collider.isTrigger = true;
            rigidbody.isKinematic = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnter.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit.Invoke(other);
        }
    }
}

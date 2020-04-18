using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactionLayer;
    [SerializeField]
    private string noCollisionInteractionLayer;
    [SerializeField]
    private float maxGrabDistance;
    [SerializeField]
    private float minGrabDistance;
    [SerializeField]
    private float zoomVelocity;
    [SerializeField]
    private float throwStrength;

    private Interactable currentInteractableObject;
    private Rigidbody currentInteractableObjectRigidbody;
    private RigidbodyInterpolation initialInterpolationSetting;
    private int initialLayer;
    private float currentGrabDistance;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(currentInteractableObject == null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, maxGrabDistance, interactionLayer, QueryTriggerInteraction.Ignore))
                {
                    currentInteractableObject = hit.transform.GetComponent<Interactable>();
                    if(currentInteractableObject != null)
                    {
                        currentGrabDistance = Mathf.Clamp(hit.distance, minGrabDistance, maxGrabDistance);
                        currentInteractableObjectRigidbody = currentInteractableObject.GetComponent<Rigidbody>();
                        if(currentInteractableObjectRigidbody != null)
                        {
                            initialInterpolationSetting = currentInteractableObjectRigidbody.interpolation;
                            initialLayer = currentInteractableObjectRigidbody.gameObject.layer;
                            currentInteractableObjectRigidbody.gameObject.layer = LayerMask.NameToLayer(noCollisionInteractionLayer); ;
                            currentInteractableObjectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        }
                    }
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(currentInteractableObjectRigidbody != null)
                {
                    currentInteractableObject = null;
                    currentInteractableObjectRigidbody.interpolation = initialInterpolationSetting;
                    currentInteractableObjectRigidbody.gameObject.layer = initialLayer;
                    currentInteractableObjectRigidbody = null;
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                if (currentInteractableObjectRigidbody != null)
                {
                    currentInteractableObject = null;
                    currentInteractableObjectRigidbody.interpolation = initialInterpolationSetting;
                    currentInteractableObjectRigidbody.gameObject.layer = initialLayer;
                    currentInteractableObjectRigidbody.velocity = Vector3.zero;
                    currentInteractableObjectRigidbody.AddForce(mainCamera.transform.forward * throwStrength, ForceMode.Impulse);
                    currentInteractableObjectRigidbody = null;
                }
            }
            float mouseScrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheelInput != 0f)
            {
                currentGrabDistance = Mathf.Clamp(currentGrabDistance + mouseScrollWheelInput * zoomVelocity, minGrabDistance, maxGrabDistance);
            }
        }
    }

    private void FixedUpdate()
    {
        if(currentInteractableObject != null && currentInteractableObjectRigidbody != null)
        {
            Vector3 holdPoint = mainCamera.transform.position + mainCamera.transform.forward * currentGrabDistance;
            Vector3 toDestination = holdPoint - currentInteractableObjectRigidbody.position;
            Vector3 force = toDestination / Time.fixedDeltaTime;

            currentInteractableObjectRigidbody.velocity = Vector3.zero;
            currentInteractableObjectRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}

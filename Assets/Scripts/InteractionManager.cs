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
    private float rotateSpeed;
    [SerializeField]
    private float throwStrength;

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
        if(currentInteractableObjectRigidbody == null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, maxGrabDistance, interactionLayer, QueryTriggerInteraction.Ignore))
                {
                    currentInteractableObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                    if(currentInteractableObjectRigidbody != null)
                    {
                        currentGrabDistance = Mathf.Clamp(hit.distance, minGrabDistance, maxGrabDistance);
                        initialInterpolationSetting = currentInteractableObjectRigidbody.interpolation;
                        initialLayer = currentInteractableObjectRigidbody.gameObject.layer;
                        currentInteractableObjectRigidbody.gameObject.layer = LayerMask.NameToLayer(noCollisionInteractionLayer); ;
                        currentInteractableObjectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
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
                    currentInteractableObjectRigidbody.interpolation = initialInterpolationSetting;
                    currentInteractableObjectRigidbody.gameObject.layer = initialLayer;
                    currentInteractableObjectRigidbody = null;
                }
            }
            else if(Input.GetMouseButtonUp(1))
            {
                if (currentInteractableObjectRigidbody != null)
                {
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
            if(Input.GetKey(KeyCode.E))
            {
                Quaternion newRotation = Quaternion.Euler(currentInteractableObjectRigidbody.rotation.eulerAngles + new Vector3(0f, 1f * rotateSpeed * Time.deltaTime, 0f));
                currentInteractableObjectRigidbody.MoveRotation(newRotation);
            }
            if(Input.GetKey(KeyCode.Q))
            {
                Quaternion newRotation = Quaternion.Euler(currentInteractableObjectRigidbody.rotation.eulerAngles + new Vector3(0f, -1f * rotateSpeed * Time.deltaTime, 0f));
                currentInteractableObjectRigidbody.MoveRotation(newRotation);
            }
        }
    }

    private void FixedUpdate()
    {
        if(currentInteractableObjectRigidbody != null)
        {
            Vector3 holdPoint = mainCamera.transform.position + mainCamera.transform.forward * currentGrabDistance;
            Vector3 toDestination = holdPoint - currentInteractableObjectRigidbody.position;
            Vector3 force = toDestination / Time.fixedDeltaTime;

            currentInteractableObjectRigidbody.angularVelocity = Vector3.zero;
            currentInteractableObjectRigidbody.velocity = Vector3.zero;
            currentInteractableObjectRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}

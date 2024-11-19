using System;
using System.Collections;
using UnityEngine;

public class ShowObjectOnProximity : MonoBehaviour
{
    [Tooltip("The distance at which the object should be shown")]
    [SerializeField] private float distanceThreshold = 10f;
    [Tooltip("The model of the object")]
    [SerializeField] private GameObject obj;
    [Tooltip("The offset of object in Y-direction")]
    [SerializeField] private float objHeightOffset = 0f;

    private GameObject player;
    [HideInInspector] public bool isObjectSeen;
    private bool isPreviouslyDeliveredObject = false;
    private bool hasBeenWrittenToFile = false;
    private int numberOfRays = 20;
    private Color rayColor = Color.red;
    private Vector3 initialPlayerPosition;
    private float movementThreshold = 2f;

    public static Action OnPlayObjectSound;
    public static Action<string> OnPreviousItemLocation;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
        }
        obj.SetActive(false);
    }

    private void Start()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = Vector3.down;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin, direction, 100.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.name.Contains("Road") || hit.collider.name.Contains("road"))
            {
                transform.position = hit.collider.bounds.center;
            }
        }
    }
    private void OnEnable()
    {
        DeliveryExperiment.OnObjectDelivered += ResetWrittenToFileStatus;
    }

    private void OnDisable()
    {
        DeliveryExperiment.OnObjectDelivered -= ResetWrittenToFileStatus;
    }

    void Update()
    {
        // if new object
        if(!isPreviouslyDeliveredObject)
        {
            if (player != null)
            {
                if (HasPlayerMoved())
                {
                    float distance = Vector3.Distance(player.transform.position, transform.position);
                    if (distance <= distanceThreshold && IsObjectInFrontOfPlayer())
                    {
                        if (!obj.activeInHierarchy)
                        {
                            RotateObjectToFacePlayer();
                            obj.SetActive(true);
                            isObjectSeen = true;
                            OnPlayObjectSound?.Invoke();
                            obj.AddComponent<Spin>();
                        }
                    }
                    else
                    {
                        if (!isObjectSeen)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
            }
        }
        else // if previously delivered object
        {
            if (player != null)
            {
                if (HasPlayerMoved())
                {
                    float distance = Vector3.Distance(player.transform.position, transform.position);
                    if (distance <= distanceThreshold && IsObjectInFrontOfPlayer() && !hasBeenWrittenToFile)
                    {
                        hasBeenWrittenToFile = true;
                        OnPreviousItemLocation?.Invoke(this.gameObject.name);
                    }
                }
            }
            
        }
    }

    public float GetHeightOffset()
    {
        return objHeightOffset;
    }

    public bool IsObjectInFrontOfPlayer()
    {
        if (player != null && obj != null)
        {
            Vector3 playerPosition = player.transform.position;

            // Calculate the step angle between each ray for a 150-degree cone
            float stepAngle = 150f / (numberOfRays - 1); // 150 degrees divided by the number of rays

            // Loop through each angle within the -75 to 75 degree range (for a 150-degree cone)
            for (int i = 0; i < numberOfRays; i++)
            {
                float currentAngle = -75f + (i * stepAngle);
                Vector3 rayDirection = Quaternion.Euler(0, currentAngle, 0) * player.transform.forward;

                // Draw the ray in the editor
                //Debug.DrawRay(playerPosition, rayDirection * distanceThreshold, rayColor);

                RaycastHit[] hits = Physics.RaycastAll(playerPosition, rayDirection, distanceThreshold);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject == obj.transform.parent.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool HasPlayerMoved()
    {
        if (player != null)
        {
            float distanceMoved = Vector3.Distance(initialPlayerPosition, player.transform.position);
            return distanceMoved >= movementThreshold;
        }
        return false;
    }

    private void RotateObjectToFacePlayer()
    {
        if (player != null && obj != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            obj.transform.forward = directionToPlayer.normalized;
        }
    }

    public void DisableObj()
    {
        obj.SetActive(false);
        isPreviouslyDeliveredObject = true;
    }

    private void ResetWrittenToFileStatus()
    {
        hasBeenWrittenToFile = false;
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Unity.FPS.Game;
using System;

public class SubCubeDetector : MonoBehaviour
{
    public Vector3 detectionCenter;
    public float detectionRadius = 5f;
    public string subCubeTag = "SubCubeTag";
    public int subCubeNum = 0;

    [Header("Cube Count Bounds")]
    public int lowerBound = 500;  
    public int upperBound = 700;  
    public bool isInbound;

    [Header("Inbound Timer")]
    public float requiredInboundTime = 5f; 
    public UnityEvent onInboundTimerComplete; 
    public float currentInboundTime = 0f;
    public bool timerCompleted = false;
    public static event Action<int> OnTimerComplete;

    private TextMeshProUGUI textMeshPro;
    private Transform cameraTransform;
    public Transform cylinderTransform;

    void Start()
    {
        detectionCenter = transform.position;
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        isInbound = false;

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on child objects.");
        }
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        DetectSubCube();
        if (textMeshPro != null)
        {
            UpdateTextDisplay();
        }
        UpdateInboundTimer();
    }
    private void UpdateInboundTimer()
    {
        if (isInbound && !timerCompleted)
        {
            currentInboundTime += Time.deltaTime;
            if (currentInboundTime >= requiredInboundTime)
            {
                timerCompleted = true;
                onInboundTimerComplete.Invoke();
            }
        }
        else if (!isInbound)
        {
            currentInboundTime = 0f;
            timerCompleted = false;
        }
    }

    private void UpdateTextDisplay()
    {
        textMeshPro.text = "Cubes in Range: " + subCubeNum;
        if (isInbound)
        {
            textMeshPro.color = Color.green;
        }
        else
        {
            textMeshPro.color = Color.red;
        }

        textMeshPro.transform.rotation = Quaternion.LookRotation(textMeshPro.transform.position - cameraTransform.position);
    }

    public void onTimerComplete()
    {
        Debug.Log("Inbound timer completed!");
        OnTimerComplete?.Invoke(subCubeNum);
    }

    public void UpdateCylinderSize()
    {
        if (cylinderTransform != null)
        {
            float diameter = detectionRadius * 2;
            cylinderTransform.localScale = new Vector3(diameter, 0.05f, diameter);
            Vector3 newPosition = detectionCenter - transform.position;
            newPosition.y = 0.25f;
            cylinderTransform.localPosition = newPosition;
        }
        else
        {
            Debug.LogError("Cylinder transform is not set.");
        }
    }

    public void DetectSubCube()
    {
        int subCubeCount = DetectSubCubesInSphere(detectionCenter, detectionRadius);
        subCubeNum = subCubeCount;
        bool wasInbound = isInbound;
        isInbound = subCubeNum >= lowerBound && subCubeNum <= upperBound;

        if (wasInbound && !isInbound)
        {
            currentInboundTime = 0f;
            timerCompleted = false;
        }
    }

    public void MoveDetectionCenterToPosition()
    {
        detectionCenter = transform.position;
        Debug.Log("Detection center moved to current position: " + detectionCenter);
    }

    int DetectSubCubesInSphere(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int count = 0;
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(subCubeTag))
            {
                count++;
            }
        }

        return count;
    }

    public void ResetInboundTimer()
    {
        currentInboundTime = 0f;
        timerCompleted = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionCenter, detectionRadius);
    }
}
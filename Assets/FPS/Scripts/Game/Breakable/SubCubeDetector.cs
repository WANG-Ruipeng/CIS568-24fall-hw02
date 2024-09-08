using UnityEngine;
using TMPro;

public class SubCubeDetector : MonoBehaviour
{
    public Vector3 detectionCenter; // ���μ�������
    public float detectionRadius = 5f; // ���μ��İ뾶
    public string subCubeTag = "SubCubeTag"; // С�����Tag
    public int subCubeNum = 0;

    private TextMeshProUGUI textMeshPro; // ����TextMeshPro���
    private Transform cameraTransform; // �����������Transform

    void Start()
    {
        detectionCenter = transform.position;

        // ��ȡTextMeshPro���������
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on child objects.");
        }

        // ��ȡ�������Transform
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        DetectSubCube();

        // ����TextMeshPro����������
        if (textMeshPro != null)
        {
            textMeshPro.text = "Cubes in Range: " + subCubeNum;
            // ��TMP����ʼ�ճ��������
            textMeshPro.transform.rotation = Quaternion.LookRotation(textMeshPro.transform.position - cameraTransform.position);
        }
    }

    public void DetectSubCube()
    {
        int subCubeCount = DetectSubCubesInSphere(detectionCenter, detectionRadius);
        subCubeNum = subCubeCount;
    }

    public void MoveDetectionCenterToPosition()
    {
        detectionCenter = transform.position;
        Debug.Log("Detection center moved to current position: " + detectionCenter);
    }

    int DetectSubCubesInSphere(Vector3 center, float radius)
    {
        // ʹ��Physics.OverlapSphere�����������ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        int count = 0;
        // ������⵽��������ײ�壬ɸѡ�������ض�Tag������
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(subCubeTag))
            {
                count++;
            }
        }

        return count; // ���ؼ�⵽��С��������
    }

    // ���ӻ���ⷶΧ�����ڵ���
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionCenter, detectionRadius);
    }
}

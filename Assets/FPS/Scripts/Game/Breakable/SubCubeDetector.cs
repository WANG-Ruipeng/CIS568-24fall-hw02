using UnityEngine;
using TMPro;

public class SubCubeDetector : MonoBehaviour
{
    public Vector3 detectionCenter; // 球形检测的中心
    public float detectionRadius = 5f; // 球形检测的半径
    public string subCubeTag = "SubCubeTag"; // 小方块的Tag
    public int subCubeNum = 0;

    private TextMeshProUGUI textMeshPro; // 引用TextMeshPro组件
    private Transform cameraTransform; // 引用主相机的Transform

    void Start()
    {
        detectionCenter = transform.position;

        // 获取TextMeshPro组件的引用
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on child objects.");
        }

        // 获取主相机的Transform
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        DetectSubCube();

        // 更新TextMeshPro的文字内容
        if (textMeshPro != null)
        {
            textMeshPro.text = "Cubes in Range: " + subCubeNum;
            // 让TMP对象始终朝向主相机
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
        // 使用Physics.OverlapSphere检测给定区域内的所有碰撞体
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        int count = 0;
        // 遍历检测到的所有碰撞体，筛选出具有特定Tag的物体
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(subCubeTag))
            {
                count++;
            }
        }

        return count; // 返回检测到的小方块数量
    }

    // 可视化检测范围，便于调试
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionCenter, detectionRadius);
    }
}

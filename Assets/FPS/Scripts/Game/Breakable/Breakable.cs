using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [Header("Sub Cube Settings")]
    public int subCubeCount = 10;
    public GameObject fullCube;
    public GameObject fragmentParent;

    [Header("Movement Settings")]
    public Transform playerTransform; // 玩家位置引用
    public float maxSpeed = 10f; // 最大移动速度
    public float minSpeed = 1f;  // 最小移动速度
    public float moveSpeedMultiplier = 1f; // 速度调整系数

    private List<GameObject> subCubes = new List<GameObject>();
    bool subCubesActivated = false;

    void Start()
    {
        // 初始化playerTransform，获取场景中名字为“Player”的Transform
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene!");
        }

        GenerateSubCubes();
    }

    void GenerateSubCubes()
    {
        float subCubeSize = fullCube.transform.localScale.x / subCubeCount;
        Vector3 startPos = fullCube.transform.position - fullCube.transform.localScale / 2 + new Vector3(subCubeSize / 2, subCubeSize / 2, subCubeSize / 2);

        for (int x = 0; x < subCubeCount; x++)
        {
            for (int y = 0; y < subCubeCount; y++)
            {
                for (int z = 0; z < subCubeCount; z++)
                {
                    Vector3 pos = startPos + new Vector3(x * subCubeSize, y * subCubeSize, z * subCubeSize);
                    GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube); // 直接生成Cube
                    subCube.transform.position = pos;
                    subCube.transform.localScale = new Vector3(subCubeSize, subCubeSize, subCubeSize);
                    subCube.transform.parent = fragmentParent.transform; // 设置为FragmentParent的子物体

                    subCube.tag = "SubCubeTag";

                    Rigidbody rb = subCube.AddComponent<Rigidbody>(); // 添加刚体组件
                    rb.useGravity = false; // 禁用gravity
                    subCube.SetActive(false); // 先禁用小方块

                    subCubes.Add(subCube);
                }
            }
        }
    }

    public void ActivateSubCubes()
    {
        if(subCubesActivated == false)
        {
            subCubesActivated = true;

            foreach (GameObject subCube in subCubes)
            {
                subCube.SetActive(true); // 激活小方块
                Rigidbody rb = subCube.GetComponent<Rigidbody>();
                rb.useGravity = true; // 启用gravity
                fullCube.SetActive(false); // 禁用原始的大方块
            }

        }

        foreach (GameObject subCube in subCubes)
        {
            Rigidbody rb = subCube.GetComponent<Rigidbody>();
            // 从玩家方向向物体本身施加初始速度
            Vector3 directionFromPlayer = (subCube.transform.position - playerTransform.position).normalized;
            float distanceFromPlayer = Vector3.Distance(playerTransform.position, subCube.transform.position);
            float speed = Mathf.Lerp(maxSpeed, minSpeed, distanceFromPlayer / 10f) * moveSpeedMultiplier; // 假设10f为触发减速的距离

            rb.velocity = directionFromPlayer * speed;
        }
        
    }

    /// <summary>
    /// 设置目标点，允许外部控制目标位置
    /// </summary>
    /// <param name="newTarget">新的目标位置</param>
    public void SetTargetPoint(Vector3 newTarget)
    {
        //targetPoint = newTarget;
    }
}

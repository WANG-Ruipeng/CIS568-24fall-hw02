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
    public Transform playerTransform; // ���λ������
    public float maxSpeed = 10f; // ����ƶ��ٶ�
    public float minSpeed = 1f;  // ��С�ƶ��ٶ�
    public float moveSpeedMultiplier = 1f; // �ٶȵ���ϵ��

    private List<GameObject> subCubes = new List<GameObject>();
    bool subCubesActivated = false;

    void Start()
    {
        // ��ʼ��playerTransform����ȡ����������Ϊ��Player����Transform
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
                    GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube); // ֱ������Cube
                    subCube.transform.position = pos;
                    subCube.transform.localScale = new Vector3(subCubeSize, subCubeSize, subCubeSize);
                    subCube.transform.parent = fragmentParent.transform; // ����ΪFragmentParent��������

                    subCube.tag = "SubCubeTag";

                    Rigidbody rb = subCube.AddComponent<Rigidbody>(); // ��Ӹ������
                    rb.useGravity = false; // ����gravity
                    subCube.SetActive(false); // �Ƚ���С����

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
                subCube.SetActive(true); // ����С����
                Rigidbody rb = subCube.GetComponent<Rigidbody>();
                rb.useGravity = true; // ����gravity
                fullCube.SetActive(false); // ����ԭʼ�Ĵ󷽿�
            }

        }

        foreach (GameObject subCube in subCubes)
        {
            Rigidbody rb = subCube.GetComponent<Rigidbody>();
            // ����ҷ��������屾��ʩ�ӳ�ʼ�ٶ�
            Vector3 directionFromPlayer = (subCube.transform.position - playerTransform.position).normalized;
            float distanceFromPlayer = Vector3.Distance(playerTransform.position, subCube.transform.position);
            float speed = Mathf.Lerp(maxSpeed, minSpeed, distanceFromPlayer / 10f) * moveSpeedMultiplier; // ����10fΪ�������ٵľ���

            rb.velocity = directionFromPlayer * speed;
        }
        
    }

    /// <summary>
    /// ����Ŀ��㣬�����ⲿ����Ŀ��λ��
    /// </summary>
    /// <param name="newTarget">�µ�Ŀ��λ��</param>
    public void SetTargetPoint(Vector3 newTarget)
    {
        //targetPoint = newTarget;
    }
}

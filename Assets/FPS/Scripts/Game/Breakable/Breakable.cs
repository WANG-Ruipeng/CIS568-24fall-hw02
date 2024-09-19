using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [Header("Sub Cube Settings")]
    public float desiredSize = 0.1f;
    public GameObject fullCube;
    public GameObject fragmentParent;
    public Material subCubeMaterial;

    [Header("Movement Settings")]
    public Transform playerTransform;
    public float maxSpeed = 10f; 
    public float minSpeed = 1f; 
    public float moveSpeedMultiplier = 1f; 

    private List<GameObject> subCubes = new List<GameObject>();
    bool subCubesActivated = false;

    void Start()
    {
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
        Vector3 fullCubeSize = fullCube.transform.localScale;
        int xCount = Mathf.CeilToInt(fullCubeSize.x / desiredSize);
        int yCount = Mathf.CeilToInt(fullCubeSize.y / desiredSize);
        int zCount = Mathf.CeilToInt(fullCubeSize.z / desiredSize);

        Vector3 actualSubCubeSize = new Vector3(
            fullCubeSize.x / xCount,
            fullCubeSize.y / yCount,
            fullCubeSize.z / zCount
        );

        Vector3 startPos = fullCube.transform.position - fullCubeSize / 2 + actualSubCubeSize / 2;

        for (int x = 0; x < xCount; x++)
        {
            for (int y = 0; y < yCount; y++)
            {
                for (int z = 0; z < zCount; z++)
                {
                    Vector3 pos = startPos + new Vector3(
                        x * actualSubCubeSize.x,
                        y * actualSubCubeSize.y,
                        z * actualSubCubeSize.z
                    );
                    GameObject subCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    subCube.transform.position = pos;
                    subCube.transform.localScale = actualSubCubeSize;
                    subCube.transform.parent = fragmentParent.transform;

                    subCube.tag = "SubCubeTag";
                    if (subCubeMaterial != null)
                    {
                        Renderer renderer = subCube.GetComponent<Renderer>();
                        renderer.material = subCubeMaterial;
                    }

                    Rigidbody rb = subCube.AddComponent<Rigidbody>();
                    rb.useGravity = false;
                    subCube.SetActive(false);

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

        AddPushForce(playerTransform.position);
    }

    public void AddPushForce(Vector3 FromPosition)
    {
        foreach (GameObject subCube in subCubes)
        {
            Rigidbody rb = subCube.GetComponent<Rigidbody>();
            Vector3 directionFrom = (subCube.transform.position - FromPosition).normalized;
            float distanceFrom = Vector3.Distance(FromPosition, subCube.transform.position);
            float speed = Mathf.Lerp(maxSpeed, minSpeed, distanceFrom / 10f) * moveSpeedMultiplier; 

            rb.velocity = directionFrom * speed;
        }
    }
}

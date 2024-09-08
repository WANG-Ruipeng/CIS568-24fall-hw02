using UnityEngine;
using TMPro;

public class SubCubeDetectorUI : MonoBehaviour
{
    public TMP_Text cubeCountText; 

    public void UpdateCubeCountUI(int subCubeCount)
    {
        cubeCountText.text = "Cubes: " + subCubeCount;
    }
}

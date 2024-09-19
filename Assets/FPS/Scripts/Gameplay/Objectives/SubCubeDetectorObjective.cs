using Unity.FPS.Game;
using UnityEngine;

public class SubCubeDetectorObjective : Objective
{
    public SubCubeDetector subCubeDetector;

    protected override void Start()
    {
        base.Start();

        Title = "Cube Detection Objective";
        //Description = "Put" + subCubeDetector.lowerBound + "~" + subCubeDetector.upperBound + "Blocks in the detection area";
        IsOptional = false;

        SubCubeDetector.OnTimerComplete += OnDetectionTimerComplete;
    }

    private void OnDetectionTimerComplete(int cubeCount)
    {
        string descriptionText = "Timer Completed";
        string counterText = $"Cubes in range: {cubeCount}";
        string notificationText = "Inbound timer objective completed!";

        CompleteObjective(descriptionText, counterText, notificationText);
    }

    private void OnDestroy()
    {
        SubCubeDetector.OnTimerComplete -= OnDetectionTimerComplete;
    }
}
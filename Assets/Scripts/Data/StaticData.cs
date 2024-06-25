using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    [Header("Car Settings")] 
    public GameObject playerPrefab;
    public float motorPower;
    public float brakePower;
    public AnimationCurve steeringCurve;

    [Header("Camera Settings")] 
    public Vector3 cameraOffset = new Vector3(0, 3f, 0);
    public float cameraDistance = -3f;
    public float cameraSmooth = 3;
}
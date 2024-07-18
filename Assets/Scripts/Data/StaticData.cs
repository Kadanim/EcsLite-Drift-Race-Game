using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    [Header("Car Settings")] 
    public GameObject PlayerPrefab;
    public float MotorPower;
    public float BrakePower;
    public AnimationCurve SteeringCurve;

    [Header("Camera Settings")] 
    public Vector3 CameraOffset = new Vector3(0, 3f, 0);
    public float CameraDistance = -3f;
    public float CameraSmooth = 3;
}
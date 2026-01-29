using UnityEngine;



public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Speed of rotation in degrees per second")]
    public float rotationSpeed = 50f;

    [Header("Rotation Axis")]
    [Tooltip("Check which axis/axes to rotate around")]
    public bool rotateX = false;
    public bool rotateY = true;
    public bool rotateZ = false;

    void Start()
    {
        Time.timeScale = 1;   
    }

    void Update()
    {
        // Calculate rotation for each axis
        float xRotation = rotateX ? rotationSpeed * Time.deltaTime : 0f;
        float yRotation = rotateY ? rotationSpeed * Time.deltaTime : 0f;
        float zRotation = rotateZ ? rotationSpeed * Time.deltaTime : 0f;

        // Apply rotation
        transform.Rotate(xRotation, yRotation, zRotation);
    }

}
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    void Update()
    {
        if (!target) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, target.position.x, smoothSpeed * Time.deltaTime);
        pos.y = Mathf.Lerp(pos.y, target.position.y, smoothSpeed * Time.deltaTime);
        transform.position = pos;
    }
}

using UnityEngine;

[System.Serializable]
public struct MapBounds
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.4f;
    public MapBounds mapBounds;

    private float _offsetZ;
    private Vector3 _currentVelocity;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned for CameraFollow script.");
            return;
        }

        _offsetZ = transform.position.z - target.position.z;

    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector3 targetPosition = target.position + Vector3.forward * _offsetZ;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        newPosition.x = Mathf.Clamp(newPosition.x, mapBounds.minX, mapBounds.maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBounds.minY, mapBounds.maxY);

        transform.position = newPosition;
    }
}

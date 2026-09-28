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
    public MapBounds mapBounds; // edges of the MAP (not the camera centre)

    private Camera _cam;
    private float _z;
    private Vector3 _velocity;

    private void Start()
    {
        _cam = GetComponent<Camera>();
        _z = transform.position.z;

        if (target == null)
        {
            Debug.LogError("Target is not assigned for CameraFollow script.");
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Half of what the camera can see (orthographic camera)
        float halfH = _cam.orthographicSize;
        float halfW = halfH * _cam.aspect;

        // Clamp the GOAL, not the smoothed result, so velocity doesn't build up at the edges
        float x = Mathf.Clamp(target.position.x, mapBounds.minX + halfW, mapBounds.maxX - halfW);
        float y = Mathf.Clamp(target.position.y, mapBounds.minY + halfH, mapBounds.maxY - halfH);

        Vector3 goal = new Vector3(x, y, _z);
        transform.position = Vector3.SmoothDamp(transform.position, goal, ref _velocity, smoothTime);
    }
}

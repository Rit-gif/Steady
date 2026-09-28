using UnityEngine;

public class teleport_toLeft : MonoBehaviour
{
    public Transform teleportPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = teleportPoint.position;
        }
    }
}

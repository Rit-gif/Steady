using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Transform destination;

    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport)
            return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    private IEnumerator Teleport(Transform player)
    {
        canTeleport = false;

        player.position = destination.position;

        yield return new WaitForSeconds(0.5f);

        canTeleport = true;
    }
}

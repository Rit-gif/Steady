using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Cant find enemy spawn points, please check it!");
            return;
        }
    }
}

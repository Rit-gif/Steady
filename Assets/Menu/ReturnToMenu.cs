using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField] string menuSceneName = "Menu";
    [SerializeField] float delay = 4f;   // set this to your animation's length

    void Start()
    {
        Time.timeScale = 1f;   // in case the game was paused
        Invoke(nameof(LoadMenu), delay);
    }

    void Update()
    {
        // Optional: let the player skip with any key or click
        if (Input.anyKeyDown)
        {
            CancelInvoke();
            LoadMenu();
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
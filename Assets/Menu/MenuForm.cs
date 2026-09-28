using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuForm : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;

    private Toggle highlighted;

    private void Start()
    {
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            SetLabelColor(toggle, Color.white);
            toggle.onValueChanged.AddListener(isOn => OnToggleValueChanged(toggle, isOn));
        }

        var first = toggleGroup.GetFirstActiveToggle();
        if (first != null)
            Highlight(first);
    }

    private void OnToggleValueChanged(Toggle toggle, bool isOn)
    {
        if (!isOn)
        {
            // Toggle was switched off because another one was selected
            SetLabelColor(toggle, Color.white);
            if (highlighted == toggle) highlighted = null;
            return;
        }

        if (toggle == highlighted)
            Confirm(toggle);      // clicked the already-highlighted item
        else
            Highlight(toggle);    // first click just highlights
    }

    private void Highlight(Toggle toggle)
    {
        highlighted = toggle;
        SetLabelColor(toggle, Color.yellow);
    }

    private void Confirm(Toggle toggle)
    {
        switch (toggle.name)
        {
            case "GameStart":
                Time.timeScale = 1f;   // in case you arrive here from a paused/game-over state
                SceneManager.LoadScene(1);
                break;

            case "Exit":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                break;

            default:
                Debug.LogError($"MenuForm: unknown toggle '{toggle.name}'");
                break;
        }
    }

    private static void SetLabelColor(Toggle toggle, Color color)
    {
        var label = toggle.transform.Find("Label");
        if (label != null && label.TryGetComponent(out TMP_Text text))
            text.color = color;
    }
}
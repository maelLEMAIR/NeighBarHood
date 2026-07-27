using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;


    [Header("UI")]
    public GameObject Root;
    public TMP_Text PromptText;
    public Image ProgressBar;
    
    private void Awake()
    {
        Instance = this;
        Debug.Log(InteractionUI.Instance);
        Hide();
    }

    public void Show(string text)
    {
        Root.SetActive(true);

        PromptText.text = text;

        ProgressBar.fillAmount = 0;
    }

    public void SetProgress(float progress)
    {
        ProgressBar.fillAmount = progress;
    }

    public void Hide()
    {
        Root.SetActive(false);
    }
}
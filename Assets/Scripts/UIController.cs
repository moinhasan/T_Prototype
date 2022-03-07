using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text GameOverScoreText;
    public GameObject GameOverPanel;

    public static UIController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        GameOverPanel.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}

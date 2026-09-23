using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;

    private void Start() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scenes.MainMenu);
        });
        resumeButton.onClick.AddListener(() => {
            Time.timeScale = 1.0f;
            Hide();
        });
        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
        });

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);

    }
}

using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    

    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    private void Awake() {
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scenes.GameScene);
        });
        quitButton.onClick.AddListener(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        });
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Transform pressKeyToRebindUI;
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private  Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button alternateInteractButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI alternateInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;

    public Action OnCloseButtonAction;


    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => {
            OnCloseButtonAction();
            Hide();
        });

        moveUpButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Move_Down);
        });
        moveRightButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Move_Right);
        });
        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Move_Left);
        });
        interactButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Interact);
        });
        alternateInteractButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Alternate_Interact);
        });
        pauseButton.onClick.AddListener(() => {
            RebindBinding(InputManager.Binding.Pause);
        });
    }
    private void Start() {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        UpdateVisual();

        Hide();
        HidePressKeyToRebindUI();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = $"SOUND EFFECTS: {Mathf.Round(SoundManager.Instance.GetVolume() * 10)}";
        musicText.text = $"MUSIC: {Mathf.Round(MusicManager.Instance.GetVolume() * 10)}";

        moveUpText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Up);
        moveDownText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Down);
        moveRightText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Right);
        moveLeftText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Left);
        interactText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);
        alternateInteractText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Alternate_Interact);
        pauseText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Pause);
    }

    public void Show(Action OnCloseButtonAction) {
        this.OnCloseButtonAction = OnCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
    public void ShowPressKeyToRebindUI() {
        pressKeyToRebindUI.gameObject.SetActive(true);
    }
    public void HidePressKeyToRebindUI() {
        pressKeyToRebindUI.gameObject.SetActive(false);
    }

    private void RebindBinding(InputManager.Binding binding) {
        ShowPressKeyToRebindUI();
        InputManager.Instance.RebindBinding(binding, () => {
            HidePressKeyToRebindUI();
            UpdateVisual();
        });
    }
}

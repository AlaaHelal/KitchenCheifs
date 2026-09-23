using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public static GameManager Instance;
    public enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 60f;
    private bool isGamePaused = false;



    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start() {
        InputManager.Instance.OnPauseAction += InputManager_OnPauseAction;
    }

    private void InputManager_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
             
                break;
        }
    }

    public bool IsGamePlayingState() {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartState() {
        return state == State.CountdownToStart;
    }
    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }
    public bool IsGameOverState() {
        return state == State.GameOver;
    }
    public float GetGamePlayingTimer() {
        return 1- (gamePlayingTimer/gamePlayingTimerMax);
    }

    private void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);

        }
    }
}
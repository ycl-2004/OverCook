using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}

    public event EventHandler OnStateChangedd;

    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;
    private enum State{
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    //[SerializeField] private float waitingToStartTimer = 1f;
    [SerializeField] private float countdDownTimer = 3f;
    [SerializeField] private float gamePlayingTimer = 10f;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePause = false;

    private void Awake(){
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        gamePlayingTimerMax = gamePlayingTimer;
        GameInput.Instance.OnPauseAction += Pause_action;
        GameInput.Instance.OnInteractAction += Instance_SkipTutorial;
    }

    private void Pause_action(object sender, EventArgs e){
        PauseGame();
    }
    private void Update() {
        switch(state){
            case State.WaitingToStart:
                break;
            case State.CountDownToStart:
                countdDownTimer -= Time.deltaTime;
                if(countdDownTimer<0f){
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                     OnStateChangedd?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer<0f){
                    state = State.GameOver;
                    OnStateChangedd?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        //Debug.Log(state);
    }

    public bool GamePlaying(){
        return state == State.GamePlaying;
    }

    public bool isCountDownToStart(){
        return state == State.CountDownToStart;
    }

    public bool isGameOver(){
        return state == State.GameOver;
    }
    public float GetCountDownTime(){
        return countdDownTimer;
    }

    public float GetGameTimeNormalized(){
        return 1-(gamePlayingTimer/gamePlayingTimerMax);
    }

    public void PauseGame(){
        isGamePause = !isGamePause;

        if(isGamePause){
            OnGamePause?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 0;
        }else{
            OnGameUnpause?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 1;
        }
    }

    private void Instance_SkipTutorial(object sender, System.EventArgs e){
        if(state == State.WaitingToStart){
            state = State.CountDownToStart;
            OnStateChangedd?.Invoke(this,EventArgs.Empty);
        }
    }

    public void AddtoGameTimer(float addedTime){
        gamePlayingTimer+=addedTime;
    }

    public float GetMaxPlayingTime(){
        return gamePlayingTimerMax;
    }
}

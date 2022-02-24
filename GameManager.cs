using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public Text score;
    public Text Tball;
    public Text Tlevel;
    public Text Thighscore;

    public GameObject Menu;
    public GameObject Play;
    public GameObject Level_Completed;
    public GameObject Game_Over;

    public GameObject[] levels;

    public static GameManager Instance { get; private set; }
    public enum State { MENU,INIT,PLAY,LEVELCOMPLETED,LOADLEVEL,GAMEOVER}
    State _state;
    GameObject _presentball;
    GameObject _presentlevel;
    bool _is_Switching_State;

    private int _score_count;

    public int Score
    {
        get { return _score_count; }
        set { _score_count = value;
            score.text= "SCORE:" + _score_count;
        }
       
    }

    private int _level_of_game;

    public int Level
    {
        get { return _level_of_game; }
        set { _level_of_game = value;

            Tlevel.text = "LEVEL:" + _level_of_game;
        }

    }

    private int _set_of_balls;

    public int Balls
    {
        get { return _set_of_balls; }
        set { _set_of_balls = value;
            Tball.text = "BALLS:" + _set_of_balls;
        }
    }


    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
        PlayerPrefs.DeleteKey("highscore");
        
    }
    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
       
    }
    IEnumerator SwitchDelay(State newState, float delay)
    {
        _is_Switching_State = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _is_Switching_State = false;
    }

    void BeginState(State newstate)
    {
        switch (newstate)
        {
            case State.MENU:
                Cursor.visible = true;
                Thighscore.text = "HIGHSCORE:" + PlayerPrefs.GetInt("highscore");
                Menu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                Play.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 2;
                if(_presentlevel != null)
                {
                    Destroy(_presentlevel);
                }
                Instantiate(player);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_presentball);
                Destroy(_presentlevel);
                Level++;
                Level_Completed.SetActive(true);
                SwitchState(State.LOADLEVEL,2f);
                break;
            case State.LOADLEVEL:
                if (Level>= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _presentlevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }

                break;
            case State.GAMEOVER:
                if(Score > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                Game_Over.SetActive(true);
                break;
        }

    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_presentball == null)
                {
                    if(Balls>0)
                    {
                        _presentball = Instantiate(ball);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                if(_presentlevel!= null && _presentlevel.transform.childCount==0 && !_is_Switching_State)
                {
                    SwitchState(State.LEVELCOMPLETED);
                }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if(Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }

    }
     void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                Menu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Level_Completed.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                Play.SetActive(false);
                Game_Over.SetActive(false);

                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    private GameStates currentState;

    public enum GameStates
    {
        Playing,
        Paused
    }
    private void Awake()
    {
        Instance = this;


        System.GC.Collect();
    }
    void Start()
    {
        currentState = GameStates.Playing;
        EnemyScript.PlayerDied += OnPlayerDied;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameStates.Playing:
                break;
            case GameStates.Paused:
                break;
        }
    }

    private void OnPlayerDied()
    {       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
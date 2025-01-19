using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GameStart", 0);
    }

    public void GameStart()
    {
        StressManager.Instance.GameStart();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}

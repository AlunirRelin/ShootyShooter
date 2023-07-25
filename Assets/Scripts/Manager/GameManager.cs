using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum State {Lobby, Active};
    public State CurrentState;
    public NetworkManagerFps networkManagerFps;

    public void Start()
    {
    }
    public void Update()
    {
    }
}

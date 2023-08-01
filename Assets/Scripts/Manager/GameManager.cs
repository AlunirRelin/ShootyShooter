using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : Singleton<GameManager>
{
    public NetworkManagerHUD networkManagerHUD;
    public enum State {Lobby, Active};
    public State CurrentState;
    public NetworkManagerFps networkManagerFps;

    private void Start()
    {
        networkManagerHUD = GetComponent<NetworkManagerHUD>();
    }
    void FixedUpdate()
    {
        if (!isOwned) { return; }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            networkManagerHUD.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            networkManagerHUD.enabled = true;
        }
    }
}

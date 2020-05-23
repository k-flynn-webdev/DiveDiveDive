using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerControls : MonoBehaviour
{

    private Player _player;

    // Start is called before the first frame update
    private void GetPlayer()
    {
        _player = ServiceLocator.Resolve<Player>();
    }

    public void Left()
    {
        if(_player == null)
        {
            GetPlayer();
        }

        _player.Left();
    }

    public void Right()
    {
        if (_player == null)
        {
            GetPlayer();
        }

        _player.Right();
    }

    public void Jump()
    {
        if (_player == null)
        {
            GetPlayer();
        }

        _player.Jump();
    }

    public void Down()
    {
        if (_player == null)
        {
            GetPlayer();
        }

        _player.Down();
    }
}

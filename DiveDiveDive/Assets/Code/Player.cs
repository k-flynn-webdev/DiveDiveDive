using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IActive, INotActive
{
    private CharMove _charMove;
    private CharacterMove _characterMove;


    void Awake()
    {
        _charMove = GetComponent<CharMove>();
        _characterMove = GetComponent<CharacterMove>();
    }

    public void Kill(float time)
    {
        StartCoroutine(OnDeath(time));
    }

    public void Force(float force, Vector3 point)
    {
        if (_characterMove != null)
        {
            _characterMove.Force(force, point);
        }
    }

    private IEnumerator OnDeath(float time)
    {
        ServiceLocator.Resolve<GameEvent>().NewEvent(new GameEventObj("Died", null));

        yield return new WaitForSeconds(time);

        ServiceLocator.Resolve<GameState>().SetStateOver();
    }

    public void Active()
    {
        ServiceLocator.Register<Player>(this);
    }

    public void NotActive()
    {
        ServiceLocator.UnRegister<Player>();
    }


    public void Left()
    {
        _charMove.Left();
    }

    public void Right()
    {
        _charMove.Right();
    }

    public void Jump()
    {
        _charMove.Jump();
    }

    public void Down()
    {
        _charMove.Down();
    }
}

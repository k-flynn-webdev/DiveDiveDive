using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level: MonoBehaviour
{
    public string ID { get { return this._id; } }
    public bool Ready { get { return this._ready; } }
    public Vector3 Progress { get { return this._progress; } }

    [SerializeField]
    private string _id;
    [SerializeField]
    private Vector3 _progress;
    [SerializeField]
    private bool _loaded = false;
    [SerializeField]
    private bool _ready = false;

    private IReset[] _itemsToReset;

    private void Awake()
    {
        if (!_loaded)
        {
            _itemsToReset = GetComponentsInChildren<IReset>();
            _loaded = true;
        }
    }

    public void SetProgress(Vector3 progress)
    {
        _progress = progress;
    }

    private void SetReady(bool isReady)
    {
        if (!_loaded)
        {
            _ready = false;
            return;
        }

        _ready = isReady;
    }

    public virtual void Load()
    {
        Awake();
        Reset();

        // todo do things here

        ServiceLocator.Resolve<LevelManager>().LevelIsLoaded();
    }

    public virtual void Reset()
    {
        if (_itemsToReset.Length > 0)
        {
            for (int i = 0, max = _itemsToReset.Length; i < max; i++)
            {
                _itemsToReset[i].Reset();
            }
        }

        SetReady(true);
    }

    // kills self and removes GO
    public virtual void UnLoad()
    {
        _ready = false;

        // do things??


        //Destroy(this.gameObject, 5f);
    }

    public virtual void PlayPre(GameStateObj state)
    {
        if (state.last != GameStateObj.gameStates.Pause)
        {
            Reset();
        }
    }

    public virtual void Play() { }

    public virtual void PlayPost() { }

    public virtual void PausePre() { }

    public virtual void Pause() { }

    public virtual void PausePost() { }

    public virtual void OverPre() { }

    public virtual void Over() { }

    public virtual void OverPost() { }

}

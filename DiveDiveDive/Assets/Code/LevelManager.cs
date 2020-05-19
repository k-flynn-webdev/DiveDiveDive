using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ISubscribeState
{
    [SerializeField]
    private Level[] Titles;

    [SerializeField]
    private Level[] Levels;

    public bool Ready { get { return this._ready; } }

    [SerializeField]
    private bool _ready = false;
    [SerializeField]
    private Level _currentLvl = null;
    private ObjectPoolItem _currentObj = null;

    [SerializeField]
    public GameStateObj State { get; set; }

    private float _loadWaitTime = 1f;

    void Awake()
    {
        ServiceLocator.Register<LevelManager>(this);
    }

    void Start()
    {
        ServiceLocator.Resolve<GameState>().SubscribeState(this);
        StartCoroutine(LoadState());
    }

    // staggered init load ..
    private IEnumerator LoadState()
    {
        yield return new WaitForSeconds(0.1f);
        ServiceLocator.Resolve<GameState>().SetStateLoad();
        StartCoroutine(LoadLevelDelay());
    }

    // staggered first load
    private IEnumerator LoadLevelDelay()
    {
        yield return new WaitForSeconds(_loadWaitTime);
        ServiceLocator.Resolve<GameState>().SetStateTitle();
    }

    public Vector3 GetProgress()
    {
        return _currentLvl != null ? _currentLvl.Progress : Vector3.zero; 
    }

    public void LevelIsLoaded()
    {
        _ready = true;
    }

    void Update()
    {
        if (!_ready)
        {
            return;
        }

        if (State.state == GameStateObj.gameStates.Title ||
            State.state == GameStateObj.gameStates.Settings ||
            State.state == GameStateObj.gameStates.Play)
        {
            _currentLvl.Play();
        }

        if (State.state == GameStateObj.gameStates.Pause)
        {
            _currentLvl.Pause();
        }

        if (State.state == GameStateObj.gameStates.Over)
        {
            _currentLvl.Over();
        }
    }

    private string GetLevelOrTitleByID(bool isTitle, string level)
    {

        Level[] _items = isTitle ? Titles : Levels;

        if (_items.Length == 0 || level == null)
        {
            Debug.Log(_items.Length);
            Debug.Log(_items[0].name);
            Debug.Log(level);
            throw new System.Exception("No levels/titles to load");
        }

        for (int i = 0, max = _items.Length; i < max; i++)
        {
            if (_items[i].ID.Contains(level))
            {
                return _items[i].name;
            }
        }

        throw new System.Exception("No levels/titles match");
    }

    private void LoadTitleOrLevel(bool isTitle, string levelId = "01")
    {
        _ready = false;

        string levelToLoad = GetLevelOrTitleByID(isTitle, levelId);

        if (_currentLvl != null)
        {
            _currentLvl.UnLoad();
            _currentObj.SetItemNotActive();
        }

        _currentObj = ServiceLocator.Resolve<ObjectPoolManager>().GetItem(levelToLoad, true);
        _currentLvl = _currentObj.GetComponent<Level>();
        _currentLvl.Load();
    }


    public void ReactState(GameStateObj state)
    {
        State = state;

        // first load
        if (state.state == GameStateObj.gameStates.Title &&
            state.last != GameStateObj.gameStates.Settings)
        {
            LoadTitleOrLevel(true, "01");
            return;
        }

        // load actual play level
        if (state.state == GameStateObj.gameStates.Play &&
            state.last == GameStateObj.gameStates.Title)
        {
            LoadTitleOrLevel(false, "01");
            return;
        }

        // cleanup previous mode
        switch (state.last)
        {
            case GameStateObj.gameStates.Title:
                _currentLvl.PlayPost();
                break;
            case GameStateObj.gameStates.Play:
                _currentLvl.PlayPost();
                break;
            case GameStateObj.gameStates.Pause:
                _currentLvl.PausePost();
                break;
            case GameStateObj.gameStates.Over:
                _currentLvl.OverPost();
                break;
        }

        // set new mode
        switch (state.state)
        {
            case GameStateObj.gameStates.Title:
                _currentLvl.PlayPre(state);
                break;
            case GameStateObj.gameStates.Play:
                _currentLvl.PlayPre(state);
                break;
            case GameStateObj.gameStates.Pause:
                _currentLvl.PausePre();
                break;
            case GameStateObj.gameStates.Over:
                _currentLvl.OverPre();
                break;
        }
    }
}
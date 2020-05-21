using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01 : Level
{
    [SerializeField]
    private TileConfigObj _lvlConfig;
    private TileConfigObj _config;

    private int _lastRender;

    [SerializeField]
    public Vector3 _playerStartPos;
    [SerializeField]
    public Vector3 _playerCamOffset;

    private Player _player;
    private ObjectPoolItem _playerObj;


    public override void Load()
    {
        _config = new TileConfigObj(
            _lvlConfig._tileSize,
            _lvlConfig._renderSize,
            _lvlConfig._offsetPost,
            _lvlConfig._itemConfig,
            _lvlConfig._renderTexture,
            _lvlConfig._offsetPre);
        ServiceLocator.Resolve<TileRender>().SetupRender(_config);

        SetupPlayer();

        ServiceLocator.Resolve<CameraControl>().SetTarget(
            _playerObj.transform,
            new Vector3(1f,1f,0f),
            _playerCamOffset);

        base.Load();
    }

    public override void Reset()
    {
        SetProgress(_playerStartPos);

        base.Reset();
    }

    private void RenderLevel()
    {
        int posHash = ServiceLocator.Resolve<TileRender>().PositionToHash(Progress);

        if (posHash == _lastRender)
        {
            return;
        }

        _lastRender = ServiceLocator.Resolve<TileRender>().Render(Progress);
    }


    private void SetupPlayer()
    {
        if (_playerObj == null)
        {
            _playerObj = ServiceLocator.Resolve<ObjectPoolManager>().GetItem("Player", false, _playerStartPos);
            _player = _playerObj.GetComponent<Player>();
        }

        _playerObj.Reset();
        _playerObj.SetItemActive();
        _playerObj.transform.position = _playerStartPos;
    }

    public override void PlayPre(GameStateObj state)
    {
        RenderLevel();

        base.PlayPre(state);
    }

    public override void Play()
    {
        if (_playerObj != null)
        {
            SetProgress(_playerObj.transform.position);
        }

        RenderLevel();

        base.Play();
    }

    public override void UnLoad()
    {
        //ServiceLocator.Resolve<TileRender>().Render(Progress);

        _playerObj.SetItemNotActive();

        base.UnLoad();
    }
}

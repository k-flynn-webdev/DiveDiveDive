using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_01 : Level
{
    [SerializeField]
    private TileConfigObj _lvlConfig;
    private TileConfigObj _config;

    private int _lastRender;

    [SerializeField]
    public Vector3 _shipStartPos = new Vector3(5f,30f,0f);

    public override void Load()
    {
        _config = new TileConfigObj(
            _lvlConfig._tileSize,
            _lvlConfig._renderSize,
            _lvlConfig._offset,
            _lvlConfig._itemConfig,
            _lvlConfig._renderTexture);
        ServiceLocator.Resolve<TileRender>().SetupRender(_config);

        SetupShip();

        ServiceLocator.Resolve<CameraControl>().SetTarget(_shipObj.transform);

        base.Load();
    }

    public override void Reset()
    {
        SetProgress(Vector3.zero);

        base.Reset();
    }

    private void RenderLevel()
    {
        _lastRender = ServiceLocator.Resolve<TileRender>().Render(Progress);
    }

    private ObjectPoolItem _shipObj;

    private void SetupShip()
    {
        _shipObj = ServiceLocator.Resolve<TileRender>().GetItem("Ship", _shipStartPos);
        _shipObj.transform.position = _shipStartPos;
    }

    public override void PlayPre(GameStateObj state)
    {
        RenderLevel();

        base.PlayPre(state);
    }

    public override void UnLoad()
    {
        base.UnLoad();
    }
}

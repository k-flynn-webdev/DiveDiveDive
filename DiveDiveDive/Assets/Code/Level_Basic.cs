using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Basic : Level
{
    [SerializeField]
    private TileConfigObj _lvlConfig;
    private TileConfigObj _config;

    private int _lastRender;

    public override void Load()
    {
        _config = new TileConfigObj(
            _lvlConfig._tileSize,
            _lvlConfig._renderSize,
            _lvlConfig._offsetPre,
            _lvlConfig._offsetPost,
            _lvlConfig._itemConfig,
            _lvlConfig._renderTexture,
            _lvlConfig._renderTexturePivot);
        ServiceLocator.Resolve<TileRender>().SetupRender(_config);

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

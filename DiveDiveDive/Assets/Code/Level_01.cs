using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01 : Level
{
    [SerializeField]
    private TileConfigObj _lvlConfig;
    private TileConfigObj _config;

    private int _lastRender;

    public Transform _follow;

    public override void Load()
    {
        //_config = new TileConfigObj(
        //    _lvlConfig._tileSize,
        //    _lvlConfig._renderSize,
        //    _lvlConfig._offset,
        //    _lvlConfig._itemConfig,
        //    _lvlConfig._renderTexture);
        //ServiceLocator.Resolve<TileRender>().SetupRender(_config);

        base.Load();
    }

    public override void Reset()
    {
        SetProgress(_follow.position);

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



    public override void PlayPre(GameStateObj state)
    {
        RenderLevel();

        base.PlayPre(state);
    }

    public override void Play()
    {
        SetProgress(_follow.position);
        RenderLevel();

        base.Play();
    }

    public override void UnLoad()
    {
        //ServiceLocator.Resolve<TileRender>().Render(Progress);

        base.UnLoad();
    }
}

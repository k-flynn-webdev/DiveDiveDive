using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IPublishScore
{

    [SerializeField]
    public ScoreObj Score
    { get { return this._score; } }

    private ScoreObj _score;

    public List<ISubscribeScore> ScoreSubscribers
    { get { return this._scoreSubscribers; } }

    private List<ISubscribeScore> _scoreSubscribers = new List<ISubscribeScore>();

    void Awake()
    {
        ServiceLocator.Register<ScoreManager>(this);
    }

    public void AddScore(float addScore)
    {
        SetScore(Score._valueFloat + addScore);
    }

    public void SetScore(float scoreValue)
    {
        _score = new ScoreObj(scoreValue);
        NotifyScore();
    }

    public void SetScore(string scoreText)
    {
        _score = new ScoreObj(scoreText);
        NotifyScore();
    }

    public void NotifyScore()
    {
        for (int i = ScoreSubscribers.Count - 1; i >= 0; i--)
        {
            ScoreSubscribers[i].ReactScore(Score);
        }
    }

    public void SubscribeScore(ISubscribeScore listener)
    { _scoreSubscribers.Add(listener); }


    public void UnSubscribeScore(ISubscribeScore listener)
    { _scoreSubscribers.Remove(listener); }

}

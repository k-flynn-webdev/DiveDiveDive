using System.Collections.Generic;

public interface IPublishScore
{
    List<ISubscribeScore> ScoreSubscribers { get; }

    void NotifyScore();

    void SubscribeScore(ISubscribeScore listener);
    void UnSubscribeScore(ISubscribeScore listener);
}

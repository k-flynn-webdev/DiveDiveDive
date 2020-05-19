using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameEventType
{
    public string _type;
    public object _value;
    public float _time;

    public GameEventType(string type, object value)
    {
        this._time = Time.time;
        this._type = type;
        this._value = value;
    }
}

public class GameEvent : MonoBehaviour, IPublishEvent
{

    [SerializeField]
    private GameEventType _current;
    [SerializeField]
    private GameEventType _last;

    private float _time;

    [SerializeField]
    private GameEventType[] _events = new GameEventType[10];



    void Awake()
    {
        ServiceLocator.Register<GameEvent>(this);
    }

    public void NewEvent(GameEventType eventType)
    {
        float timeNow = Time.time;
        float timeDiff = timeNow - _time;

        if (_current._type == eventType._type && timeDiff < .05f)
        {
            return;
        }

        SetEvent(eventType);
    }

    private void SetEvent(GameEventType eventType)
    {
        _last = _current;
        _current = eventType;
        _time = Time.time;

        #if UNITY_EDITOR
            UpdateHistory(eventType);
        #endif

        this.NotifyEvent();
    }

    private void UpdateHistory(GameEventType newEvent)
    {
        for (int i = _events.Length - 2; i > 0; i--)
        {
            _events[i] = _events[i - 1];
        }
        _events[0] = newEvent;
    }

    public List<ISubscribeEvent> EventSubscribers
    { get { return this._eventSubscribers; } }

    private List<ISubscribeEvent> _eventSubscribers = new List<ISubscribeEvent>();


    public void NotifyEvent()
    {
        for (int i = EventSubscribers.Count - 1; i >= 0; i--)
        {
            EventSubscribers[i].ReactEvent(_current);
        }
    }

    public void SubscribeEvent(ISubscribeEvent listener)
    { _eventSubscribers.Add(listener); }

    public void UnSubscribeEvent(ISubscribeEvent listener)
    { _eventSubscribers.Remove(listener); }

}

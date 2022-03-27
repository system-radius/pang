using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A listener component that can be added to objects.
/// Uses the game event listener concept from
/// Unite 2017 - Game Architecture with Scriptable Objects.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        if (gameEvent != null) gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null) gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}

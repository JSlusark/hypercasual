using System;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * Was thinking to introduce a base class for Prefab Controllers so that I can use the same
 * base of panelController when needed (only difference being that derivative classes will have their
 * additional props which can be inherited from their derivatives)
 *
 * Was also thinking to clean panelmanagement one last time, it's silly to have a huge list of panel controllers in the
 * hierarchy and feel confident enough in what i learned to make it better than what i have now.
 *
 */

public abstract class PrefabController<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected List<(Action onEnable, Action onDisable)> _eventList = new();

    private void Awake()
    {
        if (_instance != null)
        {
            // Debug.LogWarning($"[{typeof(T).Name}] was already instantiated");
            Destroy(gameObject); 
            return;
        }

        _instance = this as T;
        OnAwake();
    }
    protected abstract void OnAwake(); // abstract flags the compiler that it needs to be overriden in derivative classes

    protected virtual void OnDestroy()
    {
        if (_instance == null)
        {
            // Debug.LogWarning($"[{typeof(T).Name}] was is already null");
            return;
        }

        Debug.LogWarning($"[{typeof(T).Name}] was destroyed");
        
        _instance = null;
    }
    
    
    
    // un/subscribes to events when constructed in base and again in derivatives
    private void OnEnable() 
    {
        SubscribeToEvents(true);
        Enable(); // <-------------------------- quick workaround - i will change this
    }

    protected virtual void Enable() { }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }
    
    protected virtual void AddEvents() { }

    protected void RegisterEvent(string eventName, Action onEnable, Action onDisable)
    {
        Debug.Log($"[{GetType().Name}] Registered to {eventName}");
        _eventList.Add((onEnable, onDisable));
    }

    protected void SubscribeToEvents(bool isSubscribed)
    {
        Debug.Log($"[{GetType().Name}] Subscribed to {isSubscribed}");
        foreach (var entry in _eventList)
        {
            if (isSubscribed) entry.onEnable();
            else entry.onDisable();
        }
    }
}
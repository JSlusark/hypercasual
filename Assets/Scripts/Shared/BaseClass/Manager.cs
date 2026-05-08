using UnityEngine;


/*
 * Singleton for mono-behaviour manager classes.
 *
 * The instance of this class is decided from unity based on scene load order,
 * it happens inside Awake, therefore we need to put a singleton enforcement
 * check i this specific function to avoid it duplicates and that it destroys itself at
 * every log.
 * 
 * T is the type specified in the derivative class. Example:
 * SwipeManager : Manager<SwipeManager>
 * Using T will allow us to check for duplicates for the same type
 *
 *
 */
public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    // virtual allows derivatives to override the function
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this) // prevents new instances are created
        {
            // Debug.Log($"[{Instance.name}] Duplicate found, destroying. Existing ID: {Instance.GetInstanceID()}, Duplicate ID: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }


        Instance = this as T; // ensures that the created instance is of type T 
        // Debug.Log($"[{Instance.name}] Manager Awaken");
        transform.SetParent(null); // If I want to keep manager collected in a folder i need to set them as root 
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }

    private void OnDestroy()
    {
        // Debug.Log($"[{Instance.name}] Manager Destroyed");
    }

}
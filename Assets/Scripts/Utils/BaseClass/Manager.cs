using UnityEngine;


/*
 * Manager Base Class
 * T is the type specified in the derivative class. Example:
 * SwipeManager : Manager<SwipeManager>
 * 
 * Using T will allow us to check for duplicates fo the same type
 *
 */
public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    // virtual allows derivatives to override the function
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this) // prevents new instances are created
        {
            Debug.Log($"[{Instance.name}] Duplicate found, destroying. Existing ID: {Instance.GetInstanceID()}, Duplicate ID: {GetInstanceID()}");
            Destroy(gameObject);
            return;
        }


        Instance = this as T; // ensures that the created instance is of type T 
        Debug.Log($"[{Instance.name}] Manager Awaken");
        transform.SetParent(null); // If I want to keep manager collected in a folder i need to set them as root 
        DontDestroyOnLoad(gameObject);
    }
}
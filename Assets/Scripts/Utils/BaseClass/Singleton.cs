/*
 * Singleton for pure classes.
 * 
 * Singleton here is created by lazy instantiation: it gets instanced for the 1st time only when needed.
 * 
 */

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CreateInstance();
            }

            return _instance;
        }
    }

    private static T CreateInstance()
    {
        var instance = new T();
        instance.Initialize();
        return instance;
    }

    protected virtual void Initialize() { }
}
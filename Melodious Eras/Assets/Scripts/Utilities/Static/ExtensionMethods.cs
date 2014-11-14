using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{
    public static T GetIComponent<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponent(typeof(T)) as T;
    }

    public static T GetIComponentInChildren<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponentInChildren(typeof(T)) as T;
    }

    public static T GetIComponentInParent<T>(this GameObject gameObject) where T : class
    {
        return gameObject.GetComponentInParent(typeof(T)) as T;
    }

    public static T GetComponentInChildrenOnly<T>(this GameObject gameObject) where T : class
    {
        for (int c = 0; c < gameObject.transform.childCount; c++)
        {
            T comp = gameObject.transform.GetChild(c).GetComponentInChildren(typeof(T)) as T;
            if (comp != null)
                return comp;
        }
        return null;
    }
}

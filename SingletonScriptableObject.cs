using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTT
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T _INSTANCE;

        public static T GetInstance(bool debug = false)
        {
            if(_INSTANCE == null)
            {
                T[] currentAsset = Resources.LoadAll<T>("");
                if(currentAsset == null || currentAsset.Length <= 0)
                {
                    if(debug)
                    {
                        throw new System.Exception("No singleton scriptable object found in resources");
                    }
                }
                else if(currentAsset.Length > 1)
                {
                    Debug.LogWarning("Multiple singleton scriptable object found!");
                }
                _INSTANCE = currentAsset[0];
            }
            return _INSTANCE;
        }
    }
}
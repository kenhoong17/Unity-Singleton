using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTT
{
    /// <summary>
    /// classes that derived from SingletonMonoBehaviour should have their constructors delare as private constructor
    /// that is to prevent leaking of Singleton instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T>
    {
        // =================================================================================================================

        #region singleton

        private static object _PADLOCK = new object();
        private static T _INSTANCE;
        public static T GetInstance( bool createNewIfNotFound = false, bool debug = false )
        {
            lock( _PADLOCK )
            {
                if ( null == _INSTANCE )
                {
                    // Find existing instance in the scene
                    _INSTANCE = FindInstancesInScene();

                    // No instances found, create a new one
                    if ( null == _INSTANCE )
                    {
                        if ( createNewIfNotFound )
                        {
                            _INSTANCE = new GameObject( typeof( T ).Name ).AddComponent<T>();
                        }
                        else
                        {
                            if ( debug )
                            {
                                Debug.LogError( "[Singleton WARNING] SingletonMonoBehaviour of type: '" + typeof( T ).ToString() + "' has not awaken." );
                            }
                        }
                    }
                }

                FindInstancesInScene();
                return _INSTANCE;
            }
        }

        private static T FindInstancesInScene()
        {
            T[] instancesInScene = FindObjectsOfType<T>();

            if ( instancesInScene.Length > 1 )
                Debug.LogWarning( "[Singleton WARNING] More than 1 SingletonMonoBehaviour of type : '" + typeof( T ).ToString() + "' in the scene." );

            if ( instancesInScene.Length != 0 )
                return instancesInScene[0];

            return null;
        }

        #endregion singleton

        // =================================================================================================================

        #region monobehaviour

        protected virtual void OnApplicationPause( bool isPaused ) { }
        protected virtual void OnApplicationFocus( bool hasFocus ) { }

        protected virtual void Awake() { }
        protected virtual void OnEnable() { }
        protected virtual void Reset() { }
        protected virtual void Start() { }

        protected virtual void FixedUpdate() { }
        protected virtual void Update() { }
        protected virtual void LateUpdate() { }

        protected virtual void OnPreCull() { }
        protected virtual void OnPreRender() { }
        protected virtual void OnPostRender() { }
        protected virtual void OnDrawGizmos() { }
        protected virtual void OnGUI() { }

        protected virtual void OnApplicationQuit() { }

        protected virtual void OnDisable() { }
        protected virtual void OnDestroy()
        {
            _INSTANCE = null;
        }

        #endregion monobehaviour

        // =================================================================================================================

    }
}
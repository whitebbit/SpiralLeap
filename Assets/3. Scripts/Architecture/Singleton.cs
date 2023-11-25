using UnityEngine;

namespace _3._Scripts.Architecture
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    //GameObject singletonObject = new GameObject(typeof(T).Name);
                    //_instance = singletonObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                //Destroy(_instance.gameObject);
            }
        }
    }
}

using UnityEngine;

namespace OldMeshes
{
    public class YValue : MonoBehaviour
    {
        public float yValue;
        public static YValue Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
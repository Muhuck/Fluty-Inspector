#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Fluty.Editor
{
    /// <summary>
    /// Global Fluty Inspector for all MonoBehaviours.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class FlutyMonoEditor : FlutyEditor<MonoBehaviour>
    {
    }
}
#endif

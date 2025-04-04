using UnityEngine;

namespace Modules.Data
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(DataConfigSo), fileName = nameof(DataConfigSo))]
    public class DataConfigSo : ScriptableObject
    {
        [field: SerializeField] public JsonType JsonType { get; private set; } = JsonType.Unity;
        [field: SerializeField] public bool ConvertToByteCode { get; private set; } = true;
        [field: SerializeField] public bool LogSaveEvent { get; private set; } = true;
    }
}
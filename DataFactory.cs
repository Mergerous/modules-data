using System.Reflection;
using JetBrains.Annotations;

namespace Modules.Data
{
    [UsedImplicitly]
    public sealed class DataFactory
    {
        public DataFactory(DataManager dataManager, params IDataHandler[] handlers)
        {
            foreach (IDataHandler handler in handlers)
            {
                DataAttribute attribute = handler.GetType().GetCustomAttribute<DataAttribute>();

                if (attribute != null)
                {
                    object data = dataManager.Load(attribute.Key, attribute.Type, default);
                    handler.OnLoaded(new DataHandle(attribute.Key, data, dataManager.Save));
                }
            }
        }
    }
}

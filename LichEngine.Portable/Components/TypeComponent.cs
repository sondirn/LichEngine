using Nez;

namespace LichEngine.Portable.Components
{
    public class TypeComponent : Component
    {
        public EntityType EntityType;

        public TypeComponent()
        {
        }

        public TypeComponent(EntityType type)
        {
            EntityType = type;
        }

        public void SetType(EntityType type)
        {
            EntityType = type;
        }
    }
}
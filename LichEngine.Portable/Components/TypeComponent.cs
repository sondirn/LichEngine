using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public enum EntityType
    {
        STATICOBJECT,
        ENEMY,
        PROJECTILE,
        RAYCAST,
        SWING_COLLIDER,
        PLAYER
    }
}

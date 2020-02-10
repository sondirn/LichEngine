using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;
using System;

namespace LichEngine.Portable.Components
{
    public class StaticObject : Component, ITriggerListener, IUpdatable
    {
        public SpriteRenderer Sprite;
        public BoxCollider Collider;
        public ProjectileMover _mover;
        public TypeComponent Type;
        public CollisionListener CollisionListener;
        public TmxObject Source;

        public StaticObject(TmxObject obj)
        {
            Source = obj;
        }

        public override void OnAddedToEntity()
        {
            //Set type
            Type = Entity.AddComponent<TypeComponent>();
            Type.SetType(EntityType.STATIC_OBJECT);
            //Set up texture
            var tex = Entity.Scene.Content.Load<Texture2D>(Source.Properties["SourceTex"]);
            Sprite = new SpriteRenderer(tex);
            Entity.AddComponent(Sprite);
            //Set up collision
            Collider = Entity.AddComponent<BoxCollider>();
            Flags.SetFlagExclusive(ref Collider.CollidesWithLayers, 1);
            Flags.SetFlagExclusive(ref Collider.PhysicsLayer, 1);
            CollisionListener = Entity.AddComponent<CollisionListener>();
            base.OnAddedToEntity();
        }

        public void OnTriggerEnter(Collider other, Collider local)
        {
            if (other.GetComponent<TypeComponent>().EntityType == EntityType.SWING_COLLIDER)
                Entity.Destroy();
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            throw new NotImplementedException();
        }


        public void Update()
        {
           
        }
    }
}
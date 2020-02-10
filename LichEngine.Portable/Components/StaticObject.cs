using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.Components
{
    public class StaticObject : Component, ITriggerListener, IUpdatable
    {

        public SpriteRenderer Sprite;
        public BoxCollider Collider;
        public ProjectileMover _mover;
        public TypeComponent Type;

        public override void OnAddedToEntity()
        {
            Type = Entity.AddComponent<TypeComponent>();
            Type.SetType(EntityType.STATICOBJECT);
            var tex = Entity.Scene.Content.Load<Texture2D>(Content.Textures.DefaultTexture);
            Sprite = new SpriteRenderer(tex);
            Entity.AddComponent(Sprite);
            Collider = Entity.AddComponent<BoxCollider>();
            Flags.SetFlagExclusive(ref Collider.CollidesWithLayers, 1);
            Flags.SetFlagExclusive(ref Collider.PhysicsLayer, 1);
            _mover = Entity.AddComponent(new ProjectileMover());
            base.OnAddedToEntity();
        }

        public void OnTriggerEnter(Collider other, Collider local)
        {
            //throw new NotImplementedException();
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            _mover.Move(Vector2.Zero);
        }
    }
}

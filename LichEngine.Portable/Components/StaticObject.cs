using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LichEngine.Portable.Components
{
    public class StaticObject : Component, ITriggerListener
    {

        public SpriteRenderer Sprite;
        public BoxCollider Collider;

        public void OnTriggerEnter(Collider other, Collider local)
        {
            throw new NotImplementedException();
        }

        public void OnTriggerExit(Collider other, Collider local)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Nez
{
    /// <summary>
    /// Notifies parent entity if any collisions have taken place.
    /// </summary>
    public class CollisionListener : Component, IUpdatable
    {
        Collider _collider;
        List<ITriggerListener> _tempTriggerList = new List<ITriggerListener>();

        public override void OnAddedToEntity()
        {
            _collider = Entity.GetComponent<Collider>();
            Debug.WarnIf(_collider == null, "Collision Listener has no collider, CollisionListener requires a Collider to work!");
            base.OnAddedToEntity();
        }

        public void Update()
        {
            if (_collider == null)
                return;
            //fetch anything that we might collide with us
            var neighbors = Physics.BoxcastBroadphaseExcludingSelf(_collider, _collider.CollidesWithLayers);

            foreach (var neighbor in neighbors)
            {
                if (_collider.Overlaps(neighbor))
                {
                    NotifyListeners(_collider, neighbor);
                }
            }


        }

        void NotifyListeners(Collider self, Collider other)
        {
            Entity.GetComponents(_tempTriggerList);
            for (int i = 0; i < _tempTriggerList.Count; i++)
            {
                _tempTriggerList[i].OnTriggerEnter(other, self);
            }
            _tempTriggerList.Clear();
        }
    }
}

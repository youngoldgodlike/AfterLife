using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GameObjectExtancions 
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)
        {
            return layer == go.layer;
        }

        public static bool IsInLayer(this GameObject go, string layer) {
            return LayerMask.NameToLayer(layer) == go.layer;
        }
    }
}

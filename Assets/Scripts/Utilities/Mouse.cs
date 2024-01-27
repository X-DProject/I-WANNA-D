using UnityEngine;

namespace Game.Util
{
    public static class Mouse
    {
        public static Vector3 ScreenToWorldPosition
        {
            get
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                    mousePos.x, 
                    mousePos.y, 
                    -Camera.main.transform.position.z));
                return screenToWorld;
            }
        }
    }
}

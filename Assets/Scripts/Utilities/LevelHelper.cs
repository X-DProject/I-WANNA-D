using UnityEngine;

namespace Game.Util
{
    public sealed class LevelHelper : MonoBehaviour
    {
        public void Win()
        {
            GameInstance.Signal("next_level");
        }
    }
}

using UnityEngine;

namespace Game.Util
{
    public sealed class LevelHelper : MonoBehaviour
    {
        public void Win()
        {
            GameInstance.Signal("next_level");
            Debug.Log("level: winned");
        }

        public void Fail()
        {          
            GameInstance.Signal("game.over");
            Debug.Log("level: failed");
        }
    }
}

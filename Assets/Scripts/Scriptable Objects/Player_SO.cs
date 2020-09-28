using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
    public class Player_SO : ScriptableObject
    {
        public int health;
        public float speed;
    }
}

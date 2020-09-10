using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Zombie", menuName = "Character/Zombie")]
    public class Zombie_SO : ScriptableObject
    {
        public int health;
        public float speed;
        public float damage;
        public enum ZombieState
        {
            Idle,
            Walking,
            Running
        }
        public ZombieState zombieState;
        [TextArea(minLines:1,maxLines:10)]
        public string note;
    }
}

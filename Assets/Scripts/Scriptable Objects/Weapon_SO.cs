using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Utilities/Weapon")]
    public class Weapon_SO : ScriptableObject
    {
        public float reloadSpeed;
        public float fireRate;
        public float clipSize;
        public int maxAmmo;
        public float damage;
        [TextArea(maxLines: 4, minLines:1)]
        public string note;
        public enum FireMode
        {
            SemiAuto,
            Auto
        }
        public AudioClip weaponSound;
        public FireMode weaponFireMode;
    }
}

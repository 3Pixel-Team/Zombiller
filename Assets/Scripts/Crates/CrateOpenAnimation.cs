using UnityEngine;

namespace Crates
{
    public class CrateOpenAnimation : MonoBehaviour
    {
        /// <summary>
        /// Triggers the animation.
        /// </summary>
        /// <param name="open"></param>
        void openSesame(Animator open) => open.SetTrigger("Open");

        /// <summary>
        /// When player enter withing the hit box of the crate, BOOM!, its open.
        /// </summary>
        /// <param name="crate"></param> 
        private void OnTriggerEnter2D(Collider2D crate)
        {
            if (crate.CompareTag("Player")) openSesame(null); //**To be add
        }
    }
}

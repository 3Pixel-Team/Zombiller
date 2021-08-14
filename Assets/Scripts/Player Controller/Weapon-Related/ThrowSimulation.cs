using System;
namespace Assets.Scripts.Player_Controller.Weapon_Related
{
    using UnityEngine;
    using System.Collections;

    public class ThrowSimulation : MonoBehaviour
    {
        //public Transform Target;
        public float firingAngle = 45.0f;
        public float gravity = 4f;

        public Transform Projectile;
        private Transform myTransform;
        private Vector3 mouseClick;
        public int howFarToThrow = 2;


        void Awake()
        {
            myTransform = transform;
        }

        void Start()
        {
           
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                WeaponManager weaponManager = transform.GetComponent<WeaponManager>();
                if (weaponManager != null)
                {
                    if (weaponManager.NumberOfGrenades == 0)
                    {
                        return;
                    }
                    weaponManager.DecreaseGrenades();

                }

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    var mouseClick = hit.point;
                    var copyProjectile = Instantiate(this.Projectile);
                    copyProjectile.gameObject.SetActive(true);
                    copyProjectile.SetPositionAndRotation(this.myTransform.position, this.myTransform.rotation);
                    Vector3 eulerRotation = new Vector3(this.myTransform.eulerAngles.x, this.myTransform.eulerAngles.y -45, this.myTransform.eulerAngles.z);

                    copyProjectile.rotation = Quaternion.Euler(eulerRotation);
                    
                    StartCoroutine(SimulateProjectile(copyProjectile, mouseClick));
                }
                
            }
            
        }

        IEnumerator SimulateProjectile(Transform cpProjectile, Vector3 msClick)
        {
            var mouseClick = msClick;
            var copyProjectile = cpProjectile;
            // Short delay added before Projectile is thrown
            yield return new WaitForSeconds(0.1f);

            if (copyProjectile.ToString() == "null")
            {
                yield break;
            }
            // Move projectile to the position of throwing object + add some offset if needed.
            copyProjectile.position = myTransform.position + new Vector3(0, 4, 0);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(copyProjectile.position, mouseClick);

            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            while (copyProjectile.ToString() != "null")
            {
                if (copyProjectile.ToString() == "null")
                {
                    yield return null;
                    continue;
                }
                copyProjectile.transform.Translate(this.howFarToThrow * Time.deltaTime, -0.01f, this.howFarToThrow * Time.deltaTime);


                yield return null;

            }
        }
    }
}

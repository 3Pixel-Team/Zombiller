using UnityEngine;


public class DestroyGrenade : MonoBehaviour
{

    public int grenadeDmg = 100;
    public int grenadeRadius = 10;

    private void Update()
    {
        if (transform.position.y < 0)
        {
            this.DestroyEnamies();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != "Player")
        {
            this.DestroyEnamies();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag != "Player")
        {
            this.DestroyEnamies();
            Destroy(gameObject);
        }
    }

    private void DestroyEnamies()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, grenadeRadius);
        foreach (Collider col in cols)
        {
            if (col && col.tag == "Enemy")
            {
                Target script = col.GetComponent<Target>();
                if (script != null)
                {
                    script.TakeDamage(grenadeDmg);
                }
            }
        }
    }
}

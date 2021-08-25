using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;

    public int damage = 50;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    // Function getting called in Turret class to get the turret's target
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Distance from bullet to target
        Vector3 dir = target.position - transform.position;
        // The distance travelled in a frame
        float distanceThisFrame = speed * Time.deltaTime;

        // It hit if the distance from bullet to target is lower than the distance per frame
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move towards the target
        // dir.normalized returns the vector with a magnitude of 1 for the direction
        // multiplied with distanceThisFrame gives the actual Translate vector for movement in a frame
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // Rotate bullet towards the enemy
        transform.LookAt(target);

    }

    void HitTarget()
    {
        // Create particle effect as game object instance from the the prefab
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy particle game object
        Destroy(effectIns, 5f);

        // Missiles explode in a radius, standard bullets hit single targets
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        // Creates a sphere and return all colliders in an array
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {

            // Damage only colliders with "Enemy" tag
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {

        // Access the enemy script and store it in a variable ('e' for differentiating)
        Enemy e = enemy.GetComponent<Enemy>();

        // Damage the enemy if it has an 'Enemy' component
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    // Show the explosion radius in scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}

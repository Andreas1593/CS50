using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    // Target's enemy component
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]

    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    [HideInInspector]
    public bool laserSound = false;

    // Required to stop laser sound while paused
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Search for the closest target only 5 times per every second, for performance reasons
        InvokeRepeating("UpdateTarget", 0f, 0.2f);

        // Find pause menu in the hierarchy; required to stop laser sound while paused
        GameObject parent = GameObject.Find("OverlayCanvas");
        pauseMenu = parent.transform.Find("PauseMenu").gameObject;
    }

    void UpdateTarget()
    {

        // Get a list of all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Compute distance to each enemy
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            // Determine the closest enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Set nearest enemy as target if he's in range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;

        // Check if there's no target and disable laser in case of a laser beamer
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;

                    AudioManager.instance.Stop("LaserBeamer_Laser");
                    laserSound = false;

                    // Stops the particle system instead of instantly despawning particles
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        // Shoot laser if it's enabled, otherwise shoot bullet
        if (useLaser)
        {
            Laser();

            // Play laser sound once (looping) if it isn't playing already
            if (!laserSound)
            {
                AudioManager.instance.Play("LaserBeamer_Laser");
                laserSound = true;
            }

            // Stop laser sound while paused
            if (pauseMenu.activeSelf)
            {
                AudioManager.instance.Stop("LaserBeamer_Laser");
                laserSound = false;
            }
        }
        else
        {
            // Shoot when fire cooldown is 0
            if (fireCountdown <= 0)
            {
                Shoot();

                // Reset fire cooldown
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void LockOnTarget()
    {
        // Create a vector from the turret to the target
        Vector3 dir = target.position - transform.position;

        // Create a rotation with Unity's Quaternion
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        // Smoothen the transition between current and new rotation with Lerp method and
        // get the euler angles of the rotation (needed to rotate around the y-axis specificially)
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        // Damage and slow enemy continuously
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        // Enable laser
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        // Render laser from fire point to target
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // Vector pointing from target to turret
        Vector3 dir = firePoint.position - target.position;

        // Set impact effect's position to the point of impact (enemy's radius = 1)
        impactEffect.transform.position = target.position + dir.normalized;

        // Rotate impact effect towards turret
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {

        // Create a new bullet game object spawning at the fire point
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (name.StartsWith("StandardTurret"))
            AudioManager.instance.Play("StandardTurret_Shot");

        if (name.StartsWith("MissileLauncher"))
            AudioManager.instance.Play("MissileLauncher_Shot");

        if (bullet != null)
        {

            // Pass the turret's target to the bullet
            bullet.Seek(target);
        }
    }

    // Show the tower's range for debugging (only when selected)
    void OnDrawGizmosSelected()
    {

        // Draws a Wire Sphere around the turret
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Stop laser sound (important when reloading the level)
    private void OnDestroy()
    {
        if (laserSound)
        {
            AudioManager.instance.Stop("LaserBeamer_Laser");
            laserSound = false;
        }
    }

}

using System.Collections;
using UnityEngine;

// Includes the enemy script as dependency (for the 'speed' variable)
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    public GameObject model;

    private void Start()
    {
        // Get the enemy's script component
        enemy = GetComponent<Enemy>();

        // Set the first waypoint as enemy movement target
        target = Waypoints.waypoints[0];
    }
    private void Update()
    {
        // Move towards the next waypoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        // Rotate enemy model towards the next waypoint (see LockOnTarget() in Turret.cs)
        if (model != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(model.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
            model.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        // Get the next waypoint when close enough
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        // Reset to normal speed when out of attack (i. e. slow) range, otherwise Slow() should apply first
        // This can be ensured by setting up Script Execution Order
        // If not, worst case would be only one frame with normal speed (with random script order)
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        // Destroy enemy and reduce lives by 1 if he's passed all waypoints
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        // Increment waypoint index and set next waypoint
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        WaveSpawnerInfinite.EnemiesAlive--;
        Destroy(gameObject);
    }

}

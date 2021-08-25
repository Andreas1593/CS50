using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    // Reduce health when hit and destroy when 0 health
    public void TakeDamage(float amount)
    {
        health -= amount;

        // Adjust the health bar according to the current health as a float between 0 and 1
        healthBar.fillAmount = health / startHealth;

        // Check if the enemy is not dead to make sure the Die() function won't get called more than once
        // because the Destroy() function has some delay
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        // Slow effect doesn't stack since it's referencing to the start speed
        speed = startSpeed * (1f - pct);
    }

    // Increase money, create death particles and destroy enemy
    void Die()
    {
        isDead = true;

        // Kill bounty
        PlayerStats.Money += worth;

        // Death effect
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        WaveSpawnerInfinite.EnemiesAlive--;

        if (name.StartsWith("Enemy_Simple"))
            AudioManager.instance.Play("Enemy_Simple_Death");

        if (name.StartsWith("Enemy_Zombie"))
            AudioManager.instance.Play("Enemy_Simple_Death");

        if (name.StartsWith("Enemy_Tough"))
            AudioManager.instance.Play("Enemy_Tough_Death");

        if (name.StartsWith("Enemy_Skeleton"))
            AudioManager.instance.Play("Enemy_Tough_Death");

        if (name.StartsWith("Enemy_Fast"))
            AudioManager.instance.Play("Enemy_Fast_Death");

        if (name.StartsWith("Enemy_Bat"))
            AudioManager.instance.Play("Enemy_Fast_Death");

        Destroy(gameObject);
    }

}

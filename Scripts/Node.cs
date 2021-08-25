using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    // Change node color when hovering over it with the mouse
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        // Get the renderer once, for performance reasons
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        // Initialise build manager
        buildManager = BuildManager.instance;
    }

    // Returns the exact position to place the turret
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // Build turret when clicking on a node
    private void OnMouseDown()
    {


        // Don't build when mouse over UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Select node if there's already a turret
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (buildManager.GetTurretToBuild() != null)
        {
            BuildTurret(buildManager.GetTurretToBuild());
        }
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        // Check if the player has enough money to build that turret
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that");
            return;
        }

        // Subtract the turret's cost
        PlayerStats.Money -= blueprint.cost;

        // Instantiate the selected turret without rotation and cast it into a game object
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);

        // Place turret on node
        turret = _turret;

        turretBlueprint = blueprint;

        // Create build particles
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built");
    }

    public void UpgradeTurret()
    {
        // Check if the player has enough money to build that turret
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to build that");
            return;
        }

        // Subtract the upgrade's cost
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);

        // Place upgraded turret on node
        turret = _turret;

        // Create upgrade particles (same as build particles)
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded");
    }

    // Remove turret and return half of the turret cost to the player
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // Create sell particles
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;

        // Stop laser sound when selling while turret is attacking
        AudioManager.instance.Stop("LaserBeamer_Laser");
    }


    private void OnMouseEnter()
    {
        // Don't highlight when mouse over UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Don't highlight node if no turret was selected
        if (!buildManager.CanBuild)
            return;

        // Change color depending on whether player has enough money for that turret
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}

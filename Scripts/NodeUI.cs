using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;

    private LineRenderer prevRadius = null;

    // Show turret UI when clicking on a built turret
    public void SetTarget(Node _target)
    {
        // Disable previous radius when clicking from one turret to another
        if (prevRadius != null)
            prevRadius.enabled = false;

        // Node that was clicked on 
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }


        // Display sell amount
        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        // Show turret UI
        ui.SetActive(true);

        // Show attack range
        LineRenderer radius = target.turret.GetComponent<LineRenderer>();
        radius.enabled = true;
        // Required for disabling the radius when clicking from one turret to another
        prevRadius = radius;
    }

    public void Hide()
    {
        ui.SetActive(false);

        // Hide attack range
        if (target != null && target.turret != null)
        {
            LineRenderer radius = target.turret.GetComponent<LineRenderer>();
            radius.enabled = false;
        }
    }

    // Upgrade turret on the target node, deselect node and hide turret UI
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    // Call the sell turret function in the node script, deselect node and hide turret UI
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
        target.isUpgraded = false;
    }

}

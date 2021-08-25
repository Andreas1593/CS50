using UnityEngine;

public class BuildManager : MonoBehaviour
{

    // The singleton pattern ensures that there's only one build manager
    public static BuildManager instance;

    // For handling the singleton instance
    private void Awake()
    {

        // Error Checking
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }

        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    // Property to check if turret can be built
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    // Select node, deselect turret to build and show turret UI when clicking a node
    public void SelectNode(Node node)
    {

        // Deselect node when clicking on the same node again
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        // Select node
        selectedNode = node;

        // Deselect turret to build
        turretToBuild = null;

        // Set target node to show turret UI
        nodeUI.SetTarget(node);
    }

    // Deselecting node also hides the turret UI
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    // Select turret to build and deselect node
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}

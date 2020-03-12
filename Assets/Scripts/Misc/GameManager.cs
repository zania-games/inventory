using UnityEngine;

public class GameManager: MonoBehaviour
{
    public GameObject Player;
    public RectTransform Inventory;
    public GameObject UICamera;
    public Transform PlayerGridPosition;
    public Transform ContainerGridPosition;

    private RectTransform playerItems;

    void Awake()
    {
        playerItems = (RectTransform)Player.GetComponentInChildren<GridContainer>().transform;
    }

    public void EnterInventoryMode()
    {
        Player.SetActive(false);
        Inventory.gameObject.SetActive(true);
        UICamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerItems.SetParent(PlayerGridPosition, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            EnterInventoryMode();
    }
}

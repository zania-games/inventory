using UnityEngine;
using UnityEngine.UI;

public class GriddableTest: MonoBehaviour, IGriddable
{
    private Sprite generatedSprite;
    public byte NumRows = 1;
    public byte NumColumns = 1;
    public Texture2D GridTexture;
    public Image Target;

    public int GetNumRows()
    {
        return NumRows;
    }

    public int GetNumColumns()
    {
        return NumColumns;
    }

    public Sprite GetGridSprite()
    {
        return generatedSprite;
    }

    public void OnInsert() {}
    public void OnRemove() {}
    public void OnDrop() {}

    void Start()
    {
        generatedSprite =
            Sprite.Create(GridTexture, new Rect(0, 0, GridTexture.width, GridTexture.height), Vector2.zero);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Target.sprite = GetGridSprite();
    }
}

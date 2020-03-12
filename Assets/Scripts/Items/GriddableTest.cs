using UnityEngine;
using UnityEngine.UI;

public class GriddableTest: MonoBehaviour, IGriddable
{
    private Sprite generatedSprite;
    public byte NumRows = 1;
    public byte NumColumns = 1;
    public Texture2D GridTexture;

    void Start()
    {
        generatedSprite =
            Sprite.Create(GridTexture, new Rect(0, 0, GridTexture.width, GridTexture.height), Vector2.zero);
    }

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
}

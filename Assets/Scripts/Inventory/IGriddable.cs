using UnityEngine;

///< A `GameObject` that can be placed into a `GridContainer`, and which can therefore be put into inventories.
public interface IGriddable
{
    ///< the number of rows occupied in a grid
    int GetNumRows();

    ///< the number of columns occupied in a grid
    int GetNumColumns();

    ///< Loaded into the `Image` that will serve as the visual representation of this object in a grid.
    Sprite GetGridSprite();
}

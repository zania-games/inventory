using System.Collections.Generic;
using UnityEngine;

///< A container for items that can be put into an inventory (i.e. `IGriddable`s), in the form of a grid.
public class GridContainer: MonoBehaviour
{
    public enum Type
    {
        Other,
        Helmet,
        Vest,
        Pants,
        Backpack,
        Shoes,
        Shirt
    }

    public enum InsertOutcome
    {
        Success,
        DoesntFit,
        AlreadyPresent
    }

    private IGriddable[,] grid;
    private IDictionary<IGriddable, byte[]> items;

    ///< Indicates the type of container in the game world this object is associated with.
    public Type ContainerType;

    ///< the number of rows in the grid
    public int NumRows;

    ///< the number of columns in the grid
    public int NumColumns;

    void Start()
    {
        grid = new IGriddable[NumRows, NumColumns];
        
        items = new Dictionary<IGriddable, byte[]>();
    }

    ///< the number of items inside this container
    public int Count
    {
        get => items.Count;
    }

    /**
     * @brief Determines whether an `IGriddable` fits in this container, and can therefore be inserted into it.
     * @param item the object of the query
     * @param row the row in which the top-left corner of the item is to be placed
     * @param col the column in which the top-left corner of the item is to be placed
     * @return whether the item fits in this container
     */
    public bool ItemFits(IGriddable item, int row, int col)
    {
        int rowEnd = row + item.GetNumRows(), colEnd = col + item.GetNumColumns();
        if (rowEnd > NumRows || colEnd > NumColumns)
            return false;
        for (int i = row; i < rowEnd; ++i)
        {
            for (int j = col; j < colEnd; ++j)
            {
                if (grid[i, j] != null)
                    return false;
            }
        }
        return true;
    }

    ///< Returns whether a given `IGriddable` is inside this container.
    public bool Contains(IGriddable item)
    {
        return items.ContainsKey(item);
    }

    /**
     * @brief Attempts to insert an IGriddable into this container.
     * @param item a non-null pointer to the item to be inserted
     * @param row the row in which the top-left corner of the item is to be placed
     * @param col the column in which the top-left corner of the item is to be placed
     * @return If the item was successfully inserted, returns `InsertOutcome.Success`. If the item is already
     * contained, returns `InsertOutcome.AlreadyPresent`. If the item is not already contained, but does not fit in
     * the container, returns `InsertOutcome.DoesntFit`.
     */
    public InsertOutcome Insert(IGriddable item, int row, int col)
    {
        if (items.ContainsKey(item))
            return InsertOutcome.AlreadyPresent;
        else if (!ItemFits(item, row, col))
            return InsertOutcome.DoesntFit;
        else
        {
            int itemRows = item.GetNumRows(), itemCols = item.GetNumColumns();
            for (int i = 0; i < itemRows; ++i)
            {
                for (int j = 0; j < itemCols; ++j)

                    grid[row + i, col + j] = item;
            }
            items.Add(item, new byte[]{(byte)row, (byte)col});
            return InsertOutcome.Success;
        }
    }

    /**
     * @brief Removes an `IGriddable` from this container if it is present.
     * @param item a non-null pointer to the item to be removed
     * @return whether the item was present, and correspondingly removed
     */
    public bool Remove(IGriddable item)
    {
        byte[] location;
        if (items.TryGetValue(item, out location))
        {
            int itemRows = item.GetNumRows(), itemCols = item.GetNumColumns();
            for (int i = 0; i < itemRows; ++i)
            {
                for (int j = 0; j < itemCols; ++j)
                    grid[location[0] + i, location[1] + j] = null;
            }

            items.Remove(item);
            return true;
        }
        else
            return false;
    }

    ///< Returns the item occupying a given cell in the grid, or `null` if it is empty.
    public IGriddable this[int row, int col]
    {
        get => grid[row, col];
    }
}

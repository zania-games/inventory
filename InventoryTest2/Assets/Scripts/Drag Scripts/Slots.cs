using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IDropHandler
{

    /**************************************************************
     * Theory on how to convert to matrix UI:
     * 
     * Considering a 2x2 item, adjusting the Grid Layout Group 
     * padding to Left:55 and Top: 55 gives it the appearance of 
     * covering the four correct cells. In order to trick the 
     * non-main slots into thinking they have an item stored we 
     * can keep a reference to the object covering the item in this
     * code. Whenever the player attempts to pick up in an empty
     * slot he/she picks up the referenced item.
     * 
     * 
     * Explenation of Code:
     * 
     * This essentailly changes the parent of the dragged item
     * to the target slot so that whenever you let go it goes 
     * to the new slot rather than the old one. I'm still a bit
     * fuzzy on how it does this but the tutorial that I got this 
     * bit of code from is Here;https://www.youtube.com/watch?time_continue=925&v=c47QYgsJrWc&feature=emb_logo
     **************************************************************/

    public int col;
    public int row;

    GridLayoutGroup grid;
    public GameObject item
    {
        get
        {
            if(transform.childCount > 0){
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }


    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        GridContainer grid1 = GetComponentInParent<GridContainer>();
        Debug.Log(grid1.ItemFits(DragHandler.itemBeingDragged.GetComponent<DragHandler>(), row, col));
        //if a slot doesn't already have an item we don't want to run this we want it to fail
        if (grid1.ItemFits(DragHandler.itemBeingDragged.GetComponent<DragHandler>(), row, col))
        {
            grid1.Insert(DragHandler.itemBeingDragged.GetComponent<DragHandler>(), row, col);
            

            DragHandler.itemBeingDragged.transform.SetParent(transform);
            grid = GetComponent<GridLayoutGroup>();
            if (item.transform.localScale.x == 2 && item.transform.localScale.y == 2)
            {
                grid.padding.left = 100;
                grid.padding.top = 100;

            }
            else if(item.transform.localScale.x == 3 && item.transform.localScale.y == 3)
            {
                grid.padding.left = 200;
                grid.padding.top = 200;
            }
            else if (item.transform.localScale.x == 4 && item.transform.localScale.y == 4)
            {
                grid.padding.left = 300;
                grid.padding.top = 300;
            }
            else if (item.transform.localScale.x == 5 && item.transform.localScale.y == 5)
            {
                grid.padding.left = 400;
                grid.padding.top = 400;
            }
            else if (item.transform.localScale.x == 6 && item.transform.localScale.y == 6)
            {
                grid.padding.left = 500;
                grid.padding.top = 500;
            }

        }

        
    }
    #endregion

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IGriddable
{
    /**************************************************************
     * This code would likely need to be put in griddable.
     * It does appear that the dragging just works without
     * any sort of collision detection needed, there aren't
     * even 2d colliders on the items. OnBeginDrag() seems
     * to be similar to start, OnDrag() like update(), and
     * OnEndDrag() like start() except after you let the
     * mouse go.
     **************************************************************/
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    public int rows;
    public int cols;
    public Sprite image;

    public Sprite GetGridSprite()
    {
        return image;
    }

    public int GetNumColumns()
    {
        return cols;
    }

    public int GetNumRows()
    {
        return rows;
    }

    #region IBeginDragHandler implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        //GridLayoutGroup grid = GetComponentInParent<GridLayoutGroup>();


        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(transform.root);
    }
    #endregion

    #region IDragHandler implementation


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    #endregion

    #region IEndDragHandler implementation
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        else
        {
            GridLayoutGroup grid = startParent.gameObject.GetComponent<GridLayoutGroup>();
            grid.padding.left = 0;
            grid.padding.top = 0;
            startParent.GetComponentInParent<GridContainer>().Remove(this);
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    #endregion
}

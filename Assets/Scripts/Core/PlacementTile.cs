using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// States the placement tile can be in
/// </summary>
public enum PlacementTileState
{
    Filled,
    Empty
}

/// <summary>
/// Simple class to illustrate tile placement locations
/// </summary>
public class PlacementTile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler , IPointerExitHandler
{
    private TowerBase currentTower = null;

    /// <summary>
    /// Material to use when this tile is empty
    /// </summary>
    public Material emptyMaterial;

    /// <summary>
    /// Material to use when this tile is filled
    /// </summary>
    public Material filledMaterial;

    /// <summary>
    /// The renderer whose material we're changing
    /// </summary>
    public Renderer tileRenderer;

    /// <summary>
    /// Update the state of this placement tile
    /// </summary>
    public void SetState(PlacementTileState newState)
    {
        switch (newState)
        {
            case PlacementTileState.Filled:
                if (tileRenderer != null && filledMaterial != null)
                {
                    tileRenderer.sharedMaterial = filledMaterial;
                }

                break;
            case PlacementTileState.Empty:
                if (tileRenderer != null && emptyMaterial != null)
                {
                    tileRenderer.sharedMaterial = emptyMaterial;
                }

                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelUIBehavior.SelectedTowerBase.transform.position = transform.position;
        LevelUIBehavior.SelectedTowerBase.transform.rotation = transform.rotation;
        currentTower = LevelUIBehavior.SelectedTowerBase;
        LevelUIBehavior.SelectedTowerBase = null;
        currentTower.HideRadiusVisualizers();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentTower!=null)
            return;
        LevelUIBehavior.AboveTile = true;
        if(LevelUIBehavior.SelectedTowerBase == null)
            return;
        LevelUIBehavior.SelectedTowerBase.transform.position = transform.position;
        LevelUIBehavior.SelectedTowerBase.transform.rotation = transform.rotation;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(currentTower!=null)
            return;
        LevelUIBehavior.AboveTile = false;
    }
}
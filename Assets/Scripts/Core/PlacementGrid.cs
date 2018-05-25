using System.Collections;
using System.Collections.Generic;
using Core.Utilities;
using TowerDefense.UI.HUD;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlacementGrid : MonoBehaviour
{
    /// <summary>
    /// Prefab used to visualise the grid
    /// </summary>
    public PlacementTile placementTilePrefab;

    public IntVector2 Dimensions;

    /// <summary>
    /// Array of <see cref="PlacementTile"/>s
    /// </summary>
    PlacementTile[,] m_Tiles;


    /// <summary>
    /// Size of the edge of a cell
    /// </summary>
    [Tooltip("The size of the edge of one grid cell for this area. Should match the physical grid size of towers")]
    public float gridSize = 1;


    private void Start()
    {
        SetUpGrid();
    }

    /// <summary>
    /// Set collider's size and center
    /// </summary>
    void ResizeCollider()
    {
        var myCollider = GetComponent<BoxCollider>();
        Vector3 size = new Vector3(Dimensions.x, 0, Dimensions.y) * gridSize;
        myCollider.size = size;

        // Collider origin is our bottom-left corner
        myCollider.center = size * 0.5f;
    }

    /// <summary>
    /// Instantiates Tile Objects to visualise the grid and sets up the <see cref="m_AvailableCells" />
    /// </summary>
    protected void SetUpGrid()
    {
        PlacementTile tileToUse;
        tileToUse = placementTilePrefab;


        if (tileToUse != null)
        {
            // Create a container that will hold the cells.
            var tilesParent = new GameObject("Container");
            tilesParent.transform.parent = transform;
            tilesParent.transform.localPosition = Vector3.zero;
            tilesParent.transform.localRotation = Quaternion.identity;
            m_Tiles = new PlacementTile[Dimensions.x, Dimensions.y];

            for (int y = 0; y < Dimensions.y; y++)
            {
                for (int x = 0; x < Dimensions.x; x++)
                {
                    Vector3 targetPos = GridToWorld(new IntVector2(x, y), new IntVector2(1, 1));
                    targetPos.y += 0.01f;
                    PlacementTile newTile = Instantiate(tileToUse);
                    newTile.transform.parent = tilesParent.transform;
                    newTile.transform.position = targetPos;
                    newTile.transform.localRotation = Quaternion.identity;

                    m_Tiles[x, y] = newTile;
                    newTile.SetState(PlacementTileState.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Returns the world coordinates corresponding to a grid location.
    /// </summary>
    /// <param name="gridPosition">The coordinate in grid space</param>
    /// <param name="sizeOffset"><see cref="IntVector2"/> indicating size of object to center.</param>
    /// <returns>Vector3 containing world coordinates for specified grid cell.</returns>
    public Vector3 GridToWorld(IntVector2 gridPosition, IntVector2 sizeOffset)
    {
        // Calculate scaled local position
        Vector3 localPos =
            new Vector3(gridPosition.x + (sizeOffset.x * 0.5f), 0, gridPosition.y + (sizeOffset.y * 0.5f)) *
            gridSize;

        return transform.TransformPoint(localPos);
    }


#if UNITY_EDITOR
    /// <summary>
    /// On editor/inspector validation, make sure we size our collider correctly.
    /// Also make sure the collider component is hidden so nobody can mess with its settings to ensure its integrity.
    /// Also communicates the idea that the user should not need to modify those values ever.
    /// </summary>
    void OnValidate()
    {
        // Validate grid size
        if (gridSize <= 0)
        {
            Debug.LogError("Negative or zero grid size is invalid");
            gridSize = 1;
        }

        // Validate dimensions
        if (Dimensions.x <= 0 ||
            Dimensions.y <= 0)
        {
            Debug.LogError("Negative or zero grid dimensions are invalid");
            Dimensions = new IntVector2(Mathf.Max(Dimensions.x, 1), Mathf.Max(Dimensions.y, 1));
        }

        // Ensure collider is the correct size
        ResizeCollider();

        GetComponent<BoxCollider>().hideFlags = HideFlags.HideInInspector;
    }

    /// <summary>
    /// Draw the grid in the scene view
    /// </summary>
    void OnDrawGizmos()
    {
        Color prevCol = Gizmos.color;
        Gizmos.color = Color.cyan;

        Matrix4x4 originalMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw local space flattened cubes
        for (int y = 0; y < Dimensions.y; y++)
        {
            for (int x = 0; x < Dimensions.x; x++)
            {
                var position = new Vector3((x + 0.5f) * gridSize, 0, (y + 0.5f) * gridSize);
                Gizmos.DrawWireCube(position, new Vector3(gridSize, 0, gridSize));
            }
        }

        Gizmos.matrix = originalMatrix;
        Gizmos.color = prevCol;

        // Draw icon too, in center of position
        Vector3 center = transform.TransformPoint(new Vector3(gridSize * Dimensions.x * 0.5f,
            1,
            gridSize * Dimensions.y * 0.5f));
        Gizmos.DrawIcon(center, "build_zone.png", true);
    }
#endif
}
using System.Collections;
using System.Collections.Generic;
using Core.Utilities;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlacementGrid : MonoBehaviour
{
    public IntVector2 Dimensions;


    /// <summary>
    /// Size of the edge of a cell
    /// </summary>
    [Tooltip("The size of the edge of one grid cell for this area. Should match the physical grid size of towers")]
    public float gridSize = 1;

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
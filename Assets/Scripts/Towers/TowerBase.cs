using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public float Radius = 10f;
    public GameObject radiusVisualizer;
    public float radiusVisualizerHeight = 0.02f;


    public void ShowRadiusVisualizers()
    {
        radiusVisualizer.SetActive(true);
        
        radiusVisualizer.transform.localPosition = new Vector3(0, radiusVisualizerHeight, 0);
        radiusVisualizer.transform.localScale = Vector3.one * Radius * 2.0f;
       // radiusVisualizer.transform.localRotation = new Quaternion {eulerAngles = localEuler};

    }


    public void HideRadiusVisualizers()
    {
        radiusVisualizer.SetActive(false);
    }
}
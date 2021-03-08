using UnityEngine;
using System.Collections;

public class CustomAxisSortCamera : MonoBehaviour 
{
    void Start()
    {
        var camera = GetComponent<Camera>();
        camera.transparencySortMode = TransparencySortMode.CustomAxis;
        camera.transparencySortAxis = new Vector3(0.0f, 1.0f, -0.26f);
    }
}
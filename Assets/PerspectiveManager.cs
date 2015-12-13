using UnityEngine;
using System.Collections;

public class PerspectiveManager : MonoBehaviour {

    Vector3 vanishingPoint;

    void Start()
    {
        Camera camera = Camera.main;

        Vector3 pos = Camera.main.WorldToViewportPoint(camera.transform.position);

        Debug.Log(pos);

        if (pos.x < 0.0) Debug.Log("I am left of the camera's view.");
        if (1.0 < pos.x) Debug.Log("I am right of the camera's view.");
        if (pos.y < 0.0) Debug.Log("I am below the camera's view.");
        if (1.0 < pos.y) Debug.Log("I am above the camera's view.");

        DrawRays();
    }

    void DrawRays()
    {

    }

}

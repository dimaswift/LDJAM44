using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MaxZ, MinZ;
    public static CameraController Instance { get; private set; }

    Vector3 clickSceenPoint;
    Vector3 clickPosition;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetMousePoint()
    {
        return GetWorldPoint(Input.mousePosition);
    }

    public Vector3 GetWorldPoint(Vector2 screenPoint)
    {
        var plane = new Plane(Vector3.forward, Vector3.zero);
        var ray = Camera.main.ScreenPointToRay(screenPoint);
        float length;
        plane.Raycast(ray, out length);
        return ray.GetPoint(length);
    }

    float GetViewportSize()
    {
        return GetWorldPoint(new Vector2(0, 0)).x - GetWorldPoint(new Vector2(Screen.width, 0)).x;
    }

    void Update()
    {
        var pos = transform.position;

        if (Input.GetMouseButtonDown(2))
        {
            clickPosition = transform.position;
            clickSceenPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            pos = Vector3.Lerp(pos, clickPosition + (GetWorldPoint(clickSceenPoint) - GetMousePoint()), Time.unscaledDeltaTime * 20);
        }

        if (Input.GetMouseButtonUp(2))
        {
            clickSceenPoint = Input.mousePosition;
        }



        pos.z += Input.mouseScrollDelta.y;
        pos.z = Mathf.Clamp(pos.z, MinZ, MaxZ);
        transform.position = pos;

    }
}

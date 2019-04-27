using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{

    void SetStatic(bool isStatic);
    void AddForce(Vector2 force);

    void AddTorque(float torque);

    void SetActive(bool active);

    Vector3 Position { get; set; }

    float Rotation { get; set; }

    void ResetPosition();
}

public class Thrower : MonoBehaviour
{
    public GameObject ObjectToThrowPrefab;
    public float ThrowForce;

    IThrowable objectToThrow;

    private void Awake()
    {
        objectToThrow = Instantiate(ObjectToThrowPrefab).GetComponent<IThrowable>();

        objectToThrow.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToThrow = Instantiate(ObjectToThrowPrefab).GetComponent<IThrowable>();
            objectToThrow.SetActive(true);
            objectToThrow.SetStatic(true);
            objectToThrow.ResetPosition();
            objectToThrow.Position = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            var angle = Helper.GetAngle(objectToThrow.Position, CameraController.Instance.GetMousePoint());
            objectToThrow.Rotation = angle;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        if (Input.GetMouseButtonUp(0))
        {
            objectToThrow.SetStatic(false);
            var dir = (CameraController.Instance.GetMousePoint() - objectToThrow.Position);
            dir = Vector3.ClampMagnitude(dir, 30);
            objectToThrow.AddForce(dir * ThrowForce);
            objectToThrow.AddTorque(Random.Range(-13000, 13000));
        }
    }
}

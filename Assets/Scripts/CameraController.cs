using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 7f;
    public float minDistance = 3f;
    public float maxDistance = 8f;
    public Vector3 offset;
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target)
        {
            Debug.Log("Follow target not found");

            return;
        }

        float num = Input.GetAxis("Mouse ScrollWheel");

        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }
}
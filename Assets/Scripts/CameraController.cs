using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float camSmoothSpeed;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        float smoothPosition = Mathf.Lerp(transform.position.y, targetPosition.y, camSmoothSpeed * Time.deltaTime);
        transform.position = new Vector3(targetPosition.x, smoothPosition, targetPosition.z);
    }
}

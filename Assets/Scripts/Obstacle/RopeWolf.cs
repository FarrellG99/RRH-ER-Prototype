using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeWolf : MonoBehaviour
{
    [SerializeField] float positionY;
    [SerializeField] int goUpSpeed;

    private int elapsedFrames;
    private Vector3 targetPosition;
    bool isCapturing;

    private float startTime;

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, positionY);
        isCapturing = false;
        elapsedFrames = 0;
        startTime = Time.time;
    }

    private void Update()
    {
        if (isCapturing)
        {
            float interpolant = (float)elapsedFrames / goUpSpeed;

            transform.position = Vector3.Lerp(transform.position, targetPosition, interpolant);

            elapsedFrames = (elapsedFrames + 1) % (goUpSpeed + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RopeWolfCapture();
    }

    public void RopeWolfCapture()
    {
        GameplayManager.Gameplay.IsPlaying = false;
        isCapturing = true;
    }
}

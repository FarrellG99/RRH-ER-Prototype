using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeWolf : MonoBehaviour
{
    [SerializeField] float positionY;
    [SerializeField] [Range (0f, 4f)] float goUpSpeed;
    [SerializeField] float captureDelayTime;

    private float captureDelayTimer;
    private Vector3 targetPosition;
    bool isCapturing;

    private float startTime;

    private void Start()
    {
        captureDelayTimer = captureDelayTime;
        targetPosition = new Vector3(transform.position.x, positionY);
        isCapturing = false;
        startTime = Time.time;
    }

    private void Update()
    {
        if (isCapturing)
        {
            if(captureDelayTimer > -1)
            {
                captureDelayTimer -= Time.fixedDeltaTime;
            } else if (captureDelayTimer <= 0) {
                transform.position = Vector3.Lerp(transform.position, targetPosition, goUpSpeed * Time.deltaTime);
            }
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

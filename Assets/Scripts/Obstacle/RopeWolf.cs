using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeWolf : MonoBehaviour
{
    [SerializeField] float positionY;
    [SerializeField] float goUpSpeed;

    private float timeElapse;
    private Vector3 targetPosition;
    private Animator animator;
    bool isCapturing;

    private float startTime;

    private void Start()
    {
        timeElapse = 0;
        targetPosition = new Vector3(transform.position.x, positionY);
        isCapturing = false;
        startTime = Time.time;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isCapturing)
        {
            if (timeElapse < goUpSpeed)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, timeElapse / goUpSpeed);
                timeElapse += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            RopeWolfCapture();
    }

    public void RopeWolfCapture()
    {
        GameplayManager.Gameplay.IsPlaying = false;
        animator.SetTrigger("Capture");
    }

    public void Capturing()
    {
        isCapturing = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfNet : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject net;

    [Header("Attribute")]
    [SerializeField] float netSpeed;
    private Animator animator;
    private bool netThrown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (netThrown)
        {
            float moveNet = net.transform.localPosition.x;
            moveNet -= netSpeed * Time.deltaTime;
            net.transform.localPosition = new Vector3(moveNet, -.3f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (netThrown == false && GameplayManager.Gameplay.IsPlaying)
        {
            if (other.gameObject.tag == "WolfNetTrigger")
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    #region EventAnimation
    public void ThrowNet()
    {

        netThrown = true;
    }
    #endregion
}

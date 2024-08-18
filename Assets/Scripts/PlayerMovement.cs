using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [Header("Speed")]
    [SerializeField] private float speed;

    [Header("Layer")]
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        #region Movement
        Vector3 velocityInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocityInput.Normalize();

        rb.velocity = velocityInput * speed + Vector3.up * rb.velocity.y;
        #endregion

        #region Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, 100, groundLayer)) 
        {
            transform.LookAt(hitInfo.point);
        }
        #endregion

        #region Animation
        animator.SetFloat("velocity", rb.velocity.magnitude);
        #endregion

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        transform.GetChild(4).GetComponent<ParticleSystem>().Play();
        speed *= 3;
        yield return new WaitForSeconds(0.3f);
        speed /= 3;
    }
}

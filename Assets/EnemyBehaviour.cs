using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    [SerializeField] public float speed;
    [SerializeField] public float minDistance = 0.1f;
    private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private GameObject pivotPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        pivotPoint = new GameObject("PivotPoint");
        pivotPoint.transform.parent = transform;
        pivotPoint.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - pivotPoint.transform.position;
        float distance = direction.magnitude;
        if (distance > minDistance && distance < 20 && minDistance != 0)
        {
            animator.SetBool("IsMoving", true);
            MoveTowardsPlayer(direction);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void MoveTowardsPlayer(Vector2 direction)
    {
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pivotPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = direction * speed;
    }
}

using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float dashDistance = 3f;
    public float clickMoveThreshold = 0.1f;
    public PhotonView pv;

    private Vector2 targetPosition;
    private Rigidbody2D rb;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;
    private bool isMoving;
    private Queue<Vector2> pathQueue = new Queue<Vector2>();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (pv.IsMine)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SetNewTarget(mousePosition);

            //대시
            if (Input.GetMouseButtonDown(0) && Time.time >= lastDashTime + dashCooldown)
            {
                StartDash(mousePosition);
            }

            Vector2 lookDir = mousePosition - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            ContinueDash();
        }
        else if (isMoving)
        {
            MoveToTarget();
        }
    }

    void SetNewTarget(Vector2 newTarget)
    {
        targetPosition = newTarget;
        isMoving = true;
        pathQueue.Clear();
        pathQueue.Enqueue(targetPosition);
    }

    void MoveToTarget()
    {
        if (pathQueue.Count > 0)
        {
            Vector2 currentTarget = pathQueue.Peek();
            
            if (Vector2.Distance(rb.position, currentTarget) < clickMoveThreshold)
            {
                pathQueue.Dequeue();
                if (pathQueue.Count == 0)
                {
                    isMoving = false;
                }

                return;
            }
            
            Vector2 direction = (currentTarget - rb.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void StartDash(Vector2 dashTarget)
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        Vector2 dashDirection = (dashTarget - rb.position).normalized;
        targetPosition = rb.position + dashDirection * Mathf.Min(dashDistance, Vector2.Distance(rb.position, dashTarget));
    }

    void ContinueDash()
    {
        if (dashTimeLeft > 0)
        {
            Vector2 dashDirection = (targetPosition - rb.position).normalized;
            float distanceThisFrame = dashSpeed * Time.fixedDeltaTime;
            
            if (Vector2.Distance(rb.position, targetPosition) <= distanceThisFrame)
            {
                rb.MovePosition(targetPosition);
                isDashing = false;
            }
            else
            {
                rb.MovePosition(rb.position + dashDirection * distanceThisFrame);
            }

            dashTimeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    
}
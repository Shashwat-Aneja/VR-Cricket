using UnityEngine;

public class AIFieldingController : MonoBehaviour
{
    [Header("Fielding Settings")]
    public float moveSpeed = 5f;
    public float catchRange = 1.5f;
    public float reactionDelay = 0.4f;

    [Header("References")]
    public Transform ball;
    public Transform homePosition;

    private bool chasingBall = false;
    private float delayTimer = 0f;

    void Update()
    {
        if (ball == null) return;

        // Check if ball is moving
        if (BallIsInPlay() && !chasingBall)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= reactionDelay)
            {
                chasingBall = true;
                delayTimer = 0f;
            }
        }

        if (chasingBall)
            ChaseBall();
        else
            ReturnHome();
    }

    bool BallIsInPlay()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        return rb.velocity.magnitude > 0.3f;
    }

    void ChaseBall()
    {
        Vector3 dir = (ball.position - transform.position).normalized;

        // Rotate smoothly towards ball
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 4f);

        // Move towards ball
        transform.position += dir * moveSpeed * Time.deltaTime;

        // Catch check
        if (Vector3.Distance(transform.position, ball.position) <= catchRange)
            CatchBall();
    }

    void CatchBall()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ball.position = transform.position + transform.forward * 0.5f;
        chasingBall = false;
        delayTimer = 0f;

        // After catching, return back
        Invoke(nameof(ReleaseBall), 0.5f);
    }

    void ReleaseBall()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // Throw ball towards wicketkeeper / bowler
        rb.AddForce(-transform.forward * 10f + Vector3.up * 4f, ForceMode.VelocityChange);
    }

    void ReturnHome()
    {
        Vector3 dir = (homePosition.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, homePosition.position) > 0.5f)
        {
            transform.position += dir * moveSpeed * 0.6f * Time.deltaTime;
        }
        else
        {
            // Idle orientation
            transform.rotation = Quaternion.LookRotation(homePosition.forward);
        }
    }
}

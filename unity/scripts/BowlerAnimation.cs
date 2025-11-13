using UnityEngine;

public class BowlerAnimation : MonoBehaviour
{
    [Header("References")]
    public Transform rightArm;
    public Transform leftLeg;
    public Transform rightLeg;
    public Transform spine;
    public Transform ball;

    [Header("Animation Settings")]
    public float runUpSpeed = 2f;
    public float armSpeed = 120f;
    public float releaseTime = 2.5f;

    private float timer = 0f;
    private bool ballReleased = false;

    void Start()
    {
        ResetPose();
    }

    void Update()
    {
        timer += Time.deltaTime;

        RunUpMovement();
        ArmBowlingMotion();
        ReleaseBall();
    }

    void RunUpMovement()
    {
        // Simple forward running motion
        float offset = Mathf.Sin(Time.time * runUpSpeed) * 0.05f;

        leftLeg.localRotation = Quaternion.Euler(offset * 60f, 0, 0);
        rightLeg.localRotation = Quaternion.Euler(-offset * 60f, 0, 0);

        spine.localRotation = Quaternion.Euler(0, 0, offset * 5f);
    }

    void ArmBowlingMotion()
    {
        // Main bowling arm movement
        float armAngle = Mathf.Lerp(-30f, 200f, timer / releaseTime);

        rightArm.localRotation = Quaternion.Euler(armAngle, 0, 0);
    }

    void ReleaseBall()
    {
        if (!ballReleased && timer >= releaseTime)
        {
            ballReleased = true;

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            // Simple forward force
            rb.AddForce(transform.forward * 20f, ForceMode.VelocityChange);
        }
    }

    void ResetPose()
    {
        timer = 0f;
        ballReleased = false;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // Reset arm & leg rotations
        rightArm.localRotation = Quaternion.Euler(-30f, 0, 0);
        leftLeg.localRotation = Quaternion.identity;
        rightLeg.localRotation = Quaternion.identity;
        spine.localRotation = Quaternion.identity;
    }

    public void TriggerBowl()
    {
        ResetPose();
    }
}

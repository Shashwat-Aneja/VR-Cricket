using UnityEngine;

public class PlayerBattingController : MonoBehaviour
{
    [Header("References")]
    public Transform bat;        // Bat model
    public Rigidbody ball;       // The cricket ball

    [Header("Shot Settings")]
    public float powerMultiplier = 1.8f;
    public float loftMultiplier = 0.6f;
    public float edgeChance = 0.1f;

    private Vector3 lastBatPosition;
    private Vector3 batVelocity;

    void Start()
    {
        lastBatPosition = bat.position;
    }

    void Update()
    {
        // Calculate bat speed for shot strength
        batVelocity = (bat.position - lastBatPosition) / Time.deltaTime;
        lastBatPosition = bat.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ProcessShot(collision);
        }
    }

    void ProcessShot(Collision collision)
    {
        Vector3 hitDirection = bat.forward.normalized;

        float swingSpeed = batVelocity.magnitude;

        // Base force
        Vector3 force = hitDirection * swingSpeed * powerMultiplier;

        // Add loft (vertical component)
        force += Vector3.up * (swingSpeed * loftMultiplier);

        // Random edges for realism
        if (Random.value < edgeChance)
        {
            force = (bat.right * (Random.value > 0.5f ? 1 : -1)) * swingSpeed * 1.2f;
            force += Vector3.up * (swingSpeed * 0.3f);
        }

        // Apply force to ball
        ball.isKinematic = false;
        ball.AddForce(force, ForceMode.Impulse);

        Debug.Log($"Ball Hit! Force: {force}, Swing Speed: {swingSpeed}");
    }
}

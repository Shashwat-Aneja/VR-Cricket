using UnityEngine;
using TMPro;

public class VRHUDManager : MonoBehaviour
{
    [Header("References")]
    public Rigidbody ball;
    public Transform ballStartPoint;
    public TMP_Text runsText;
    public TMP_Text wicketsText;
    public TMP_Text oversText;

    [Header("Gameplay Settings")]
    public float boundaryDistance = 55f;
    public int ballsPerOver = 6;

    private int runs = 0;
    private int wickets = 0;
    private int ballsBowled = 0;

    private Vector3 lastBallPosition;
    private bool ballHit = false;

    void Start()
    {
        lastBallPosition = ball.transform.position;
        UpdateHUD();
    }

    void Update()
    {
        DetectBoundary();
        DetectDeadBall();
    }

    void DetectBoundary()
    {
        if (!ballHit) return;

        float distance = Vector3.Distance(ballStartPoint.position, ball.transform.position);

        if (distance >= boundaryDistance)
        {
            AddRuns(4); // treat as boundary
            ResetBall();
        }
    }

    void DetectDeadBall()
    {
        if (!ballHit) return;

        // If ball velocity is low, treat as completed play
        if (ball.velocity.magnitude < 0.3f)
        {
            EndBall();
        }
    }

    public void RegisterHit()
    {
        ballHit = true;
        lastBallPosition = ball.transform.position;
    }

    void AddRuns(int r)
    {
        runs += r;
        UpdateHUD();
    }

    public void AddRunsManual(int r)  // useful for AI fielder throws
    {
        runs += r;
        UpdateHUD();
    }

    void EndBall()
    {
        ballHit = false;
        ballsBowled++;

        if (ballsBowled >= ballsPerOver)
        {
            ballsBowled = 0;
        }

        UpdateHUD();
        ResetBall();
    }

    void ResetBall()
    {
        ball.isKinematic = true;
        ball.transform.position = ballStartPoint.position;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        ball.isKinematic = false;
    }

    public void AddWicket()
    {
        wickets++;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        runsText.text = "Runs: " + runs;
        wicketsText.text = "Wickets: " + wickets;
        oversText.text = "Overs: " + (ballsBowled / ballsPerOver) + "." + (ballsBowled % ballsPerOver);
    }
}

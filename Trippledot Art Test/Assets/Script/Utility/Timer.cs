using UnityEngine;

public struct Timer
{
    private readonly float startTime;
    private readonly float duration;

    public Timer(float duration)
    {
        startTime = Time.time;
        this.duration = Mathf.Max(duration, float.Epsilon);
    }

    public float Progress
    {
        get
        {
            return Mathf.Clamp01((Time.time - startTime) / duration);
        }
    }

    public bool IsCompleted
    {
        get
        {
            return Progress >= 1f;
        }
    }
}

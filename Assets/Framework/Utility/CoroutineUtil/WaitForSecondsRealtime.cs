using UnityEngine;

public class WaitForSecondsRealtime : CustomYieldInstruction
{
    private float waitTime;
    public override bool keepWaiting => Time.realtimeSinceStartup < waitTime;
    public WaitForSecondsRealtime(float time) => waitTime = Time.realtimeSinceStartup + time;

    public void Reset(float waitTime)
    {
        Reset();
        this.waitTime = Time.realtimeSinceStartup + waitTime;
    }
}

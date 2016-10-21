public sealed class CoolDownTimer
{
    public float tickRate;
    public float rate;
    private float currentRate;
    private bool startCoolDown;
    public bool timerFinished;

    public void ManualUpdate()
    {
        if (currentRate < rate)
        {
            currentRate += tickRate;
            timerFinished = false;
        }
        if (!(currentRate >= rate))
        {
            return;
        }
        timerFinished = true;
        currentRate = 0;
    }
}
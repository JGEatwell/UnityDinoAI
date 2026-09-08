using UnityEngine;

public class SleepState : IDinoState
{
    private const float RestRatePerSecond = 5;

    public void Enter(DinoController controller)
    {
        controller.Movement.Stop();
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController controller)
    {
        controller.Stats.Rest(RestRatePerSecond * Time.deltaTime);

        if (controller.Stats.Tiredness >= controller.Config.sleepSatiation)
            return new Idlestate();

        return this;
    }
}

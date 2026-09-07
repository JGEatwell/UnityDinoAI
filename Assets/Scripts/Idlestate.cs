using UnityEngine;

public class Idlestate : IDinoState
{
    private float wanderTimer;
    private const float wanderInterval = 10f;
    private const float wanderRadius = 20f;

    public void Enter(DinoController controller)
    {
        wanderTimer = 0f;
    }

    public void Exit(DinoController controller)
    {
        controller.Movement.Stop();
    }

    public IDinoState Tick(DinoController controller)
    {
        var stats = controller.Stats;

        if (stats.IsHungry)
            return new SeekFoodState();

        if (stats.IsThirsty)
            return new SeekWaterState();

        if (stats.IsTired)
            return new SleepState();

        if (Time.time >= wanderTimer)
        {
            if (controller.Movement.TryGetRandomPoint(controller.transform.position, wanderTimer, out Vector3 point))
                controller.Movement.MoveTo(point);

            wanderTimer = Time.time + wanderInterval;
        }
        return this;
    }
}

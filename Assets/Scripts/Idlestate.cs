using UnityEngine;

public class Idlestate : IDinoState
{
    private float wanderTimer;
    private const float wanderInterval = 3f;
    private const float wanderRadius = 30f;

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

        //Debug.Log($"Idle tick - Hungry:{stats.IsHungry} Thirsty:{stats.IsThirsty} Tired:{stats.IsTired} Time:{Time.time} NextWander:{wanderTimer}");

        if (stats.IsHungry)
            return new SeekFoodState();

        if (stats.IsThirsty)
            return new SeekWaterState();

        if (stats.IsTired)
            return new SleepState();

        if (Time.time >= wanderTimer)
        {
            // bool found = controller.Movement.TryGetRandomPoint(controller.transform.position, wanderRadius, out Vector3 point);
            // Debug.Log($"TryGetRandomPoint success: {found}, point: {point}");
            // if (found) controller.Movement.MoveTo(point);

            if (controller.Movement.TryGetRandomPoint(controller.transform.position, wanderRadius, out Vector3 point))
                controller.Movement.MoveTo(point);

            wanderTimer = Time.time + wanderInterval;
        }
        return this;
    }
}

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

    public IDinoState Tick(DinoController dino)
    {
        var stats = dino.Stats;

        if (dino.Stats.IsHungry)
            return new SeekFoodState();

        if (dino.Stats.IsThirsty)
            return new SeekWaterState();

        if (dino.Stats.IsTired)
            return new SleepState();

        if (Time.time >= wanderTimer)
        {
            Vector3 pos = dino.transform.position;
            Vector3 origin = pos;
            float radius = 30f;

            if (dino.TryGetHerdCentre(out Vector3 centre))
            {
                radius = 12f;
                float comfort = dino.Config.herdComfort;

                if ((centre - pos).sqrMagnitude > comfort * comfort)
                    origin = Vector3.Lerp(pos, centre, 0.7f);
            }

            if (dino.Movement.TryGetRandomPoint(origin, radius, out Vector3 point))
                dino.Movement.MoveTo(point);

            wanderTimer = Time.time + 3f;
        }
        return this;
    }
}

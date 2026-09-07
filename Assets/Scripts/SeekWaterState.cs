using UnityEditor;
using UnityEngine;

public class SeekWaterState : IDinoState
{
    private WaterSource target;

    public void Enter(DinoController controller)
    {
        target = FindNearestWater(controller);
        if (target != null)
            controller.Movement.MoveTo(target.ClosestWaterSource(controller.transform.position));
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController controller)
    {
        if (target == null)
            return new Idlestate();

        if (controller.Movement.HasArrived)
            return new DrinkState(target);

        return this;
    }

    private WaterSource FindNearestWater(DinoController controller)
    {
        Collider[] hits = Physics.OverlapSphere(controller.transform.position, controller.Config.senseRange);

        WaterSource nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out WaterSource water))
                continue;

            float dist = Vector3.Distance(controller.transform.position, water.ClosestWaterSource(controller.transform.position));

            if (dist < nearestDistance)
            {
                nearestDistance = dist;
                nearest = water;
            }
        }
        return nearest;
    }
}

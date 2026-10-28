using UnityEditor;
using UnityEngine;

public class SeekWaterState : IDinoState
{
    private WaterSource target;
    private float nextScanTime;
    
    private const float ScanInterval = 1f;
    private const float SearchRadius = 40f;

    public void Enter(DinoController controller)
    {
        target = FindNearestWater(controller);
        if (target != null)
            controller.Movement.MoveTo(target.ClosestWaterSource(controller.transform.position));
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController  c)
    {
        if (!c.Stats.IsThirsty)
            return new Idlestate();

        if (target == null && Time.time >= nextScanTime)
        {
            target = FindNearestWater(c);
            nextScanTime = Time.time + ScanInterval;

            if (target != null)
                c.Movement.MoveTo(target.transform.position);
        }

        if (target == null)
        {
            if (c.Movement.HasArrived && c.Movement.TryGetRandomPoint(c.transform.position, SearchRadius, out Vector3 p))
                c.Movement.MoveTo(p);

            return this;
        }

        if (c.Movement.HasArrived)
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

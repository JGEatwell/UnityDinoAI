using Mono.Cecil.Cil;
using UnityEngine;

public class SeekFoodState : IDinoState
{
    private FoodSource target;

    public void Enter(DinoController controller)
    {
        target = FindNearestFood(controller);

        if (target != null)
            controller.Movement.MoveTo(target.transform.position);
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController controller)
    {
        if (target == null || !target.HasFood)
            return new Idlestate();

        if (controller.Movement.HasArrived)
            return new EatState(target);

        return this;
    }

    private FoodSource FindNearestFood(DinoController controller)
    {
        Collider[] hits = Physics.OverlapSphere(controller.transform.position, controller.Config.senseRange);

        FoodSource nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out FoodSource food) || !food.HasFood)
                continue;
            
            float dist = Vector3.Distance(controller.transform.position, food.transform.position);
            if (dist < nearestDistance)
            {
                nearestDistance = dist;
                nearest = food;
            }
        }
        return nearest;
    }
}

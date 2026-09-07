using UnityEngine;

public class EatState : IDinoState
{
    private readonly FoodSource food;
    private const float ConsumptionRatePerSecond = 20f;

    public EatState(FoodSource food)
    {
        this.food = food;
    }

    public void Enter(DinoController controller)
    {
        controller.Movement.Stop();
        //controller.Animator?.SetTrigger("Eat");
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController controller)
    {
        if (food == null || !food.HasFood)
            return new Idlestate();

        float foodConsumed = food.Consume(ConsumptionRatePerSecond * Time.deltaTime);
        controller.Stats.Eat(foodConsumed);

        if (!controller.Stats.IsHungry)
            return new Idlestate();

        return this;
    }
}

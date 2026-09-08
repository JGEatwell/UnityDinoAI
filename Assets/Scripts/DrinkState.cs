using UnityEngine;

public class DrinkState : IDinoState
{
    private readonly WaterSource water;
    private const float DrinkRatePerSecond = 20f;

    public DrinkState(WaterSource water)
    {
        this.water = water;
    }

    public void Enter(DinoController controller)
    {
        controller.Movement.Stop();
    }

    public void Exit(DinoController controller) {}

    public IDinoState Tick(DinoController controller)
    {
        controller.Stats.Drink(DrinkRatePerSecond * Time.deltaTime);

        if (!controller.Stats.IsThirsty)
            return new Idlestate();

        return this;
    }
}

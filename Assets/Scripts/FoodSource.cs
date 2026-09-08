using System;
using UnityEngine;

public class FoodSource : MonoBehaviour
{
    [SerializeField]
    private float hungerRegenAvailable = 100f;
    [SerializeField]
    private float regenRatePerSecond = 0.5f;
    [SerializeField]
    private float maximumHungerRegen = 100f;

    public bool HasFood => hungerRegenAvailable > 0f;

    private void Update()
    {
        if (regenRatePerSecond > 0f && hungerRegenAvailable < maximumHungerRegen)
            hungerRegenAvailable = MathF.Min(maximumHungerRegen, hungerRegenAvailable + regenRatePerSecond * Time.deltaTime);
    }

    public float Consume(float amount)
    {
        float taken = Mathf.Min(amount, hungerRegenAvailable);
        hungerRegenAvailable -= taken;
        return taken;
    }
}

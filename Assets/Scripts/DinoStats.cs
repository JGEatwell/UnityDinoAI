using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(DinoController))]
public class DinoStats : MonoBehaviour
{
    [SerializeField]
    private DinoConfig config;
    private float hungerRate;
    private float thirstRate;
    private float tiredRate;
/*-----------------------------------------------------------------------*/
    public float Health { get; private set;}
    public float Hunger { get; private set;}
    public float Thirst { get; private set;}
    public float Tiredness { get; private set;}
/*-----------------------------------------------------------------------*/
   public bool IsHungry => Hunger <= config.hungerThreshhold;
   public bool IsThirsty => Thirst <= config.thirstThreshhold;
   public bool IsTired => Tiredness <= config.tiredThreshhold;
   public bool IsDead => Health <= 0f;

   public event Action OnDeath;

    public void Initialize(DinoConfig cfg)
    {
        config = cfg;
        Health = config.maxhealth;

        Hunger = Random.Range(config.startingRange.x, config.startingRange.y);
        Thirst = Random.Range(config.startingRange.x, config.startingRange.y);
        Tiredness = Random.Range(config.startingRange.x, config.startingRange.y);

        hungerRate = config.hungerDecay * Vary();
        thirstRate = config.thirstDecay * Vary();
        tiredRate = config.tiredDecay * Vary();
        Debug.Log($"{name} init: hunger rate {hungerRate}");
    }
   
    // Update is called once per frame
    private void Update()
    {
        if (IsDead)
            return;

        Hunger = Mathf.Max(0f, Hunger - hungerRate * Time.deltaTime);
        Thirst = Mathf.Max(0f, Thirst - thirstRate * Time. deltaTime);
        Tiredness = Mathf.Max(0f, Tiredness - tiredRate * Time.deltaTime);

        if (Hunger <= 0 || Thirst <= 0f)
            ApplyDamage(config.starveDPS * Time.deltaTime);
    }


    public void Eat(float amount) => Hunger = Mathf.Min(100f, Hunger + amount);
    public void Drink(float amount) => Thirst = Mathf.Min(100, Thirst + amount);
    public void Rest(float amount) => Tiredness = Mathf.Min(100, Tiredness + amount);

    public void ApplyDamage(float amount)
    {
        Health = Mathf.Max(0f, Health - amount);
        if (Health <= 0f)
            OnDeath?.Invoke();

    }

    private float Vary() => 1f + Random.Range(-config.decayVariance, config.decayVariance);
    
}

using UnityEngine;

[CreateAssetMenu(fileName = "NewDinoConfig", menuName = "Dinosaurs/Dinosaur Config")]
public class DinoConfig : ScriptableObject
{
    public enum DieteryType{
        Herbivore,
        Carnivore,
        Omnivore
    }

    [Header("Identity")]
    public string dinoTypeName = "Stegosaurus";
    public DieteryType diet = DieteryType.Herbivore;

    [Header("Move Speed")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float rotationSpeed = 120f;

    [Header("Decay Rate")]
    public float hungerDecay = 0.5f;
    public float thirstDecay = 0.7f;
    public float tiredDecay = 0.3f;

    [Header("Thresholds")]
    public float hungerThreshhold = 40f;
    public float thirstThreshhold = 40f;
    public float tiredThreshhold = 30f;

    [Header("Satiation")]
    public float hungerSatiation = 80f;
    public float thirstSatiation = 80f;
    public float sleepSatiation = 80f;

    [Header("Senses")]
    public float senseRange = 25f;

    [Header("Health")]
    public float maxhealth = 100f;
    public float starveDPS = 1f;
}

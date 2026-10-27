using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


[RequireComponent(typeof(DinoController))]
public class DinoDebugLabel : MonoBehaviour
{
   public static bool Visible = true;
   private static int lastToggleFrame = -1;
   private static Texture2D whiteTex;

   [SerializeField] 
   private float heightOffset = 3.5f;
   [SerializeField]
   private float maxDistance = 60f;
   [SerializeField]
   private Vector2 boxSize = new Vector2(150f, 78f);

   private DinoController controller;
   private Camera cam;
   private GUIStyle stateStyle;

    private void Awake()
    {
        controller = GetComponent<DinoController>();

        if (whiteTex == null)
        {
            whiteTex = new Texture2D(1,1);
            whiteTex.SetPixel(0,0, Color.white);
            whiteTex.Apply();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame && lastToggleFrame != Time.frameCount)
        {
            lastToggleFrame = Time.frameCount;
            Visible = !Visible;
        }
    }

    private void OnGUI()
    {
        if (!Visible || Event.current.type != EventType.Repaint)
            return;

        if (cam == null)
            cam = Camera.main;
        
        if (cam == null)
            return;
        
        Vector3 screen = cam.WorldToScreenPoint(transform.position + Vector3.up * heightOffset);
        if (screen.z <= 0f || screen.z > maxDistance)
            return;

        if (stateStyle == null)
        {
            stateStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white}
            };
        }

        float x = screen.x - boxSize.x * 0.5f;
        float y = Screen.height - screen.y - boxSize.y;
 
        // Background
        GUI.color = new Color(0f, 0f, 0f, 0.6f);
        GUI.DrawTexture(new Rect(x, y, boxSize.x, boxSize.y), whiteTex);
        GUI.color = Color.white;
 
        // State name
        GUI.Label(new Rect(x, y + 2f, boxSize.x, 22f), controller.CurrentStateName, stateStyle);
 
        // Need bars
        var stats = controller.Stats;
        DrawBar(x + 8f, y + 28f, boxSize.x - 16f, stats.Hunger, new Color(0.9f, 0.6f, 0.2f));
        DrawBar(x + 8f, y + 44f, boxSize.x - 16f, stats.Thirst, new Color(0.3f, 0.6f, 1f));
        DrawBar(x + 8f, y + 60f, boxSize.x - 16f, stats.Tiredness, new Color(0.7f, 0.5f, 0.9f));

    }

    private static void DrawBar(float x, float y, float width, float value01x100, Color fill)
    {
        const float h = 10f;
        float t = Mathf.Clamp01(value01x100 / 100f);
 
        GUI.color = new Color(1f, 1f, 1f, 0.15f);
        GUI.DrawTexture(new Rect(x, y, width, h), whiteTex);
 
        GUI.color = fill;
        GUI.DrawTexture(new Rect(x, y, width * t, h), whiteTex);
 
        GUI.color = Color.white;
    }

}

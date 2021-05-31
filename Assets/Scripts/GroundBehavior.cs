using UnityEngine;


public class GroundBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isAlive = false;


    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            UpdateColor(isAlive?Color.white:Color.black);
        }
    }

    public void UpdateColor(Color color)
    {
        spriteRenderer.color = color;
    }
}


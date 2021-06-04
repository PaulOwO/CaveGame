
using UnityEngine;
public struct Cell
{
    public bool isAlive;

    public Cell(bool isAlive)
    {
        this.isAlive = isAlive;
    }
}
public class CellBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    private bool isAlive = true;


    private void Start()
    {
        spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
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

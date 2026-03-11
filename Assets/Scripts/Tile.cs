using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color mainColor, offsetColor;
    [SerializeField] private SpriteRenderer tileRenderer;

    public void Init(bool isOffset){
        tileRenderer.color = isOffset? offsetColor : mainColor;
    }
}

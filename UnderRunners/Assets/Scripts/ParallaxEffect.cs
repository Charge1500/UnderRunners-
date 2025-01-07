using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;

    void Awake(){
        material=GetComponent<SpriteRenderer>().material;
    }
    void Update(){
        offset=velocidadMovimiento*Time.deltaTime;
        material.mainTextureOffset +=offset; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScreenManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public SpriteRenderer[] initialSprites;
    public CanvasGroup initialCanvas; // Canvas Antesala
    public CanvasGroup menuCanvas;    // Canvas del menú principal
    public GameObject opcionesDeEmpezar;
    public float transitionDuration = 1f; // Duración de la transición

    private bool transitioning = false;

    void Start(){
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (!transitioning && Input.anyKeyDown)
        {
            StartCoroutine(TransitionToMenu());
        }
    }

    private IEnumerator TransitionToMenu()
    {
        transitioning = true;
        menuPrincipal.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            initialCanvas.alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);

            float progress = Mathf.Clamp01(elapsedTime / transitionDuration);

            // Ajusta la opacidad de los sprites
            foreach (SpriteRenderer sprite in initialSprites)
            {
                Color color = sprite.color;
                color.a = Mathf.Lerp(1f, 0f, progress); // Reduce alpha de 1 a 0
                sprite.color = color;
            }
            if (progress >= 1f)
            {
                foreach (SpriteRenderer sprite in initialSprites)
                {
                    sprite.gameObject.SetActive(false); // Opcional: desactiva los sprites
                }
            }
            yield return null;
        }
        initialCanvas.alpha = 0;
        initialCanvas.gameObject.SetActive(false);

        menuCanvas.gameObject.SetActive(true);
        opcionesDeEmpezar.SetActive(false);
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            menuCanvas.alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);

            yield return null;
        }
        menuCanvas.alpha = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuEmpezar : MonoBehaviour
{
    public GameObject[] prefabsPlayers;
    public GameObject[] selectedPlayers;
    public Button[] selectedPlayersButtons;
    public GameObject[] buttonObject; 
    public TMP_Text buttonIniciarText;

    public int numeroDeJugadorSeleccionado;
    public GameObject menuPrincipal;
    public GameObject reglas;
    public GameObject menuPrincipalSprite;
    [SerializeField] private GameObject opcionesDeInicio;
    [SerializeField] private TextMeshProUGUI players; 
    private int numPlayers;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI pts;
    private int secTime;
    private int ptsToWin;


    //Pasando a la seleccion de personaje
    public CanvasGroup selectionCharacter; 
    public CanvasGroup menuCanvas;    
    public float transitionDuration = 1f; 

    private bool transitioning = false; 

    //
    //Reglas
    public string[] textoReglas=new string[3];
    public TextMeshProUGUI textoReglasVisual;
    public int textoActual=0;
    void Start()
    {
        numeroDeJugadorSeleccionado=0;
        numPlayers = 2; 
        secTime = 10;  
        ptsToWin = 10;  

        players.text = numPlayers.ToString();
        time.text = secTime.ToString();

        textoReglas[0]="En este emocionante juego inspirado en el universo de Undertale, los jugadores se enfrentarán en un laberinto lleno de trampas mortales, objetos que pueden cambiar el curso de la partida y habilidades especiales que garantizan estrategias únicas. El objetivo es simple, pero peligroso: llegar al centro del laberinto, tomar a Flowey y sobrevivir durante 10 turnos consecutivos con ella equipada.";
        textoReglas[1]="El juego se desarrolla en turnos.Cada turno tiene un límite de tiempo. Si el jugador no realiza ninguna acción antes de que se acabe el tiempo, el turno finaliza automáticamente.Las acciones disponibles son las siguientes: \n-Usar habilidades especiales: Activa habilidades únicas del personaje, siempre que no estén en enfriamiento.\n-En cada turno, un jugador puede realizar un ataque y moverse libremente por el mapa hasta que se acabe el tiempo de su turno.\n Si deseas conocer más sobre el laberinto,sus trampas objetos y personajes visita el museo.";
        textoReglas[2]="Agradecimientos\n...";
    }

    void Update(){
        if (transitioning){
            if(selectedPlayers[selectedPlayers.Length-1]!=null){
                foreach (Button button in selectedPlayersButtons) { 
                    button.interactable = false;
                }
                buttonObject[7].SetActive(true);
                EventSystem.current.SetSelectedGameObject(null); 
                EventSystem.current.SetSelectedGameObject(buttonObject[7]);
                selectedPlayersButtons[7].interactable = true;
            }
        }
    }

    //CONTINUAR
    public void Continuar()
    {   
        if(!transitioning){
            selectedPlayers=new GameObject[numPlayers];
            StartCoroutine(TransitionToCharacterSelection());   
        }
    }

    private IEnumerator TransitionToCharacterSelection()
    {
        transitioning = true;
        
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            menuCanvas.alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);

            float progress = Mathf.Clamp01(elapsedTime / transitionDuration);
            yield return null;
        }

        menuCanvas.alpha = 0;
        menuCanvas.gameObject.SetActive(false);

        selectionCharacter.gameObject.SetActive(true);
        menuPrincipalSprite.SetActive(false);

        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            selectionCharacter.alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);

            yield return null;
        }
        selectionCharacter.alpha = 1;

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[0]); 
    }
    //****************************************
    public void MasJugadores()
    {
        Debug.Log("Aumentando jugadores...");
        if (numPlayers < 4)
        {
            numPlayers++;
            players.text = numPlayers.ToString();
            
        }
    }

    public void MenosJugadores()
    {
        Debug.Log("Disminuyendo jugadores...");
        if (numPlayers > 2)
        {
            numPlayers--;
            players.text = numPlayers.ToString();
        }
    }

    public void MasTiempo()
    {
        Debug.Log("Aumentando tiempo...");
        if (secTime < 20)
        {
            secTime++;
            time.text = secTime.ToString();
        }
    }

    public void MenosTiempo()
    {
        Debug.Log("Disminuyendo tiempo...");
        if (secTime > 5)
        {
            secTime--;
            time.text = secTime.ToString();
        }
    }
    public void MasPuntos()
    {
        if (ptsToWin < 15)
        {
            ptsToWin++;
            pts.text = ptsToWin.ToString();
        }
    }

    public void MenosPuntos()
    {
        if (ptsToWin > 3)
        {
            ptsToWin--;
            pts.text = ptsToWin.ToString();
        }
    }

    public void Empezar(){
        menuPrincipal.SetActive(false);
        opcionesDeInicio.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[8]);
    }

    public void Cancelar(){
        menuPrincipal.SetActive(true);
        opcionesDeInicio.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }

    public void IniciarJuego(){
        GameData.Instance.selectedPlayers = selectedPlayers;
        GameData.Instance.turnTime = secTime;
        GameData.Instance.ptsToWin = ptsToWin;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Museo(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Museo");
    }
    public void Reglas(){
        menuPrincipal.SetActive(false);
        reglas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[10]);
    }
    public void Siguiente(){
        textoActual++;
        textoReglasVisual.text=textoReglas[textoActual];
        if(textoActual==textoReglas.Length-1){
            buttonObject[11].SetActive(false);
        }
        if(textoActual>0){
            buttonObject[12].SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }
    public void Anterior(){
        textoActual--;
        textoReglasVisual.text=textoReglas[textoActual];
        if(textoActual==0){
            buttonObject[12].SetActive(false);
        }
        if(textoActual<textoReglas.Length-1){
            buttonObject[11].SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }
    public void Regresar(){
        reglas.SetActive(false);
        menuPrincipal.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }
    public void Salir(){
        Application.Quit();
    }

    public void SeleccionarFrisk(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[0];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[0],0);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarAlphys(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[1];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[1],1);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarSans(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[2];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[2],2);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarPapyrus(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[3];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[3],3);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarUndyne(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[4];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[4],4);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarToriel(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[5];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[5],5);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarAsgore(){
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[6];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[6],6);
        SeleccionarPersonajeDisponible();
    }

    private void SeleccionarPersonajeDisponible(){
        for(int i=0;i<7;i++) { 
            if(selectedPlayersButtons[i].interactable == true){
                EventSystem.current.SetSelectedGameObject(null); 
                EventSystem.current.SetSelectedGameObject(buttonObject[i]);
                break;
            }
        }
    }

    private void DisableButtonFromNavigation(Button button, int index)
    {
        button.interactable = false;

        Navigation previous;
        Navigation next;
        Navigation vertical;

        if (index == 0){
            int intPrevious=ChequearPreviousIndex(6);
            int intNext=ChequearNextIndex(1);
            previous = selectedPlayersButtons[intPrevious].navigation;
            next = selectedPlayersButtons[intNext].navigation;
            vertical = selectedPlayersButtons[4].navigation;

            previous.selectOnRight = selectedPlayersButtons[intNext];
            selectedPlayersButtons[intPrevious].navigation = previous;

            next.selectOnLeft = selectedPlayersButtons[intPrevious];
            selectedPlayersButtons[intNext].navigation = next;

            vertical.selectOnDown = null;
            vertical.selectOnUp = null;
            selectedPlayersButtons[4].navigation = vertical;
        }else if (index == 6){
            int intPrevious=ChequearPreviousIndex(5);
            int intNext=ChequearNextIndex(0);
            previous = selectedPlayersButtons[intPrevious].navigation;
            next = selectedPlayersButtons[intNext].navigation;
            vertical = selectedPlayersButtons[2].navigation;

            previous.selectOnRight = selectedPlayersButtons[intNext];
            selectedPlayersButtons[intPrevious].navigation = previous;

            next.selectOnLeft = selectedPlayersButtons[intPrevious];
            selectedPlayersButtons[intNext].navigation = next;

            vertical.selectOnDown = null;
            vertical.selectOnUp = null;
            selectedPlayersButtons[2].navigation = vertical;
        }else if(index == 3){
            int intPrevious=ChequearPreviousIndex(2);
            int intNext=ChequearNextIndex(4);
            previous = selectedPlayersButtons[intPrevious].navigation;
            next = selectedPlayersButtons[intNext].navigation;

            previous.selectOnRight = selectedPlayersButtons[intNext];
            selectedPlayersButtons[intPrevious].navigation = previous;

            next.selectOnLeft = selectedPlayersButtons[intPrevious];
            selectedPlayersButtons[intNext].navigation = next;
        }else{
            int intPrevious=ChequearPreviousIndex(index-1);
            int intNext=ChequearNextIndex(index+1);
            int indexVer=(index < 3) ? index + 4 : index - 4;
            previous = selectedPlayersButtons[intPrevious].navigation;
            next = selectedPlayersButtons[intNext].navigation;
            vertical = selectedPlayersButtons[indexVer].navigation;

            previous.selectOnRight = selectedPlayersButtons[intNext];
            selectedPlayersButtons[intPrevious].navigation = previous;

            next.selectOnLeft = selectedPlayersButtons[intPrevious];
            selectedPlayersButtons[intNext].navigation = next;

            vertical.selectOnDown = null;
            vertical.selectOnUp = null;
            selectedPlayersButtons[indexVer].navigation = vertical;
        }
    }

    private int ChequearPreviousIndex(int index){
        for(int i=0;i<3;i++){
            if(selectedPlayersButtons[index].interactable){
                return index;
            } else{
                if(index==0){
                    index=6;
                }else{
                    index--;
                }
            }
        }
        return index;
    }

    private int ChequearNextIndex(int index){
        for(int i=0;i<3;i++){
            if(selectedPlayersButtons[index].interactable){
                return index;
            } else{
                if(index==6){
                    index=0;
                }else{
                    index++;
                }
            }
        }
        return index;
    }
}
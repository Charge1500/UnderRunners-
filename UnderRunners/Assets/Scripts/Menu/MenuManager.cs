using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

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
    [SerializeField] private GameObject opciones;
    [SerializeField] private TextMeshProUGUI players; 
    private int numPlayers;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI pts;
    [SerializeField] private TextMeshProUGUI sizText;
    private int secTime;
    private int ptsToWin;
    private int size;
    private bool pantallaFull=true;
    public GameObject pantallaCompletaCheck;
    //Pasando a la seleccion de personaje
    public CanvasGroup selectionCharacter; 
    public CanvasGroup menuCanvas;    
    public float transitionDuration = 1f; 

    private bool transitioning = false; 

    //
    //Reglas
    private string[] textoReglas=new string[6];
    public TextMeshProUGUI textoReglasVisual;
    public int textoActual=0;
    //Audio
    public AudioClip[] tracks;
    private AudioSource audioSource;
    public Slider slider;   
    void Start()
    {
        numeroDeJugadorSeleccionado=0;
        numPlayers = 2; 
        secTime = 10;  
        ptsToWin = 3;  
        size = 15;

        players.text = numPlayers.ToString();
        time.text = secTime.ToString();

        textoReglas[0]="Captura del Rubí: El Rubí del Centro debe ser tomado por un jugador y mantenido durante X turnos para ganar puntos. Si el jugador muere mientras tiene el Rubí, éste volverá al centro del laberinto.\nVictoria: El jugador que logre acumular la cantidad de puntos requerida primero, será el ganador del juego.";
        textoReglas[1]="Turnos y Tiempo: Cada jugador tiene un tiempo limitado para realizar su turno. Durante el turno puede realizar las siguientes acciones\n-Usar habilidades especiales: Activa habilidades únicas del personaje, siempre que no estén en enfriamiento.\n-En cada turno, un jugador puede realizar un ataque a todos los jugadores cerca de el y moverse libremente por el mapa hasta que se acabe el tiempo de su turno.\n Si deseas conocer más sobre el laberinto,sus trampas objetos y personajes visita el museo.";
        textoReglas[2]="En el misterioso mundo subterráneo de Undertale, una antigua leyenda habla de un rubí mágico escondido en el corazón de un peligroso laberinto. Este Rubí, conocido como el \"Corazón de las Almas\", posee un poder inimaginable que sólo se desatará en manos de quien demuestre ser verdaderamente digno.\nLos personajes icónicos de Undertale - Sans, Papyrus, Undyne, Alphys, Toriel y otros - se reúnen para participar en un reto épico. Al enterarse del legendario Rubí, cada uno de ellos está determinado a obtenerlo, no solo para demostrar su valentía y habilidades, sino también para proteger el mundo subterráneo de aquellos con intenciones siniestras.";
        textoReglas[3]="Con trampas traicioneras en cada esquina y objetos misteriosos que pueden cambiar el curso del juego, cada turno es una carrera contra el tiempo y contra los demás jugadores. Solo el más astuto y valiente puede tomar el Rubí del Centro y mantenerlo el tiempo suficiente para desatar su verdadero poder.\n¿Serás tú el héroe que el mundo subterráneo necesita? La batalla por el Corazón de las Almas está a punto de comenzar...";
        textoReglas[4]="Controles(Funcionan las flechas también):\n\nW:Mover hacia arriba\n\nS:Mover hacia abajo\n\nD:Mover hacia la derecha\n\nA:Mover hacia la izquierda\n\n Ataque:E   Habilidad:Q\nMENU en el juego:ESC";
        textoReglas[5]="Agradecimientos:\nLuis Ronaldo Benitez\nJavier Fontes\nAilema Matos\nAbner Abreu\nAdrian Estevez\nKarla Ramirez\n\n\nProyecto creado por:\n Raul Roberto Espinosa Poma";
        audioSource=GetComponent<AudioSource>();
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
        if(reglas.activeSelf){
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                if(textoActual<textoReglas.Length-1){
                    Siguiente();
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                if(textoActual>0){
                    Anterior();
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape)){
                Regresar();
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
        audioSource.Play();
        
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
        MusicManager.Instance.ChangeTrack(tracks[2]); 
    }
    //****************************************
    public void MasJugadores()
    { 
        if (numPlayers < 4)
        {
            numPlayers++;
            players.text = numPlayers.ToString();
            
        }
    }

    public void MenosJugadores()
    {
        if (numPlayers > 2)
        {
            numPlayers--;
            players.text = numPlayers.ToString();
        }
    }

    public void MasTiempo()
    {
        if (secTime < 20)
        {
            secTime++;
            time.text = secTime.ToString();
        }
    }

    public void MenosTiempo()
    {
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
    public void MasGrande()
    {
        if (size < 30)
        {
            size++;
            sizText.text = size.ToString();
        }
    }

    public void MasPeque()
    {
        if (size > 15)
        {
            size--;
            sizText.text = size.ToString();
        }
    }

    public void Empezar(){
        audioSource.Play();

        menuPrincipal.SetActive(false);
        opcionesDeInicio.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[8]);
    }

    public void Cancelar(){
        audioSource.Play();

        menuPrincipal.SetActive(true);
        opcionesDeInicio.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }

    public void IniciarJuego(){
        audioSource.Play();

        GameData.Instance.selectedPlayers = selectedPlayers;
        GameData.Instance.turnTime = secTime;
        GameData.Instance.ptsToWin = ptsToWin;
        GameData.Instance.size = size;
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnderMaze");
        MusicManager.Instance.ChangeTrack(tracks[0]);
    }

    public void Museo(){
        audioSource.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Museo");
        MusicManager.Instance.ChangeTrack(tracks[1]);
    }

    public void Opciones(){
        audioSource.Play();

        menuPrincipal.SetActive(false);
        opciones.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[13]);
    }

    public void PantallaCompleta(){
        if(pantallaFull){
            pantallaCompletaCheck.SetActive(false);
            pantallaFull=false;
            Screen.fullScreen = pantallaFull;
        } else{
            pantallaCompletaCheck.SetActive(true);
            pantallaFull=true;
            Screen.fullScreen = pantallaFull;
        }
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[13]);
    }

    public void CambiarVolumen(float volumen){
        volumen=slider.value;
        MusicManager.Instance.SetVolume(volumen);
    }

    public void RegresarOpciones(){
        audioSource.Play();

        menuPrincipal.SetActive(true);
        opciones.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }
    public void Reglas(){
        audioSource.Play();

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
        
    }
    public void Regresar(){
        audioSource.Play();

        reglas.SetActive(false);
        menuPrincipal.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(buttonObject[9]);
    }
    public void Salir(){
        Application.Quit();
    }

    public void SeleccionarFrisk(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[0];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[0],0);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarAlphys(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[1];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[1],1);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarSans(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[2];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[2],2);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarPapyrus(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[3];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[3],3);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarUndyne(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[4];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[4],4);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarToriel(){
        audioSource.Play();
        selectedPlayers[numeroDeJugadorSeleccionado]=prefabsPlayers[5];
        numeroDeJugadorSeleccionado++;
        DisableButtonFromNavigation(selectedPlayersButtons[5],5);
        SeleccionarPersonajeDisponible();
    }

    public void SeleccionarAsgore(){
        audioSource.Play();
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
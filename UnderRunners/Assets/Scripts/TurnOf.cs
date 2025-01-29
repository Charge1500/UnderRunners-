using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOf : MonoBehaviour
{
    //Interfaz
    public Image image;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI health;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI coldown;
    public TextMeshProUGUI puntuation;
    public Image imageDialog;
    public TextMeshProUGUI dialogText;
    //---------------------------------
    private Pencil pencil;
    public Button habButton;
    public Button attackButton;
    public List<Player> turns = new List<Player>();
    public int currentTurnIndex = 0;
    public float turnDuration = 10f; // Duración del turno en segundos
    private float turnTimer; // Temporizador del turno
    public TMP_Text turnTimerText; // Referencia al texto de UI para el temporizador

    public int puntuationToWin=5;
    public bool oneAttack=true;
    public bool gameEnded = false;
    public bool isWaitingForAbilityClick = false;
    public GameObject winScreen;
    public GameObject endTurnScreen;
    public Image winnerImage;
    public TextMeshProUGUI winPlayer;
    public Animator floweyAnimator;
    // Cursor personalizado
    public Texture2D abilityCursor; // Arrastra tu textura personalizada aquí
    public Vector2 abilityCursorHotspot;// Punto de anclaje
    private Texture2D defaultCursor; // Para guardar el cursor por defecto
    // Cámara
    public Camera mainCamera;
    public float cameraFollowSpeed = 5f;
    public float cameraZoom = 1f;
    private float originalCameraZoom;
    public float cameraOffsetX = -0.4f;
    //Audio
    public AudioClip[] audioClips;
    private bool isMegalovania=false;
    private bool oneTime=false;
    public AudioClip megalovania;
    public AudioClip determination;

    void Awake()
    {
        pencil = GetComponent<Pencil>();
        defaultCursor = null;
        abilityCursorHotspot = new Vector2(abilityCursor.width / 2, abilityCursor.height); // Punto de anclaje
    }
    void Start(){
        turnDuration=GameData.Instance.turnTime;
        puntuationToWin=GameData.Instance.ptsToWin;
        originalCameraZoom = mainCamera.orthographicSize;
    }
    void Update(){
        if(!turns[currentTurnIndex].isUsingHab){
            if (turnTimer > 0)
            {
                turnTimer -= Time.deltaTime; // Reducir el temporizador en función del tiempo transcurrido
                turnTimerText.text = Mathf.Ceil(turnTimer).ToString(); // Actualizar el texto del temporizador solo con los segundos
            }else
            {
                NextTurn();
            }
        }
        if (isWaitingForAbilityClick)
        {
            Cursor.SetCursor(abilityCursor, abilityCursorHotspot, CursorMode.Auto);
            if (Input.GetMouseButtonDown(0))
            {
                HandleAbilityClick();
            }
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
        
        // Seguir al jugador actual con la cámara
        FollowCurrentPlayer();
    }
    public void AssignPlayerOrder()
    {
        turns.Clear();
        if (pencil.player1 != null)
        {
            turns.Add(pencil.player1.GetComponent<Player>());
            turns[0].isTurn = false;
        }
        if (pencil.player2 != null)
        {
            turns.Add(pencil.player2.GetComponent<Player>());
            turns[1].isTurn = false;
        }
        if (pencil.player3 != null)
        {
            turns.Add(pencil.player3.GetComponent<Player>());
            turns[2].isTurn = false;
        }
        if (pencil.player4 != null)
        {
            turns.Add(pencil.player4.GetComponent<Player>());
            turns[3].isTurn = false;
        }
    }

    public void StartTurn()
    {
        if(!turns[currentTurnIndex].stun){
            CheckWin();
            oneAttack=true;
            Rigidbody2D currentPlayerRigidbody = turns[currentTurnIndex].GetComponent<Rigidbody2D>();
            currentPlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            UpdateUI();
            UpdateDialogText(turns[currentTurnIndex].dialogStartTurn,0);
            turns[currentTurnIndex].isTurn = true; // Comienza el turno
            turnTimer = turnDuration; // Restablecer el temporizador
            UpdateHabButton();
        } else{
            turns[currentTurnIndex].stun = false;
            NextNextTurn();
        }
    }


    public void NextTurn()
    {
        isWaitingForAbilityClick=false;
        Time.timeScale = 0f; 
        turns[currentTurnIndex].isTurn = false;
        endTurnScreen.SetActive(true);
        
    }
    public void NextNextTurn(){
        Rigidbody2D currentPlayerRigidbody = turns[currentTurnIndex].GetComponent<Rigidbody2D>();
        currentPlayerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll; // Congela todo
        attackButton.interactable = (turns[currentTurnIndex].playersToAttack.Count > 0||turns[currentTurnIndex].friskCopy) && oneAttack;;
        turns[currentTurnIndex].RestoreOriginalStats();
        currentTurnIndex = (currentTurnIndex + 1) % turns.Count; // Pasa al siguiente jugador
        if(turns[currentTurnIndex].currentColdown > 0) turns[currentTurnIndex].currentColdown--;
        habButton.interactable =(turns[currentTurnIndex].currentColdown == 0) ?  true : false;
        ResetCameraZoom(); // Restaurar el zoom de la cámara antes de iniciar el siguiente turno
        StartTurn(); // Comienza el turno del siguiente jugador
    }

    private void FollowCurrentPlayer(){
        Player currentPlayer = turns[currentTurnIndex];
        Vector3 targetPosition = new Vector3(
        currentPlayer.transform.position.x,
        currentPlayer.transform.position.y,
        -10
        );

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);

        // Ajustar el zoom
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraZoom, Time.deltaTime * cameraFollowSpeed);
    }

    private void ResetCameraZoom(){
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalCameraZoom, Time.deltaTime * cameraFollowSpeed);
    }
    public void IncreaseTurnTime(float additionalTime){ 
        turnTimer += additionalTime; 
    }
    public void CheckWin(){
        if(turns[currentTurnIndex].hasRuby && !turns[currentTurnIndex].stun){
            turns[currentTurnIndex].puntuation+=1;
        }
        if(turns[currentTurnIndex].puntuation==puntuationToWin){ 
            MusicManager.Instance.ChangeTrack(determination);      
            winScreen.SetActive(true);
            winnerImage.sprite = turns[currentTurnIndex].imageWinner;
            gameEnded=true;
            winPlayer.text = $"{turns[currentTurnIndex].playerName} wins!";
        }
    }

    private void HandleAbilityClick()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hit.point, 0.6f);

        // Verifica si hay un jugador cerca 
        bool playerNearby = false; 
        foreach (Collider2D collider in colliders) { 
            if (collider.CompareTag("Player")) { 
                playerNearby = true; 
                break; 
            } 
        }

        if(turns[currentTurnIndex].playerName=="Undyne"){
            if (hit.collider != null && !hit.collider.CompareTag("Untagged"))
            {
                turns[currentTurnIndex].ActivateSkillOnPath(hit.collider.transform.position);
            }
            else
            {
                UpdateDialogText("A donde disparas tonto",1);
            }
        }else if(turns[currentTurnIndex].playerName=="Papyrus" || turns[currentTurnIndex].playerName=="Sans"){
            if (hit.collider != null && !hit.collider.CompareTag("Untagged") && !playerNearby)
            {
                turns[currentTurnIndex].ActivateSkillOnPath(hit.collider.transform.position);
            }
            else
            {
                UpdateDialogText("Hay un jugador cerca",1);
            }
        }

        isWaitingForAbilityClick = false; // Desactivar modo habilidad
    }


    private void UpdateHabButton()
    {
        habButton.onClick.RemoveAllListeners();
        habButton.onClick.AddListener(turns[currentTurnIndex].ActiveHab);
    }

    public void UpdateUI(){
        image.sprite = turns[currentTurnIndex].image;
        playerName.text = turns[currentTurnIndex].playerName;
        health.text = turns[currentTurnIndex].currentHealth.ToString();
        attack.text = turns[currentTurnIndex].currentAttack.ToString();
        speed.text = turns[currentTurnIndex].currentSpeed.ToString();
        coldown.text = turns[currentTurnIndex].currentColdown.ToString();
        puntuation.text = turns[currentTurnIndex].puntuation.ToString();
        if(turns[currentTurnIndex].puntuation==0){
            floweyAnimator.SetBool("Winning", false);
            floweyAnimator.SetBool("OnePoint", false);
        } else if(turns[currentTurnIndex].puntuation<puntuationToWin/2){
             floweyAnimator.SetBool("Winning", false);
            floweyAnimator.SetBool("OnePoint", true);
        } else if(turns[currentTurnIndex].puntuation>=puntuationToWin/2){
            isMegalovania=true;
            if(!oneTime && isMegalovania){
                MusicManager.Instance.ChangeTrack(megalovania);
                oneTime=true;
            }
            floweyAnimator.SetBool("OnePoint", true);
            floweyAnimator.SetBool("Winning", true);
        }
    }

    public void UpdateDialogText(string text,int n){
        dialogText.text=text;
        imageDialog.sprite = turns[currentTurnIndex].dialogImages[n];   
    }

    public void Attack(){
        if(attackButton.interactable && oneAttack){
            UpdateDialogText(turns[currentTurnIndex].dialogAttack,1);
            turns[currentTurnIndex].audioSource.PlayOneShot(audioClips[0]);
            for (int i = 0; i < turns[currentTurnIndex].playersToAttack.Count; i++){
                Player currentPlayer = turns[currentTurnIndex].playersToAttack[i];
                currentPlayer.TakeDamage(turns[currentTurnIndex].currentAttack);     
            }
            if(turns[currentTurnIndex].friskCopy){
                turns[currentTurnIndex].TakeDamage(turns[currentTurnIndex].currentAttack);
            }
            oneAttack=false;
            attackButton.interactable = false;
        }
    }

}

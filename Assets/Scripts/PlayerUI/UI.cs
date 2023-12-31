using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Assigned to UI object under Player Controller
/// Shows CounterSO, Restart button, Quit button, and either Pause or Game Over text.
/// </summary>
public class UI : MonoBehaviour
{
    #region Attributes
    [SerializeField] CounterSO MyScoreSO; // CounterSO scriptable object
    [Header("Input System")]
    [SerializeField] PlayerInput PlayerInput;
    [SerializeField] InputSystemUIInputModule PauseUIInputModule;
    [SerializeField] InputSystemUIInputModule GameUIInputModule;
    #region Score Tracking
    public static int GathererProductivity; // Total pollen gathered
    public static int WarriorProductivity; // Total Beentbarians killed
    public static int WorkerProductivity; // Total defenses built & nectar produced
    private int Score // Average beent productivity
    {
        get { return Mathf.FloorToInt(GathererProductivity * WarriorProductivity * WorkerProductivity / 3); }
    }
    #endregion
    #region Flags
    [SerializeField] bool IsPaused;
    [SerializeField] bool GameOver;
    #endregion
    #region UI Elements
    [Header("UI Elements")]
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject GameUI;
    [SerializeField] TextMeshProUGUI StateText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;
    [SerializeField] Slider Healthbar;
    private string ScoreTextString => "Score: " + Score.ToString("N0"); // Average beent productivity
    private string HighScoreString => "High Score: " + MyScoreSO.MaxCount.ToString("N0"); // High score from scriptable object
    #endregion
    #region Pollen and Nectar Counters
    [Header("Pollen and Nectar Counters")]
    [SerializeField] TextMeshProUGUI PollenText;
    [SerializeField] TextMeshProUGUI NectarText;
    #endregion
    #endregion
    #region Initialization
    private void Awake()
    {
        PauseUIInputModule.enabled = false; // Disable the Pause UI input module
        HighScoreText.text = HighScoreString; // Update high score
        GathererProductivity = 0; // Reset productivity
        WarriorProductivity = 0;
        WorkerProductivity = 0;
        IsPaused = false; // Reset flags
        GameOver = false;
    }
    private void Start()
    {
        UpdateUI(); // Update the UI
        PauseUI.SetActive(IsPaused); // Hide
        GameUI.SetActive(!IsPaused); // Show
        // Listeners for updating Pollen and Nectar counters
        Hive.Instance.OnPollenChange.AddListener(UpdatePollenText);
        Hive.Instance.OnNectarChange.AddListener(UpdateNectarText);
        Hive.Instance.OnHealthChange.AddListener(UpdateHealthbar);
        Healthbar.maxValue = Hive.Instance.Health;
        Healthbar.value = Healthbar.maxValue;
        //PlayerInput.uiInputModule = null; //<-- Commenting out this line of code enables the UI on start without needing to pause, however, it doesn't fix the issue where you need to pause and unpause when restarting to make the camera work -Ky'onna
        PlayerInput.uiInputModule = GameUIInputModule; // Send the game's input module to the player
    }
    #endregion
    #region Operations
    public void DoPause() // Pause method
    {
        if (!IsPaused) // Game is not paused
        {
            IsPaused = true; // Update flag
            Time.timeScale = 0; // Pause the game
            UpdateUI();
            GameUI.SetActive(!IsPaused); // Hide the Game UI
            PauseUI.SetActive(IsPaused); // Show the Pause UI
            PauseUIInputModule.enabled = true; // Enable the Pause UI input module
            PlayerInput.uiInputModule = PauseUIInputModule; // Send my input module to the player
        }
        else if (!GameOver) // Game is paused, and not over
        {
            IsPaused = false; // Update flag
            Time.timeScale = 1; // Unpause the game
            PauseUI.SetActive(IsPaused); // Hide the Pause UI
            GameUI.SetActive(!IsPaused); // Show the Game UI
            PlayerInput.uiInputModule = GameUIInputModule; // Send the game's input module to the player
        }
        else return; // Game is over, do nothing
    }
    private void UpdateUI() // Update the UI with the current score
    {
        ScoreText.text = ScoreTextString;
        StateText.text = GameOver ? "Game Over" : "Paused";
    }
    public void EndGame()
    {
        GameOver = true; // Update flag
        IsPaused = false; // Affirm unpause so the game can end
        DoPause(); // Pause the game
    }
    public void QuitOrRestart(bool QuitGame) // Called by buttons
    {
        MyScoreSO.CheckScore(Score); // Check the score
        if (QuitGame) Application.Quit(); // Quit the game
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
    }
    private void UpdatePollenText() // Update the pollen counter
    {
        PollenText.text = Hive.Instance.CurrentPollen.ToString();
    }
    private void UpdateNectarText() // Update the nectar counter
    {
        NectarText.text = Hive.Instance.CurrentNectar.ToString();
    }
    private void UpdateHealthbar() // Update the healthbar
    {
        Healthbar.value = Hive.Instance.Health;
    }
    #endregion
}
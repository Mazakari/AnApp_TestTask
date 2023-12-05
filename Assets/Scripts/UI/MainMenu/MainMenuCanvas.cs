using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour, ISavedProgress
{
    [Header("Level Selection Popup")]
    [SerializeField] private GameObject _levelSelectionPopup;
    [SerializeField] private Transform _levelSelectionContent;

    [Space(10)]
    [Header("Shop Settings")]
    [SerializeField] private Button _shopButton;
    public static event Action OnShopButtonPress;

    [Space(10)]
    [Header("Settings Popup")]
    [SerializeField] private GameObject _settingsPopup;

    [Space(10)]
    [Header("Developers Popup")]
    [SerializeField] private GameObject _developersPopup;

    private ILevelCellsService _levelCellsService;


    private void OnEnable()
    {
        _levelCellsService = AllServices.Container.Single<ILevelCellsService>();
        SettingsPopup.OnSettingsSaved += HideSettingsPopup;

        InitPopups();
    }
    private void Start() => 
        InitLevelsSelectionPopup();

    private void OnDisable()
    {
        SettingsPopup.OnSettingsSaved -= HideSettingsPopup;
    }

    public void ShowSelectLevelsPopup()
    {
        Debug.Log("ShowSelectLevelsPopup");
        _levelSelectionPopup.SetActive(true);
    }

    public void HideSelectLevelsPopup() => 
        _levelSelectionPopup.SetActive(false);
    public void LoadShop() =>
      OnShopButtonPress?.Invoke();

    public void ShowSettingsPopup() =>
        _settingsPopup.SetActive(true);


    public void QuitGame() => 
        Application.Quit();

    private void HideSettingsPopup() =>
        _settingsPopup.SetActive(false);

    private void InitPopups()
    {
        _levelSelectionPopup.SetActive(false);
        _settingsPopup.SetActive(false);
    }

    private void InitLevelsSelectionPopup()
    {
        for (int i = 0; i < _levelCellsService.Levels.Length; i++)
        {
            ParentCellAndResetScale(i);
        }
    }

    private void ParentCellAndResetScale(int i)
    {
        LevelCell cell = _levelCellsService.Levels[i];
        cell.transform.SetParent(_levelSelectionContent);
        cell.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void UpdateProgress(PlayerProgress progress) {}
    public void LoadProgress(PlayerProgress progress) => 
        OverwriteLevelCellsData(progress);

    private void OverwriteLevelCellsData(PlayerProgress progress)
    {
        int number;
        string name;
        bool locked;

        if (progress.gameData.levels.Count > 0)
        {
            for (int i = 0; i < progress.gameData.levels.Count; i++)
            {
                number = progress.gameData.levels[i].number;
                name = progress.gameData.levels[i].sceneName;
                locked = progress.gameData.levels[i].locked;

                _levelCellsService.Levels[i].InitLevelCell(number, name, locked);
            }
        }
    }
}

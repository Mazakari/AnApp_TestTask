using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour, ISavedProgress
{
    [Header("Level Selection Popup")]
    [SerializeField] private GameObject _levelSelectionPopup;
    [SerializeField] private Transform _levelSelectionContent;
    [SerializeField] private RectTransform[] _levelCellsParents;

    [Space(10)]
    [Header("Settings Popup")]
    [SerializeField] private GameObject _settingsPopup;

    [Space(10)]
    [Header("Daily Bonus Settings")]
    [SerializeField] private GameObject _dailyBonusPopup;

    [Space(10)]
    [Header("Shop Settings")]
    [SerializeField] private Button _shopButton;
    public static event Action OnShopButtonPress;
   

    private ILevelService _levelCellsService;
    private ISaveLoadService _saveLoadService;


    private void OnEnable()
    {
        CacheServices();

        SubscribeUICallbacks();

        InitPopups();
    }

    private void Start() => 
        InitLevelsSelectionPopup();

    private void OnDisable() => 
        UnsubscribeUICallbacks();

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
    public void UpdateProgress(PlayerProgress progress) => 
        _levelCellsService.UpdateLevelsProgress(progress);

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

                OverwriteLevel(number, name, locked, i);
            }
        }
    }

    private void InitPopups()
    {
        HideSettingsPopup();
        HideDailyBonusPopup();
        HideLevelSelectionPopup();
    }

    private void InitLevelsSelectionPopup()
    {
        try
        {
            for (int i = 0; i < _levelCellsService.Levels.Length; i++)
            {
                ParentCellAndResetScale(i);
                DeactivateLevelSpawnPosition(i);
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void ParentCellAndResetScale(int i)
    {
        LevelCell cell = _levelCellsService.Levels[i];
        RectTransform cellRectTransform = cell.GetComponent<RectTransform>();

        cell.transform.SetParent(_levelSelectionContent);
        cellRectTransform.localScale = Vector3.one;
        cellRectTransform.position = _levelCellsParents[i].position;
    }
    private void DeactivateLevelSpawnPosition(int index) =>
       _levelCellsParents[index].gameObject.SetActive(false);

    private void OverwriteLevel(int number, string name, bool locked, int index)
    {
        try
        {
            if (index >= _levelCellsService.Levels.Length) return;

            _levelCellsService.Levels[index].InitLevelCell(number, name, locked);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void UnlockNextLevelAndSaveProgress(string currentLevelName)
    {
        UnlockNextLevel(currentLevelName);

        SaveProgress();
    }
    private void UnlockNextLevel(string currentLevelName) => 
        _levelCellsService.UnlockNextLevel(currentLevelName);
    private void SaveProgress() => 
        _saveLoadService.SaveProgress();

    private void HideLevelSelectionPopup()
    {
        try
        {
            _levelSelectionPopup.SetActive(false);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
       
    }
    private void HideSettingsPopup()
    {
        try
        {
            _settingsPopup.SetActive(false);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void HideDailyBonusPopup()
    {
        try
        {
            _dailyBonusPopup.SetActive(false);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }

    }

    private void CacheServices()
    {
        _levelCellsService = AllServices.Container.Single<ILevelService>();
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }
    private void SubscribeUICallbacks()
    {
        SettingsPopup.OnSettingsSaved += HideSettingsPopup;
        DailyBonusPopup.OnCloseDailyBonusPopup += HideDailyBonusPopup;
        LevelCell.OnLevelCellPress += UnlockNextLevelAndSaveProgress;
    }
    private void UnsubscribeUICallbacks()
    {
        SettingsPopup.OnSettingsSaved -= HideSettingsPopup;
        DailyBonusPopup.OnCloseDailyBonusPopup -= HideDailyBonusPopup;
        LevelCell.OnLevelCellPress -= UnlockNextLevelAndSaveProgress;
    }

   
}

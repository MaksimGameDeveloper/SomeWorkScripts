using System;
using System.Collections.Generic;
using System.Linq;
using _Project.GameFeatures.SettingsWindow.Scripts;
using _Project.Scripts.Common.Extensions;
using _Project.Scripts.Gameplay.Common.Time;
using _Project.Scripts.Infrastructure.PlayerResources;
using _Project.Scripts.Infrastructure.SaveLoad;
using _Project.Scripts.Meta.Services.Quest.Configs;
using _Project.Scripts.Meta.Services.Quest.Factory;
using _Project.Scripts.Meta.Services.Quest.SaveData;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.Meta.Services.Quest
{
  public class QuestsService : IQuestsService
  {
    private const int Delay = 10000;

    private readonly IQuestFactory _questFactory;
    private readonly ISaveLoadService _saveLoadService;
    private readonly QuestCardCollectionPresenter _questCardCollectionPresenter;
    private readonly ChestsConfig _chestsConfig;
    private readonly ITimeService _time;
    private readonly ResourceService _resourceService;
    private readonly QuestsConfig _questsConfig;

    private bool _completedTask = false;

    public event Action OnTasksStateChanged;
    
    public bool HasCompletedTask => _questCardCollectionPresenter.HasReadyToCompleteTask;

    private QuestsSaveData QuestsSaveData => _saveLoadService.Progress.Quests;
    
    public QuestsService(IQuestFactory questFactory, ISaveLoadService saveLoadService, QuestCardCollectionPresenter questCardCollectionPresenter,
       ChestsConfig chestsConfig, ITimeService time, ResourceService resourceService, QuestsConfig questsConfig)
    {
      _questFactory = questFactory;
      _saveLoadService = saveLoadService;
      _questCardCollectionPresenter = questCardCollectionPresenter;
      _chestsConfig = chestsConfig;
      _time = time;
      _resourceService = resourceService;
      _questsConfig = questsConfig;
    }

    public void Initialize()
    {
      UpdateQuestsTime();
      
      if (QuestsSaveData.Quests.IsNullOrEmpty()) 
        InitializeBaseTask();
      else
        InitializeSavedTasks();

      if (QuestsSaveData.Chests.IsNullOrEmpty())
        InitializedChests();
      
      _questCardCollectionPresenter.Initialize();
    }
    
    public void UpdateQuestsState()
    {
      _questCardCollectionPresenter.UpdateCardsState();

      if (_questCardCollectionPresenter.HasReadyToCompleteTask && _completedTask == false)
      {
        _completedTask = true;
        OnTasksStateChanged?.Invoke();
      }
      else if (_questCardCollectionPresenter.HasReadyToCompleteTask == false && _completedTask)
      {
        _completedTask = false;
        OnTasksStateChanged?.Invoke();
      }
    }

    private async UniTaskVoid UpdateQuestsTime()
    {
      while (true)
      {
        if (QuestsSaveData.Quests.IsNullOrEmpty())
        {
          QuestsSaveData.WeeklyQuestsStartsTime = GetLastMonday(from: _time.UtcNow);
          QuestsSaveData.DailyQuestsStartsTime = GetLastDay(from: _time.UtcNow);
        }
      
        if (IsMondayPassed(from: QuestsSaveData.WeeklyQuestsStartsTime))
        {
          Debug.Log("Updated weekly quests!");
          QuestsSaveData.WeeklyQuestsStartsTime = GetLastMonday(from: _time.UtcNow);

          ResetChests(ChestType.Weekly);
          SpendAllWeeklyActivityPoints();
        }
      
        if (IsDayPassed(from: QuestsSaveData.DailyQuestsStartsTime))
        {
          Debug.Log("Updated daily quests!");
          QuestsSaveData.DailyQuestsStartsTime = GetLastDay(from: _time.UtcNow);
          
          InitializeBaseTask();
          _questCardCollectionPresenter.UpdateCards();

          ResetChests(ChestType.Daily);
          SpendAllDailyActivityPoints();
        }
        
        await UniTask.Delay(Delay);
      }
    }
    private void ResetChests(ChestType chestType)
    {
      _chestsConfig.Data.Values.Where(x => x.Type == chestType).Select(x => x.Id).ForEach(x => {
        if (QuestsSaveData.Chests.TryGetValue(x, out var chest))
          chest.Completed = false;
      });
    }
    
    private void SpendAllWeeklyActivityPoints() => _resourceService.TrySpend(ResourceType.WeeklyActivityPoint,
      _resourceService.GetResourceAmount(ResourceType.WeeklyActivityPoint), SaveReason.WeekPassed, null, null);

    private void SpendAllDailyActivityPoints() => _resourceService.TrySpend(ResourceType.DailyActivityPoint,
      _resourceService.GetResourceAmount(ResourceType.DailyActivityPoint), SaveReason.DayPassed, null, null);
    

    public bool IsMondayPassed(DateTime from)
    {
      DateTime lastMonday = GetLastMonday(_time.UtcNow);
      return lastMonday > from;
    }

    public bool IsDayPassed(DateTime from)
    {
      DateTime lastDay = GetLastDay(_time.UtcNow);
      return lastDay > from;
    }

    private DateTime GetLastMonday(DateTime from)
    {
      int daysSinceMonday = (int)from.DayOfWeek - (int)DayOfWeek.Monday;
        
      if (daysSinceMonday < 0) 
        daysSinceMonday += 7;

      DateTime lastMonday = from.AddDays(-daysSinceMonday);
      return new DateTime(lastMonday.Year, lastMonday.Month, lastMonday.Day, 0, 0, 0);
    }

    private DateTime GetLastDay(DateTime from) => from.Date;

    private void InitializeBaseTask()
    {
      QuestsSaveData.Quests = new();

      foreach (var questId in _questsConfig.Data.Keys)
        QuestsSaveData.Quests.Add(questId, _questFactory.CreateQuest(questId));

      _saveLoadService.SaveProgress(SaveReason.QuestStateUpdated, "Initialized quests");
    }

    private void InitializeSavedTasks()
    {
      foreach (var questsKey in new List<int>(QuestsSaveData.Quests.Keys)) 
        QuestsSaveData.Quests[questsKey] = _questFactory.CreateQuest(questsKey, QuestsSaveData.Quests);
    }
    
    private void InitializedChests()
    {
      QuestsSaveData.Chests = new();
      
      foreach (int chestsKey in _chestsConfig.Data.Keys) 
        QuestsSaveData.Chests.Add(chestsKey, new ChestSaveData());
    }
  }
}
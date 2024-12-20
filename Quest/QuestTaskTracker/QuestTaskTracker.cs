using System;

namespace _Project.Scripts.Meta.Services.Quest.QuestTaskTracker
{
  public class QuestTaskTracker
  {
    public event Action OnBattleStarted; 
    public event Action OnGameEntered; 
    public event Action<int> OnChestOpened; 
    public event Action OnArchersUpgraded;
    public event Action OnCastleUpgraded;
    public event Action OnChampionUpgraded;
    public event Action<int> OnSummoned;


    public void SendStartBattleTask() => OnBattleStarted?.Invoke();
    public void SendEnterGameTask() => OnGameEntered?.Invoke();
    public void SendOpenChestTask(int amount = 1) => OnChestOpened?.Invoke(amount);
    public void SendArchersUpgradeTask() => OnArchersUpgraded?.Invoke();
    public void SendCastleUpgradeTask() => OnCastleUpgraded?.Invoke();
    public void SendChampionUpgradeTask() => OnChampionUpgraded?.Invoke();
    public void SendSummonTask(int summonAmount) => OnSummoned?.Invoke(summonAmount);
  }
}

using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Meta.Services.Quest
{
  public interface IQuestsService
  {
    void Initialize();
    void UpdateQuestsState();
    bool HasCompletedTask { get; }
    event Action OnTasksStateChanged;
  }
}
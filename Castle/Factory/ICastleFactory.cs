using _Project.Scripts.Infrastructure.View;

namespace _Project.Scripts.Gameplay.FixedUpdateFeatures.Castle.Factory
{
  public interface ICastleFactory
  {
    void CreateCastle(EntityBehaviour castleView);
  }
}
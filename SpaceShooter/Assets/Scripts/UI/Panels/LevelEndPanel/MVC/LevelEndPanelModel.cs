using Managers.GameManagers;
using Zenject;

public class LevelEndPanelModel : Model
{
    #region MEMBERS

    private IGameMainManager gameMainManager;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IGameMainManager gameMainManager)
    {
        this.gameMainManager = gameMainManager;
    }

    public void BackToMenu()
    {
        LevelManager.Instance.EndLevel();
        gameMainManager.OpenMenu();
    }

    public void Continue()
    {
        LevelManager.Instance.EndLevel();
        gameMainManager.OpenWaitingRoom();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}

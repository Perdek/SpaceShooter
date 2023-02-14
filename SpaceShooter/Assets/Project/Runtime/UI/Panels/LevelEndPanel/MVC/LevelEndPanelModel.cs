using Managers.GameManagers;
using Managers.LevelManagers;
using Zenject;

public class LevelEndPanelModel : Model
{
    #region MEMBERS

    private IGameMainManager gameMainManager;
    private LevelEventsCommunicator _levelEventsCommunicator;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IGameMainManager gameMainManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        this.gameMainManager = gameMainManager;
        _levelEventsCommunicator = levelEventsCommunicator;
    }

    public void BackToMenu()
    {
        _levelEventsCommunicator.NotifyOnRequestLevelEnd();
        gameMainManager.OpenMenu();
    }

    public void Continue()
    {
        _levelEventsCommunicator.NotifyOnRequestLevelEnd();
        gameMainManager.OpenWaitingRoom();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}

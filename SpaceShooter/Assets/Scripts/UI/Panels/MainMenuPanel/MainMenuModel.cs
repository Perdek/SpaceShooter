using Managers.GameManagers;
using Zenject;

public class MainMenuModel : Model
{
    #region FIELDS

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

	public void StartGame()
	{
		gameMainManager.StartGame();
	}

	#endregion

	#region ENUMS

	#endregion
}

using Managers.GameManagers;
using Zenject;

public class LevelEndPanelController : Controller
{
	#region MEMBERS

	private IUpdateManager updateManager;
	private IGameMainManager gameMainManager;

	#endregion

	#region PROPERTIES

	private LevelEndPanelModel Model {
		get;
		set;
	}

	private LevelEndPanelView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	[Inject]
	public void InjectDependencies(IUpdateManager updateManager, IGameMainManager gameMainManager)
	{
		this.updateManager = updateManager;
		this.gameMainManager = gameMainManager;
	}

	public void AttachEvents()
	{
		gameMainManager.OnGameOver += View.ShowEndLevelPanel;
	}

	public void DetachEvents()
	{
		gameMainManager.OnGameOver -= View.ShowEndLevelPanel;
	}

	public bool IsShowedCenterPanel()
	{
		return View.IsShowedLevelEndPanel();
	}

	public void ShowLevelEndPanel()
	{
		updateManager.PauseTime();
		View.ShowEndLevelPanel();
	}

	public void ShowGameOver()
    {
		updateManager.PauseTime();
		View.ShowGameOverPanel();
	}

	protected virtual void OnDestroy()
	{
		DetachEvents();
	}

	public override void Initialize()
	{
		base.Initialize();


		PrepareProperties();
		AttachEvents();

		View.AddListenerToContinueButton(Model.Continue);
		View.AddListenerToMenuButton(Model.BackToMenu);
	}

	private void PrepareProperties()
	{
		Model = GetModel<LevelEndPanelModel>();
		View = GetView<LevelEndPanelView>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}

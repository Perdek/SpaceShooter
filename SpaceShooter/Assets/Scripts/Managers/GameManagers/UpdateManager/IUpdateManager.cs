using System;

namespace Managers.GameManagers
{
	public interface IUpdateManager
	{
		#region EVENTS

		public event Action OnUpdateInputInformation;
		public event Action OnDataChange;
		public event Action OnUpdatePhysic;
		public event Action OnUpdateView;

		#endregion

		#region METHODS

		public void PauseTime();
		public void UnPauseTime();

		#endregion
	}
}
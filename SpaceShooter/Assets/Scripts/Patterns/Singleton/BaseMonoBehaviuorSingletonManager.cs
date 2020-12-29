using UnityEngine;

public class BaseMonoBehaviourSingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
	#region FIELDS

	private static T instance;

	#endregion

	#region PROPERTIES

	public static T Instance {
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();

				if (instance == null)
				{
					//GameObject newSingletonGameObject = new GameObject(typeof(T).ToString());
					//instance = newSingletonGameObject.AddComponent<T>();
				}
			}

			return instance;
		}

		private set => instance = value;
	}

	public static bool IsInstantiated {
		get {
			return instance != null;
		}
	}

	#endregion

	#region METHODS

	public void SingletonInitialization()
	{
		Instance = this as T;
	}

	public virtual void Initialize()
	{

	}

	#endregion

	#region ENUMS

	#endregion
}

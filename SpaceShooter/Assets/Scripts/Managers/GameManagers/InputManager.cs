using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseMonoBehaviourSingletonManager<InputManager>
{
	#region FIELDS

	[Header("Player Movement")]

	[SerializeField]
	private KeyCode keyCodeMoveUp = KeyCode.W;
	[SerializeField]
	private KeyCode keyCodeMoveDown = KeyCode.S;
	[SerializeField]
	private KeyCode keyCodeMoveLeft = KeyCode.A;
	[SerializeField]
	private KeyCode keyCodeMoveRight = KeyCode.D;

	[Header("Shooting")]
	[SerializeField]
	private KeyCode keyCodeShoot = KeyCode.Space;

	#endregion

	#region PROPERTIES

	public KeyCode KeyCodeMoveUp => keyCodeMoveUp;
	public KeyCode KeyCodeMoveLeft => keyCodeMoveLeft;
	public KeyCode KeyCodeMoveRight => keyCodeMoveRight;
	public KeyCode KeyCodeMoveDown => keyCodeMoveDown;
	public KeyCode KeyCodeShoot => keyCodeShoot;

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}

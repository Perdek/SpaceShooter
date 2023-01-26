using UnityEngine;

namespace Managers.GameManagers
{
    public interface IInputManager
    {
        #region PROPERTIES

        public KeyCode KeyCodeMoveUp { get; }
        public KeyCode KeyCodeMoveLeft { get; }
        public KeyCode KeyCodeMoveRight { get; }
        public KeyCode KeyCodeMoveDown { get; }
        public KeyCode KeyCodeShoot { get; }
        public KeyCode KeyCodeNextWeapon { get; }
        public KeyCode KeyCodePrevWeapon { get; }

        #endregion

        #region METHODS

        #endregion
    }
}

using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;

[System.Serializable]
public class AsteroidViewComponent
{
    #region MEMBERS

    private IPoolManager poolManager;
    private LevelEventsCommunicator _levelEventsCommunicator;

    #endregion

    #region PROPERTIES

    private Transform AsteroidTransform {
        get;
        set;
    } = null;

    #endregion

    #region METHODS

    public void InjectDependencies(IPoolManager poolManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        this.poolManager = poolManager;
        _levelEventsCommunicator = levelEventsCommunicator;
    }

    public void SetAsteroidTransform(Transform asteroidTransform)
    {
        AsteroidTransform = asteroidTransform;
    }

    public void Explosion()
    {
        if (_levelEventsCommunicator.NotifyOnIsLevelEnded() == false)
        {
            poolManager.GetPoolObject(SpawnableObjectsTagsEnum.ASTEROID_EXPLOSION_TAG, AsteroidTransform.position, AsteroidTransform.rotation);
        }
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}

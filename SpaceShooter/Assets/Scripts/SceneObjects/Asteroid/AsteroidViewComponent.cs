using Managers.GameManagers;
using UnityEngine;

[System.Serializable]
public class AsteroidViewComponent
{
    #region MEMBERS

    private IPoolManager poolManager;

    #endregion

    #region PROPERTIES

    private Transform AsteroidTransform {
        get;
        set;
    } = null;

    #endregion

    #region METHODS

    public void InjectDependencies(IPoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public void SetAsteroidTranform(Transform asteroidTransform)
    {
        AsteroidTransform = asteroidTransform;
    }

    public void Explosion()
    {
        if (LevelManager.Instance != null && LevelManager.Instance.IsEndedLevel() == false)
        {
            poolManager.GetPoolObject(SpawnableObjectsTagsEnum.ASTEROID_EXPLOSION_TAG, AsteroidTransform.position, AsteroidTransform.rotation);
        }
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}

using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerVisualisationController
{
    #region FIELDS

    [FormerlySerializedAs("forceShieldParticleSystem")] [SerializeField] private ParticleSystem _forceShieldParticleSystem = default;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void TurnOnForceShield()
    {
        _forceShieldParticleSystem.gameObject.SetActive(true);
    }

    public void TurnOffForceShield()
    {
        _forceShieldParticleSystem.gameObject.SetActive(false);
    }

    #endregion
}


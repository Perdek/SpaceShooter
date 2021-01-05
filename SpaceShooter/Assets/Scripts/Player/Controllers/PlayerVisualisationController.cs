using UnityEngine;

[System.Serializable]
public class PlayerVisualisationController
{
    #region FIELDS

    [SerializeField] private ParticleSystem forceShieldParticleSystem = default;

    #endregion

    #region PROPERTIES

    private ParticleSystem ForceShieldParticleSystem => forceShieldParticleSystem;

    #endregion

    #region METHODS

    public void TurnOnForceShield()
    {
        ForceShieldParticleSystem.gameObject.SetActive(true);
    }

    public void TurnOffForceShield()
    {
        ForceShieldParticleSystem.gameObject.SetActive(false);
    }

    #endregion
}


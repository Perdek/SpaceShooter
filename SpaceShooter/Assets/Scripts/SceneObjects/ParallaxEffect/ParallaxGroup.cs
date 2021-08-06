using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxGroup : MonoBehaviour
{
    #region FIELDS

    [SerializeField]
    private List<ParallaxElement> parallaxGroupElements = new List<ParallaxElement>();

    [SerializeField]
    private float speed;

    #endregion

    #region PROPERTIES

    public List<ParallaxElement> ParallaxGroupElements => parallaxGroupElements;

    private float CachedMaxYPositionForParallaxElements { get; set; }

    #endregion

    #region METHODS

    public void UpdateParallaxEffects()
    {
        for (int i = 0; i < parallaxGroupElements.Count; i++)
        {
            parallaxGroupElements[i].transform.position += Vector3.down * speed;

            if (parallaxGroupElements[i].transform.position.y < -parallaxGroupElements[i].SpriteHeight)
            {
                parallaxGroupElements[i].transform.position = new Vector3(0, CachedMaxYPositionForParallaxElements, 0);
            }
        }
    }

    protected virtual void Awake()
    {
        CachedMaxYPositionForParallaxElements = ParallaxGroupElements.GetLastElement().SpriteHeight;
    }

    #endregion

    #region ENUMS

    #endregion
}

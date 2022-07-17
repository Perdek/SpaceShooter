
using Zenject;

public class PoolObjectsParentFactory : IFactory<UnityEngine.Object, UnityEngine.Transform, PoolObjectsParent>
{
    [Inject]
    private DiContainer container;

    //constructor?

    public PoolObjectsParent Create(UnityEngine.Object prefab, UnityEngine.Transform parentTransform)
    {
        return container.InstantiatePrefabForComponent<PoolObjectsParent>(prefab, parentTransform);
    }
}

using Zenject;

public class BasePoolObjectsFactory : IFactory<UnityEngine.Object, UnityEngine.Transform, IBasePoolObject>
{
    [Inject]
    private DiContainer container;

    //constructor?

    public IBasePoolObject Create(UnityEngine.Object prefab, UnityEngine.Transform parentTransform)
    {
        return container.InstantiatePrefabForComponent<IBasePoolObject>(prefab, parentTransform);
    }
}

using Zenject;

public class BasePoolObjectsFactory : IFactory<UnityEngine.Object, IBasePoolObject>
{
    [Inject]
    readonly DiContainer container;

    //constructor?

    public IBasePoolObject Create(UnityEngine.Object prefab)
    {
        return container.InstantiatePrefabForComponent<IBasePoolObject>(prefab);
    }
}

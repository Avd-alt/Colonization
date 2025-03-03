using UnityEngine;
using Zenject;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void CreateBase(Bot bot, Flag flag)
    {
        Base newBase = _container.InstantiatePrefabForComponent<Base>(_basePrefab, flag.transform.position, Quaternion.identity, null);

        newBase.MarkAsChildBase();
        newBase.AddBotInBase(bot);
        bot.SetBasePosition(newBase.transform.position);

        Destroy(flag.gameObject);
    }
}
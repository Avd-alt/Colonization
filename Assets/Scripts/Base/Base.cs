using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RemoverResource))]
public class Base : MonoBehaviour, IResourceDeliveryAnnouncer
{
    private const int SpawnStartCount = 3;
    private const int BotCost = 3;
    private const int BaseCost = 5;
    private const int MinBotsForBuilding = 1;
    private const int MinFlagsRequired = 0;

    [SerializeField] private ResourceDepot _resourceDepot;
    [SerializeField] private SpawnerBots _spawnerBots;

    [Inject] private ResourceDistributor _resourceDistributor;
    [Inject] private FlagCreator _flagCreator;

    private List<Bot> _bots = new List<Bot>();
    private Flag _flag;
    private int _quantityFlags;
    private bool _isCreatingNewBase = false;
    private bool _isMainBase = true;

    private void Start()
    {
        _quantityFlags = 1;

        if (_isMainBase)
        {
            for (int i = 0; i < SpawnStartCount; i++)
            {
                AddBotInBase(_spawnerBots.SpawnBot());
            }
        }
    }

    private void OnEnable()
    {
        _flagCreator.OnFlagSet += OnFlagSet;
    }

    private void Update()
    {
        if (_isCreatingNewBase)
        {
            if (_resourceDepot.QuantityResources >= BaseCost)
            {
                AttemptToCreateNewBase();
            }
        }
        else
        {
            AttemptToBuyBot();
        }

        AssignResourcesToBots();
    }

    public void NotifyResourceDelivered(Resource resource)
    {
        _resourceDistributor.ReleaseResource(resource);
    }

    public void MarkAsChildBase()
    {
        _isMainBase = false;
    }

    public void AddBotInBase(Bot bot)
    {
        bot.SetResourceManager(this);
        bot.SetBasePosition(transform.position);
        _bots.Add(bot);
    }

    public bool HasAvailableFlags() => _quantityFlags > MinFlagsRequired;

    public bool HasEnoughBots() => _bots.Count > MinBotsForBuilding;

    private void AssignResourcesToBots()
    {
        foreach (var bot in _bots)
        {
            if (bot.IsAvailable)
            {
                Resource resource = _resourceDistributor.TryGetAvailableResource();

                if (resource != null)
                {
                    bot.GatherResource(resource.transform.position);
                }
            }
        }
    }

    private void AttemptToBuyBot()
    {
        if (_resourceDepot.QuantityResources >= BotCost)
        {
            _resourceDepot.TakeResources(BotCost);
            AddBotInBase(_spawnerBots.SpawnBot());
        }
    }

    private void OnFlagSet(Flag flag)
    {
        _isCreatingNewBase = true;
        _flag = flag;
    }

    private void AttemptToCreateNewBase()
    {
        Bot freeBot = FindAvailableBot();

        if (freeBot != null)
        {
            _resourceDepot.TakeResources(BaseCost);
            freeBot.BuildNewBase(_flag);
            _bots.Remove(freeBot);
            _isCreatingNewBase = false;
            _quantityFlags = 0;
            _flagCreator.OnFlagSet -= OnFlagSet;
        }
    }

    private Bot FindAvailableBot() => _bots.Find(bot => bot.IsAvailable);
}
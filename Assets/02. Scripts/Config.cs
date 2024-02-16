using System;
using UnityEngine;

/// <summary>
/// 프로젝트 설정
/// </summary>
public class Config : Singleton<Config>
{
    [Header("API 인증")]
    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private string staticMapBaseUrl;

    [SerializeField]
    [Tooltip("true: api 사용, false: 로컬 데이터 사용")]
    private bool network = false;

    private INavigationRepository navigationRepository;

    public NavigationService NavigationService { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        ConfigRepository();
        ConfigNavigationService();
    }

    private void ConfigNavigationService()
    {
        NavigationService = new NavigationService(navigationRepository);
    }

    private void ConfigRepository()
    {
        if (network)
            navigationRepository = new NetworkNavigationRepository(clientId, clientSecret, staticMapBaseUrl);
        else
            navigationRepository = new LocalNavigationRepository();
    }
}
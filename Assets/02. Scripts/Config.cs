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
    private NavigationService navigationService;

    private IGpsRepository gpsRepository;
    private GpsService gpsService;

    protected override void Awake()
    {
        base.Awake();
        Inject();
    }

    private void Inject()
    {
        ConfigNavigationService();
        ConfigGpsService();
    }

    private NavigationService ConfigNavigationService()
    {
        if (navigationService == null)
            navigationService = new NavigationService(ConfigNavigationRepository(), ConfigGpsService());
        return navigationService;
    }

    private GpsService ConfigGpsService()
    {
        if (gpsService == null)
            gpsService = new GpsService(ConfigGpsRepository());
        return gpsService;
    }

    private IGpsRepository ConfigGpsRepository()
    {
        if (gpsRepository == null)
            gpsRepository = null;
        // gpsRepository = new AndroidGpsRepository();
        return gpsRepository;
    }

    private INavigationRepository ConfigNavigationRepository()
    {
        if (navigationRepository == null)
            if (network)
                navigationRepository = new NetworkNavigationRepository(clientId, clientSecret, staticMapBaseUrl);
            else
                navigationRepository = new LocalNavigationRepository();
        return navigationRepository;
    }
}
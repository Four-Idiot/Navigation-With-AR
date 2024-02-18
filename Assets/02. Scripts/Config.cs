using System.Collections.Generic;
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

    private IPoiRepository poiRepository;
    private PoiService poiService;

    protected override void Awake()
    {
        base.Awake();
        Inject();
    }

    private void Inject()
    {
        NavigationService();
        GpsService();
        PoiService();
    }

    public PoiService PoiService()
    {
        if (poiService == null)
            poiService = new PoiService(PoiRepository());
        return poiService;
    }

    private IPoiRepository PoiRepository()
    {
        if (poiRepository == null)
            poiRepository = new LocalPoiRepository();
        return poiRepository;
    }

    public NavigationService NavigationService()
    {
        if (navigationService == null)
            navigationService = new NavigationService(NavigationRepository(), GpsService(), PoiService());
        return navigationService;
    }

    public GpsService GpsService()
    {
        if (gpsService == null)
            gpsService = new GpsService(GpsRepository());
        return gpsService;
    }

    private IGpsRepository GpsRepository()
    {
        if (gpsRepository == null)
        {
#if UNITY_EDITOR
            gpsRepository = new LocalGpsRepository();
#else
            gpsRepository = new AndroidGpsRepository();
#endif
        }
        return gpsRepository;
    }

    private INavigationRepository NavigationRepository()
    {
        if (navigationRepository == null)
        {
#if UNITY_EDITOR
            if (network)
                navigationRepository = new NetworkNavigationRepository(clientId, clientSecret, staticMapBaseUrl);
            else
                navigationRepository = new LocalNavigationRepository();
#else
                navigationRepository = new NetworkNavigationRepository(clientId, clientSecret, staticMapBaseUrl);
#endif
        }
        return navigationRepository;
    }
}
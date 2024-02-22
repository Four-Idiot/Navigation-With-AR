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

    private IMarkerRepository markerRepository;
    private MarkerService markerService;

    private CameraService cameraService;

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

    public MarkerService PoiService()
    {
        if (markerService == null)
            markerService = new MarkerService(PoiRepository());
        return markerService;
    }

    private IMarkerRepository PoiRepository()
    {
        if (markerRepository == null)
            markerRepository = new LocalMarkerRepository();
        return markerRepository;
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

    public CameraService CameraService()
    {
        if (cameraService == null)
            cameraService = new CameraService();
        return cameraService;
    }

    private IGpsRepository GpsRepository()
    {
        if (gpsRepository == null)
        {
#if UNITY_EDITOR
            gpsRepository = new LocalGpsRepository();
            Debug.Log("In Editor");
#elif UNITY_ANDROID && !UNITY_EDITOR
            gpsRepository = new AndroidGpsRepository(Input.location);
            Debug.Log("In Android");
#endif
        }
        return gpsRepository;
    }

    private INavigationRepository NavigationRepository()
    {
        if (navigationRepository == null)
        {
            if (network)
                navigationRepository = new NetworkNavigationRepository(clientId, clientSecret, staticMapBaseUrl);
            else
                navigationRepository = new LocalNavigationRepository();
        }
        return navigationRepository;
    }
}
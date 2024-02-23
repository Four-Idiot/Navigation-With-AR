using System.Threading.Tasks;

public class GpsService: Component
{
    private readonly IGpsRepository gpsRepository;

    public GpsService(IGpsRepository gpsRepository)
    {
        this.gpsRepository = gpsRepository;
    }

    public async Task<Coords> FindCurrentCoordinates()
    {
        return await gpsRepository.FindCoordsByCurrentLocationOrDefault();
    }
}
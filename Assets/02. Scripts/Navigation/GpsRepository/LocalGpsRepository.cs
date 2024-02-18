using System.Threading.Tasks;

public class LocalGpsRepository: IGpsRepository
{
    public Task<Coords> FindCoordsByCurrentLocation()
    {
        // 강의실 좌표
        var coords = new Coords(126.744577f, 37.713834f);
        return Task.FromResult(coords);
    }

}
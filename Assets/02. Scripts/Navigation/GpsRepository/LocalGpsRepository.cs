using System.Threading.Tasks;

public class LocalGpsRepository: IGpsRepository
{
    public async Task<Coords> FindCoordsByCurrentLocation()
    {
        // 강의실 좌표
        var coords = new Coords(126.744577, 37.713834);
        return await Task.FromResult(coords);
    }

}
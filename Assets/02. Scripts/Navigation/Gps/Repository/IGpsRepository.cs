using System.Threading.Tasks;

public interface IGpsRepository
{
    public Task<Coords> FindCoordsByCurrentLocationOrDefault();
}
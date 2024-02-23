using System.Threading.Tasks;

public interface IGpsRepository: Component
{
    public Task<Coords> FindCoordsByCurrentLocationOrDefault();
}
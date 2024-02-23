using System.Threading.Tasks;

public interface IMarkerRepository : Component
{
    public Task<MarkerResponseDto> FindPoiInfo();
}
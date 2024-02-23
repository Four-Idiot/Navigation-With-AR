using System.Threading.Tasks;

public interface INavigationRepository : Component
{
    public Task<MapResponseDto> FindMapByCurrentLocation(MapRequestDto mapRequestDto);
}
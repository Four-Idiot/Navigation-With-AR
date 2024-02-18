using System.Threading.Tasks;

public interface INavigationRepository
{
   public Task<MapResponseDto> FindMapByCurrentLocation(MapRequestDto mapRequestDto);
}
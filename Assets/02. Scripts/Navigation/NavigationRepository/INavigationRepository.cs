using System.Threading.Tasks;

public interface INavigationRepository
{
   public Task<ResponseMapDto> FindMapByCurrentLocation(RequestMapDto requestMapDto);
}
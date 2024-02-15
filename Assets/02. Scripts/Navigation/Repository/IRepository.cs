using System.Threading.Tasks;

public interface IRepository
{
   public Task<ResponseMapDto> FindMapByCurrentLocation(RequestMapDto requestMapDto);
}
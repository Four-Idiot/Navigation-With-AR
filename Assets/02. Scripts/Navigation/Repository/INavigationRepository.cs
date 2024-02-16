using System.Threading.Tasks;

public interface INavigationRepository
{
   public Task<ResponseMapDto> FindMapByCurrentLocation(RequestMapDto requestMapDto);

   // TODO 구현체 꼭 변경해줘야함 지금 테스트용 데이터 넣어놨음
   public Task<Coords> GetCurrentLocation();
}
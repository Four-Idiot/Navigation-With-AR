using System.Threading.Tasks;

public interface IPoiRepository
{
    public Task<PoiInfoResponseDto> FindPoiInfo();
}
using System.Threading.Tasks;

public interface IMarkerRepository
{
    public Task<MarkerResponseDto> FindPoiInfo();
}
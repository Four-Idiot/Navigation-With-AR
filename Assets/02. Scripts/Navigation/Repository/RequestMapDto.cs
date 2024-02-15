/// <summary>
/// for static map api
/// </summary>
public class RequestMapDto
{
    public readonly double latitude;
    public readonly double longitude;
    public readonly uint width;
    public readonly uint height;
    public readonly uint level;
    public readonly bool scale;
    public readonly string markers;
    
    public RequestMapDto(double latitude, double longitude, uint width, uint height, uint level, bool scale, string markers)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.width = width;
        this.height = height;
        this.level = level;
        this.scale = scale;
        this.markers = markers;
    }
}
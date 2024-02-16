/// <summary>
/// for static map api
/// </summary>
public record RequestMapDto(double Latitude, double Longitude, uint Width, uint Height, uint Level);
/*{
    public double Latitude { get;}
    public double Longitude {get; init;}
    public uint Width {get; init;}
    public uint Height {get; init;}
    public uint Level {get; init;}
    public RequestMapDto(double latitude, double longitude, uint width, uint height, uint level = 15)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.width = width;
        this.height = height;
        this.level = level;
    }
}*/
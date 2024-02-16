using System.Threading.Tasks;
using UnityEngine;

public class NavigationService
{
    private readonly INavigationRepository navigationRepository;

    public NavigationService(INavigationRepository navigationRepository)
    {
        this.navigationRepository = navigationRepository;
    }

    public async Task<Texture2D> MapByCurrentLocation()
    {
        var response = await navigationRepository
            .FindMapByCurrentLocation(
                new(
                    37.3591614, 127.1054221, 1024,
                    1024, 15
                    ));
        Texture2D tex = new(350, 450);
        tex.LoadImage(response.BinaryImage);
        return tex;
    }
}
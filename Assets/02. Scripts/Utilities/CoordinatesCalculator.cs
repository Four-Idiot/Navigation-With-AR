using System;
using UnityEngine;

public static class CoordinatesCalculator
{
    private const double EarthRadius = 6371.01; // 지구 반경 (km)
    
    public static double CalculateDistanceBetweenCoords(Coords start, Coords dest)
    {
        // 위도, 경도를 라디안으로 변환
        double radLat1 = Mathf.Deg2Rad * start.Latitude;
        double radLat2 = Mathf.Deg2Rad * dest.Latitude;
        double radLon1 = Mathf.Deg2Rad * start.Longitude;
        double radLon2 = Mathf.Deg2Rad * dest.Longitude;

        // 두 지점간 위도, 경도 차이 계산
        double deltaLat = radLat2 - radLat1;
        double deltaLon = radLon2 - radLon1;

        // Haversine 공식 적용
        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));

        // 두 지점간 거리 (km)
        double distance = EarthRadius * c;
        
        return distance * 1000;
    }
}
using UnityEngine;

public class ResponseMapDto
{
    public readonly byte[] binaryImage;
    
    public ResponseMapDto(byte[] binaryImage)
    {
        this.binaryImage = binaryImage;
    }
}
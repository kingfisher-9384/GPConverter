namespace GPConverter.Interfaces;

public interface IImageFileConversions
{
    Image ConvertToImage(byte[] imageBytes);
    byte[] ConvertToByteArray(Image img);
}
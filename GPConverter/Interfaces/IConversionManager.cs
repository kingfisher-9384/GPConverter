using GPConverter.Models;

namespace GPConverter.Interfaces;

public interface IConversionManager
{
    string? Convert(ConversionParameters parameters);
}

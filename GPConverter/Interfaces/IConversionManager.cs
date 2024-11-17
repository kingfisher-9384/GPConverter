using GPConverter.Models;

namespace GPConverter.Interfaces;

public interface IConversionManager
{
    bool Convert(ConversionParameters parameters);
}

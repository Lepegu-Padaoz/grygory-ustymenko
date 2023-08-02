using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medical.DAL.Helpers
{
    public class EnumToStringConverter<TEnum> : ValueConverter<TEnum, string>
    {
        public EnumToStringConverter(ConverterMappingHints mappingHints = null)
            : base(
                v => v.ToString(),
                v => (TEnum)Enum.Parse(typeof(TEnum), v),
                mappingHints)
        {
        }
    }
}

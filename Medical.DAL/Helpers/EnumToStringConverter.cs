using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medical.DAL.Helpers
{
    /// <summary>
    /// Needs to convert int of enum to string enum's name (to write data in database as a nvarchar)
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
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

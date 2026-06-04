using System.Collections.Generic;

namespace Fantasy_World_GIS.GIS.Validation
{
    public interface IGisValidator<T>
    {
        IEnumerable<ValidationResult> Validate(T item);
    }
}
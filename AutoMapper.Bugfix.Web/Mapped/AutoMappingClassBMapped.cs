using System.Collections.Generic;

namespace AutoMapper.Bugfix.Web.Mapped;

public class AutoMappingClassBMapped
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Used in RESOURCE-class to be returned to client.")]
    public List<AutoMappingSharedClassMapped>? Data { get; set; }

    public AutoMappingClassBMapped()
    {
    }
}

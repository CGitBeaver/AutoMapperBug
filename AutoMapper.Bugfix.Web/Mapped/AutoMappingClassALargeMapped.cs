using System.Collections.Generic;

namespace AutoMapper.Bugfix.Web.Mapped;

public class AutoMappingClassALargeMapped
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "")]
    public List<AutoMappingClassAMapped>? ClassAMappedList { get; set; }
}

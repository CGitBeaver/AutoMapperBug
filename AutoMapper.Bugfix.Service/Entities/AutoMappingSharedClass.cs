using System;
using System.Text;

namespace AutoMapper.Bugfix.Service.Entities;

public class AutoMappingSharedClass
{
    public string Name { get; set; } = string.Empty;

    public AutoMappingSharedClass()
    {
        // necessary for JSON deserializer
    }

    public AutoMappingSharedClass(string name)
    {
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if ((obj is null) || !GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            AutoMappingSharedClass p = (AutoMappingSharedClass)obj;
            if (Name != p.Name)
            {
                return false;
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        var metadataDescriptor = new StringBuilder();
        metadataDescriptor.Append(Name);
        return metadataDescriptor.ToString().GetHashCode(StringComparison.Ordinal);
    }
}

using AutoMapper;
using AutoMapper.Bugfix.Service.Dto;
using AutoMapper.Bugfix.Service.Entities;
using AutoMapper.Bugfix.Web.Mapped;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoMapper.Bugfix.Web.Controllers;
[Route("api/v1.0/automapping")]
[ApiController]
public class AutoMappingController : ControllerBase
{
    private readonly IMapper _mapper;

    public AutoMappingController(
        IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("/A")]
    public ActionResult<AutoMappingClassALargeMapped> ReadRecordsAutoMapping()
    {
        AutoMappingClassALarge data = new();
        AutoMappingClassA classA = new();
        classA.Data.Add(new AutoMappingSharedClass());
        data.ClassAList.AddRange([
                classA
            ]);

        return _mapper.Map<AutoMappingClassALargeMapped>(data);
    }

    [HttpGet("/B")]
    public ActionResult<AutoMappingClassBLargeMapped> ReadRecordVersionHistoryAutoMapping()
    {
        AutoMappingClassB classB = new();
        classB.Data.Add(new AutoMappingSharedClass());
        List<AutoMappingClassB> dataList = [classB];
        AutoMappingClassBLarge data = new();
        data.ClassBList.AddRange(dataList);

        return _mapper.Map<AutoMappingClassBLargeMapped>(data);
    }
}

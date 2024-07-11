# AutoMapperBug
This is a solution that showcases a bug with AutoMapper and Dependency Injection
When running through the Starter-project there should be two functions that can be called through Swagger, A and B.

To reproduce the bug execute the following steps:
- Perform an IISReset
- Call function A
- Call function B

The second call, B, should result in a code 500 server error.
Without another IISReset call B will continue to fail.
Calling B first however means that both calls can be called repeatedly without any issues.
For example a call order of B -> A -> B works completely fine.

The copied error from within Visual Studio looks like this:


"AutoMapper.AutoMapperMappingException
  HResult=0x80131500
  Message=Error mapping types.
  Source=Cannot evaluate the exception source
  StackTrace:
Cannot evaluate the exception stack trace

  This exception was originally thrown at this call stack:

Inner Exception 1:
AutoMapperMappingException: Error mapping types.

Inner Exception 2:
AutoMapperMappingException: Error mapping types.

Inner Exception 3:
NullReferenceException: Object reference not set to an instance of an object."


Replacing the injected _mapper within the AutoMappingController with a locally created one via

var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMappingWeb>());
var mapper = config.CreateMapper();

fixes the error.

Both mapped classes contain the same AutoMappingSharedCollection. Replacing the collection on one of the classes with a simple List of the same AutoMappingSharedClass also fixes the problem.
This leads us to believe that the error occurs due to some sort of caching of the mapping of the AutoMappingSharedCollection within Dependency Injection but as to why exactly it happens is beyond us.

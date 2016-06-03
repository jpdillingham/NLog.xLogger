# xLogger
An extension of NLog.Logger that provides additional functionality for tracing the entry and exit, arbitrary checkpoints, exceptions and stack traces within methods.

*xLogger depends on [NLog](https://www.nuget.org/packages/NLog/), [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json) and my own [BigFont Class](https://github.com/jpdillingham/BigFont).*

# Features
## EnterMethod()

The ```EnterMethod()``` method is used to log the entry point of a method.  The method accepts an object array containing the method parameters and boolean used to determine whether to persist the timestamp of entry.  Returns a ```Guid``` if persistence is used, ```default(Guid)``` otherwise.

```c#
public void MyMethod(int one, int two)
{
  Guid persistedGuid = logger.EnterMethod(xLogger.Params(one, two), true);
       
  // method body
        
  logger.ExitMethod(persistedGuid);
}
```

## Customization

Customize the output of the logger by editing the strings in the ```Variables``` region to your liking.

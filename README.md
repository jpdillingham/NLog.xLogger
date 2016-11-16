# xLogger

[![Build status](https://ci.appveyor.com/api/projects/status/nxki86lvv5gxmgdl/branch/master?svg=true)](https://ci.appveyor.com/project/jpdillingham/xlogger/branch/master) 
[![codecov](https://codecov.io/gh/jpdillingham/xLogger/branch/master/graph/badge.svg)](https://codecov.io/gh/jpdillingham/xLogger)
[![Dependency Status](https://www.versioneye.com/user/projects/581c04a04304530b557dc736/badge.svg?style=flat-round)](https://www.versioneye.com/user/projects/581c04a04304530b557dc736)
[![NuGet version](https://badge.fury.io/nu/NLog.xLogger.svg)](https://badge.fury.io/nu/NLog.xLogger)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/jpdillingham/xLogger/blob/master/LICENSE)

An extension of NLog.Logger that provides additional functionality for tracing the entry and exit, arbitrary checkpoints, exceptions and stack traces within methods.

This library depends on [NLog](https://www.nuget.org/packages/NLog/), [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json) and my own [BigFont Class](https://github.com/jpdillingham/BigFont).

## Installation

Install from the NuGet gallery GUI or with the Package Manager Console using the following command:

```Install-Package NLog.xLogger```

## NLog.xLogger

The ```xLogger``` class is the only type within the namespace.  This type extends ```NLog.Logger```.

### Instantiating

xLogger instances are instantiated using the ```LogManager.GetLogger()``` and ```LogManager.GetCurrentClassLogger()``` factory methods provided by NLog.  

The ```GetCurrentClassLogger()``` method returns an instance of xLogger named using the current class name.

```c#
private xLogger logger = (xLogger)LogManager.GetCurrentClassLogger(typeof(xLogger));
```

Note that ```typeof(xLogger)``` must be passed to the factory method so that the correct type can be instantiated.  The result of the method must also be cast to xLogger.

The ```GetLogger()``` method returns a named instance of xLogger using the supplied name.

```c#
private xLogger logger = (xLogger)LogManager.GetLogger("generic logger name", typeof(xLogger));
```

#### xLogManager

The ```xLogManager``` wrapper for ```LogManager``` may also be used to create instances of xLogger using the ```GetCurrentClassxLogger()``` and ```GetxLogger()``` methods.

```c#
private xLogger logger = xLogManager.GetxLogger("generic name");
private xLogger logger = xLogManager.GetCurrentClassxLogger();
```

### Methods
#### EnterMethod()

The ```EnterMethod()``` method is used to log the entry point of a method.  The method accepts an object array containing the method parameters and boolean used to determine whether to persist the timestamp of entry.  Returns a ```Guid``` if persistence is used, ```default(Guid)``` otherwise.

The static method ```xLogger.Params()``` is included to assist with the building of the object array.  Pass each method parameter, in order, to this method and an object array containing the passed arguments is returned.  If you wish to exclude any arguments from logging, replace the passed argument with ```new xLogger.ExcludedParam()```.  This maintains proper positioning, which is important so that reflection can retrieve the name and type of the parameter.

Note that the class ExampleObject has been created to demonstrate the serialization functionality and will continue to be used for the remainder of the examples.

##### Example

```c#
public static void EnterMethodExample(int one, int two, ExampleObject three)
{
    logger.EnterMethod(xLogger.Params(one, two, three));

    // method body
    logger.Trace("Standard log message");
}
```

##### Output

```
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ──► Entering method: Void EnterMethodExample<Tone, Ttwo>(Int32 one, Int32 two, ExampleObject three) (Program.cs:line 56)
[x]: │   ├┄┈ Tone: System.String
[x]: │   ├┄┈ Ttwo: System.Boolean
[x]: │   ├┄┈ one: 1
[x]: │   ├┄┈ two: 2
[x]: │   ├┄┈ three: {
[x]: │   ├┄┈ three:   "Num": 3,
[x]: │   ├┄┈ three:   "Str": "three",
[x]: │   ├┄┈ three:   "List": [
[x]: │   ├┄┈ three:     1.1,
[x]: │   ├┄┈ three:     2.2,
[x]: │   ├┄┈ three:     3.3
[x]: │   ├┄┈ three:   ]
[x]: │   └┄┈ three: }
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: Standard log message 
```

#### ExitMethod()

The ```ExitMethod()``` method logs the exit point of a method.  The method accepts an object representing the method return value and an optional ```Guid```, used if the corresponding ```EnterMethod()``` was called with the persistence option.

##### Example

```c#
public static ExampleObject ExitMethodPersistentExample(int one, int two)
{
    Guid persistedGuid = logger.EnterMethod(xLogger.Params(one, two), true);

    // method body

    logger.Trace("Standard log message");
    ExampleObject returnValue = new ExampleObject(1, "return", new double[] { 5.5 }.ToList());

    logger.ExitMethod(returnValue, persistedGuid);
    return returnValue;
}
```

##### Output

```
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ──► Entering method: ExampleObject ExitMethodPersistentExample(Int32 one, Int32 two) (Program.cs:line 71), persisting with Guid: ee9f56c7-f910-447b-8d03-e14bd8598654
[x]: │   ├┄┈ one: 1
[x]: │   └┄┈ two: 2
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: Standard log message
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ◄── Exiting method: ExampleObject ExitMethodPersistentExample(Int32 one, Int32 two) (Program.cs:line 78), Guid: ee9f56c7-f910-447b-8d03-e14bd8598654
[x]: │   ├┄┈ return: {
[x]: │   ├┄┈ return:   "Num": 1,
[x]: │   ├┄┈ return:   "Str": "return",
[x]: │   ├┄┈ return:   "List": [
[x]: │   ├┄┈ return:     5.5
[x]: │   ├┄┈ return:   ]
[x]: │   └┄┈ return: }
[x]: ├────────────────────────────────────┄┈
[x]: │ ◊ Method execution duration: 2.0017ms
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈

```

#### Checkpoint()

The ```Checkpoint()``` method logs an arbitrary checkpoint anywhere within the method body.  The method accepts a string representing the name of the checkpoint, an object array containing an arbitrary list of variables, a string array containing the names of the variables contained in the object list, and an optional ```Guid```, used if the corresponding ```EnterMethod()``` call used the persistence option.

##### Example

```c#
public static int CheckpointExample(int one)
{
    Guid persistedGuid = logger.EnterMethod(xLogger.Params(one), true);

    logger.Trace("Standard log message");
    int two = 2;
    int three = 3;

    logger.Checkpoint("example checkpoint", xLogger.Vars(one, two, three), xLogger.Names("one", "two", "three"), persistedGuid);

    logger.Trace("Another standard log message");

    int returnValue = one + two + three;

    logger.ExitMethod(returnValue, persistedGuid);
    return returnValue;
}
```

##### Output

```
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ──► Entering method: Int32 CheckpointExample(Int32 one) (Program.cs:line 89), persisting with Guid: 9792bbef-4609-429d-a0d3-478ba65e9b0e
[x]: │   └┄┈ one: 1
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: Standard log message
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ √ Checkpoint 'example checkpoint' reached in method: Int32 CheckpointExample(Int32 one) (Program.cs:line 95), Guid: 9792bbef-4609-429d-a0d3-478ba65e9b0e
[x]: │   ├┄┈ one: 1
[x]: │   ├┄┈ two: 2
[x]: │   └┄┈ three: 3
[x]: ├────────────────────────────────────┄┈
[x]: │ ◊ Current execution duration: 1.0009ms
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: Another standard log message
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ◄── Exiting method: Int32 CheckpointExample(Int32 one) (Program.cs:line 101), Guid: 9792bbef-4609-429d-a0d3-478ba65e9b0e
[x]: │   └┄┈ return: 6
[x]: ├────────────────────────────────────┄┈
[x]: │ ◊ Method execution duration: 1.0009ms
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
```

#### Exception()

The ```Exception()``` method logs exception details.  The method differs from the others in that it can log to levels other than Trace.  The first parameter is of type ```LogLevel``` which is an enumeration of the logging levels within NLog.  
Other parameters are the ```Exception``` that was caught and, as always, the optional ```Guid``` created if the method was entered with the persistence option.

##### Example

```c#
public static void ExceptionExample()
{
	logger.EnterMethod();

	try
	{
		// intentionally raise an exception
		var arr = new string[5];
		Console.WriteLine(arr[5]);
	}
	catch (Exception ex)
	{
		logger.Exception(LogLevel.Error, ex);
	}
	finally
	{
		logger.ExitMethod();
	}
}
```

##### Output

```
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ──► Entering method: Void ExceptionExample() (Program.cs:line 110)
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌──┐┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │██││ ╳ Exception 'IndexOutOfRangeException' caught in method: Void ExceptionExample() (Program.cs:line 120)
[x]: │██││   └┄┈ "Index was outside the bounds of the array."
[x]: │██│├────────────────────────────────────┄┈
[x]: │██││   └┄► Void Main(String[] args)
[x]: │██││      └┄► Void ExceptionExample()
[x]: │██│├────────────────────────────────────┄┈
[x]: │██││   ├┄┈ ex: {
[x]: │██││   ├┄┈ ex:   "ClassName": "System.IndexOutOfRangeException",
[x]: │██││   ├┄┈ ex:   "Message": "Index was outside the bounds of the array.",
[x]: │██││   ├┄┈ ex:   "Data": null,
[x]: │██││   ├┄┈ ex:   "InnerException": null,
[x]: │██││   ├┄┈ ex:   "HelpURL": null,
[x]: │██││   ├┄┈ ex:   "StackTraceString": "
[x]: │██││   ├┄┈ ex:       at NLog.xLogger.Examples.Program.ExceptionExample() in C:\Users\JP.WHATNET\Google Drive\Projects\xLogger\Examples\Program.cs:line 116
[x]: │██││   ├┄┈ ex:   "
[x]: │██││   ├┄┈ ex:   "RemoteStackTraceString": null,
[x]: │██││   ├┄┈ ex:   "RemoteStackIndex": 0,
[x]: │██││   ├┄┈ ex:   "ExceptionMethod": "8\nExceptionExample\nExamples, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\nNLog.xLogger.Examples.Program\nVoid ExceptionExample()",
[x]: │██││   ├┄┈ ex:   "HResult": -2146233080,
[x]: │██││   ├┄┈ ex:   "Source": "Examples",
[x]: │██││   ├┄┈ ex:   "WatsonBuckets": null
[x]: │██││   └┄┈ ex: }
[x]: └──┘└──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ◄── Exiting method: Void ExceptionExample() (Program.cs:line 124)
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
```

#### Stack Trace
The ```StackTrace``` method logs the current call stack at the point of invocation.

##### Example

```c#
public static void StackTraceExample()
{
    logger.StackTrace(LogLevel.Info);
}
```

##### Output

```
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ @ Stack Trace from method: Void StackTraceExample() (Program.cs:line 133)
[x]: │   └┄► Void Main(String[] args)
[x]: │      └┄► Void StackTraceExample()
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
```

#### Additional Methods

* ```Multiline(LogLevel, string|string[])```: Logs the provided string or string array in multiple lines, split around the array elements or newline characters.
* ```MultilineWrapped(LogLevel, string|string[])```: Same as above, but wrapped in the stylized lines seen in the examples above.
* ```Separator(LogLevel)```: Logs a horizontal line
* ```Heading(LogLevel, string)```: Logs the provided string in large letters (provided by BigFont), followed by a separator.
* ```SubHeading(LogLevel, string)```: Logs the provided string in medium letters.
* ```SubSubHeading(LogLevel, string)```: Logs the provided string in smaller letters.

##### Example

```c#
public static void OtherExamples()
{
    logger.Multiline(LogLevel.Trace, "hello \n world!");
    logger.MultilineWrapped(LogLevel.Trace, new string[] { "hello", "again", "world!!" });
    logger.Separator(LogLevel.Trace);
    logger.Heading(LogLevel.Trace, "Hello world!");
    logger.SubHeading(LogLevel.Trace, "Hello world!");
    logger.SubSubHeading(LogLevel.Trace, "Hello world!");
}
```

##### Output

```
[x]: hello
[x]:  world!
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ hello
[x]: │ again
[x]: │ world!!
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ███    ███ ████████ ███       ███        ▄██████▄         ███         ███  ▄██████▄  ████████▄   ███       ████████▄  ▄███▄
[x]: │ ███    ███ ███      ███       ███       ███    ███        ███         ███ ███    ███ ███    ███  ███       ███   ▀███ █████
[x]: │ ███    ███ ███      ███       ███       ███    ███        ███         ███ ███    ███ ███    ███  ███       ███    ███ █████
[x]: │ ███▄▄▄▄███ ███▄▄▄   ███       ███       ███    ███        ███         ███ ███    ███ ███    ███  ███       ███    ███ █████
[x]: │ ███▀▀▀▀███ ███▀▀▀   ███       ███       ███    ███        ███   ▄█▄   ███ ███    ███ ████████▀   ███       ███    ███ ▀███▀
[x]: │ ███    ███ ███      ███       ███       ███    ███        ███  ▄█▀█▄  ███ ███    ███ ███▀██▄     ███       ███    ███  ███
[x]: │ ███    ███ ███      ███       ███       ███    ███        ███ ▄█▀ ▀█▄ ███ ███    ███ ███  ▀██▄   ███       ███   ▄███
[x]: │ ███    ███ ████████ █████████ █████████  ▀██████▀         █████▀   ▀█████  ▀██████▀  ███    ▀██▄ █████████ ████████▀   ███
[x]: │ ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ██   ██ ██████ ██       ██       ▄██████▄      ██      ██ ▄██████▄ ██████▄ ██       ██████▄  ▄███▄
[x]: │ ██   ██ ██     ██       ██       ██    ██      ██      ██ ██    ██ ██   ██ ██       ██   ▀██ █████
[x]: │ ██▄▄▄██ ██▄▄   ██       ██       ██    ██      ██      ██ ██    ██ ██   ██ ██       ██    ██ ▀███▀
[x]: │ ██▀▀▀██ ██▀▀   ██       ██       ██    ██      ██ ▄██▄ ██ ██    ██ ██████▀ ██       ██    ██  ███
[x]: │ ██   ██ ██     ██       ██       ██    ██      ██▄█▀▀█▄██ ██    ██ ██▀██▄  ██       ██   ▄██
[x]: │ ██   ██ ██████ ████████ ████████ ▀██████▀      ███▀  ▀███ ▀██████▀ ██  ▀██ ████████ ██████▀   ███
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: ┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈
[x]: │ ██  ██ █████ ██     ██     ▄████▄    ██   ██ ▄████▄ ████▄ ██     █████▄ ▄██▄
[x]: │ ██▄▄██ ██▄▄  ██     ██     ██  ██    ██   ██ ██  ██ ██ ██ ██     ██  ██ ▀██▀
[x]: │ ██▀▀██ ██▀▀  ██     ██     ██  ██    ██ ▄ ██ ██  ██ ████▀ ██     ██  ██  ██
[x]: │ ██  ██ █████ ██████ ██████ ▀████▀    ███▀███ ▀████▀ ██▀█▄ ██████ █████▀  ▄▄
[x]: └──────────────────────────────────────────────────────────────────────────────────────────┄┈
```

# Notes

## Performance

Performance is severely impacted with the Trace logging level enabled.  To alleviate this, configure NLog to asynchronously by adding the following to the configuration:

```<targets async="true">```

## Customization

The formatting of the output can be customized by creating and modifying the following variables within the NLog configuration.

```xml
  <variable name="xLogger.Prefix" value="│ "/>
  <variable name="xLogger.Header" value="┌────────────────────────────────────────────────────────────────────────────────────────────────────┄┈ "/>
  <variable name="xLogger.EnterPrefix" value="${xLogger.Prefix}──► "/>
  <variable name="xLogger.ExitPrefix" value="${xLogger.Prefix}◄── "/>
  <variable name="xLogger.CheckpointPrefix" value="${xLogger.Prefix}√ "/>
  <variable name="xLogger.ExceptionPrefix" value="${xLogger.Prefix}╳ "/>
  <variable name="xLogger.StackTracePrefix" value="${xLogger.Prefix}@ "/>
  <variable name="xLogger.ExecutionDurationPrefix" value="${xLogger.Prefix}◊ "/>
  <variable name="xLogger.LinePrefix" value="${xLogger.Prefix}  ├┄┈ "/>
  <variable name="xLogger.FinalLinePrefix" value="${xLogger.Prefix}  └┄┈ "/>
  <variable name="xLogger.LinePrefixVariable" value="${xLogger.Prefix}  $└┄► "/>
  <variable name="xLogger.Footer" value="└──────────────────────────────────────────────────────────────────────────────────────────┄┈ "/>
  <variable name="xLogger.InnerSeparator" value="├────────────────────────────────────┄┈ "/>
  <variable name="xLogger.OuterSeparator" value="■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■"/>
  <variable name="xLogger.ExceptionHeaderPrefix" value="┌──┐"/>
  <variable name="xLogger.ExceptionLinePrefix" value="│██│"/>
  <variable name="xLogger.ExceptionFooterPrefix" value="└──┘"/>
  <variable name="xLogger.HeadingFont" value="Block"/>
  <variable name="xLogger.SubHeadingFont" value="Block"/>
  <variable name="xLogger.SubSubHeadingFont" value="Block"/>
  <variable name="xLogger.Indent" value="3"/>
  <variable name="xLogger.AutoPruneEnabled" value="true"/>
  <variable name="xLogger.AutoPruneAge" value="300"/>
```

If any of the variables above are missing from the configuration, the default values are used.  The default values are hard coded within the library but are shown above for demonstration.
# xLogger
An extension of NLog.Logger that provides additional functionality for tracing the entry and exit, arbitrary checkpoints, exceptions and stack traces within methods.

xLogger depends on [NLog](https://www.nuget.org/packages/NLog/), [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json) and my own [BigFont Class](https://github.com/jpdillingham/BigFont).

# Features
## EnterMethod()

The ```EnterMethod()``` method is used to log the entry point of a method.  The method accepts an object array containing the method parameters and boolean used to determine whether to persist the timestamp of entry.  Returns a ```Guid``` if persistence is used, ```default(Guid)``` otherwise.

The static method ```xLogger.Params()``` is included to assist with the building of the object array.  Pass each method parameter, in order, to this method and an object array containing the passed arguments is returned.  If you wish to exclude any arguments from logging, replace the passed argument with ```new xLogger.ExcludedParam()```.  This maintains proper positioning, which is important so that reflection can retrieve the name and type of the parameter.

Note that the class ExampleObject has been created to demonstrate the serialization functionality and will continue to be used for the remainder of the examples.

### Example

```c#
public static void EnterMethodExample(int one, int two, ExampleObject three)
{
    logger.EnterMethod(xLogger.Params(one, two, three));

    // method body
    logger.Trace("Standard log message");
}
```

### Output

```
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ──► Entering method: Void EnterMethodExample(Int32 one, Int32 two, ExampleObject three) (Program.cs:line 18) 
[x]: │   ├┄┈ one: 1 
[x]: │   ├┄┈ two: 2 
[x]: │   ├┄┈ three: { 
[x]: │   ├┄┈ three:   "num": 3, 
[x]: │   ├┄┈ three:   "str": "three", 
[x]: │   ├┄┈ three:   "list": [ 
[x]: │   ├┄┈ three:     1.1, 
[x]: │   ├┄┈ three:     2.2, 
[x]: │   ├┄┈ three:     3.3 
[x]: │   ├┄┈ three:   ] 
[x]: │   └┄┈ three: } 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: Standard log message 
```

## ExitMethod()

The ```ExitMethod()``` method logs the exit point of a method.  The method accepts an object representing the method return value and an optional ```Guid```, used if the corresponding ```EnterMethod()``` was called with the persistence option.

### Example

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

### Output

```
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ──► Entering method: ExampleObject ExitMethodPersistentExample(Int32 one, Int32 two) (Program.cs:line 28), persisting with Guid: 6cd26c76-69a6-4425-a316-bf03368108b0 
[x]: │   ├┄┈ one: 1 
[x]: │   └┄┈ two: 2 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: Standard log message 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ◄── Exiting method: ExampleObject ExitMethodPersistentExample(Int32 one, Int32 two) (Program.cs:line 35), Guid: 6cd26c76-69a6-4425-a316-bf03368108b0 
[x]: │   ├┄┈ return: { 
[x]: │   ├┄┈ return:   "num": 1, 
[x]: │   ├┄┈ return:   "str": "return", 
[x]: │   ├┄┈ return:   "list": [ 
[x]: │   ├┄┈ return:     5.5 
[x]: │   ├┄┈ return:   ] 
[x]: │   └┄┈ return: } 
[x]: ├──────────────────────── ─       ──  ─ 
[x]: │ ◊ Method execution duration: 4.0107ms 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 

```

## Checkpoint()

The ```Checkpoint()``` method logs an arbitrary checkpoint anywhere within the method body.  The method accepts a string representing the name of the checkpoint, an object array containing an arbitrary list of variables, a string array containing the names of the variables contained in the object list, and an optional ```Guid```, used if the corresponding ```EnterMethod()``` call used the persistence option.

### Example

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

### Output

```
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ──► Entering method: Int32 CheckpointExample(Int32 one) (Program.cs:line 42), persisting with Guid: df75374b-9909-4680-84d5-1b0b5c52df09 
[x]: │   └┄┈ one: 1 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: Standard log message 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ √ Checkpoint 'example checkpoint' reached in method: Int32 CheckpointExample(Int32 one) (Program.cs:line 48), Guid: df75374b-9909-4680-84d5-1b0b5c52df09 
[x]: │   ├┄┈ one: 1 
[x]: │   ├┄┈ two: 2 
[x]: │   └┄┈ three: 3 
[x]: ├──────────────────────── ─       ──  ─ 
[x]: │ ◊ Current execution duration: 0.5015ms 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: Another standard log message 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ◄── Exiting method: Int32 CheckpointExample(Int32 one) (Program.cs:line 54), Guid: df75374b-9909-4680-84d5-1b0b5c52df09 
[x]: │   └┄┈ return: 6 
[x]: ├──────────────────────── ─       ──  ─ 
[x]: │ ◊ Method execution duration: 1.0032ms 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
```

## Exception()

The ```Exception()``` method logs exception details.  The method differs from the others in that it can log to levels other than Trace.  The first parameter is of type ```Action<string>``` which corresponds to the type of the logging methods within NLog.  Other parameters are the ```Exception``` that was caught and, as always, the optional ```Guid``` created if the method was entered with the persistence option.

### Example

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
        logger.Exception(logger.Error, ex);
    }
    finally
    {
        logger.ExitMethod();
    }
}
```

### Output

```
[x]: ┌──┐┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │██││ ╳ Exception 'IndexOutOfRangeException' caught in method: Void ExceptionExample() (Program.cs:line 73): 
[x]: │██││   └┄┈ "Index was outside the bounds of the array." 
[x]: │██│├──────────────────────── ─       ──  ─ 
[x]: │██││   └┄► Void Main(String[] args) 
[x]: │██││      └┄► Void ExceptionExample() 
[x]: │██│├──────────────────────── ─       ──  ─ 
[x]: │██││   ├┄┈ ex: { 
[x]: │██││   ├┄┈ ex:   "ClassName": "System.IndexOutOfRangeException", 
[x]: │██││   ├┄┈ ex:   "Message": "Index was outside the bounds of the array.", 
[x]: │██││   ├┄┈ ex:   "Data": null, 
[x]: │██││   ├┄┈ ex:   "InnerException": null, 
[x]: │██││   ├┄┈ ex:   "HelpURL": null, 
[x]: │██││   ├┄┈ ex:   "StackTraceString": " 
[x]: │██││   ├┄┈ ex:       at xLogger_Example.Program.ExceptionExample() in C:\Users\JP.WHATNET\OneDrive\Projects\xLogger\xLogger\xLogger-Example\xLogger-Example\Program.cs:line 69 
[x]: │██││   ├┄┈ ex:   " 
[x]: │██││   ├┄┈ ex:   "RemoteStackTraceString": null, 
[x]: │██││   ├┄┈ ex:   "RemoteStackIndex": 0, 
[x]: │██││   ├┄┈ ex:   "ExceptionMethod": "8\nExceptionExample\nxLogger-Example, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\nxLogger_Example.Program\nVoid ExceptionExample()", 
[x]: │██││   ├┄┈ ex:   "HResult": -2146233080, 
[x]: │██││   ├┄┈ ex:   "Source": "xLogger-Example", 
[x]: │██││   ├┄┈ ex:   "WatsonBuckets": null 
[x]: │██││   └┄┈ ex: } 
[x]: └──┘└──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ◄── Exiting method: Void ExceptionExample() (Program.cs:line 73) 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
```

## Stack Trace
The ```StackTrace``` method logs the current call stack at the point of invocation.

### Example

```c#
public static void StackTraceExample()
{
    logger.StackTrace(logger.Info);
}
```

### Output

```
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ @ Stack Trace from method: Void StackTraceExample() (Program.cs:line 83) 
[x]: │   └┄► Void Main(String[] args) 
[x]: │      └┄► Void StackTraceExample() 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
```

## Additional Methods

* ```Multiline(action, string|string[])```: Logs the provided string or string array in multiple lines, split around the array elements or newline characters.
* ```MultilineWrapped(action, string|string[])```: Same as above, but wrapped in the stylized lines seen in the examples above.
* ```Separator(action)```: Logs a horizontal line
* ```Heading(action, string)```: Logs the provided string in large letters (provided by BigFont), followed by a separator.
* ```SubHeading(action, string)```: Logs the provided string in medium letters.
* ```SubSubHeading(action, string)```: Logs the provided string in smaller letters.

### Example

```c#
public static void OtherExamples()
{
    logger.Multiline(logger.Trace, "hello \n world!");
    logger.MultilineWrapped(logger.Trace, new string[] { "hello", "again", "world!!" });
    logger.Separator(logger.Trace);
    logger.Heading(logger.Trace, "Hello world!");
    logger.SubHeading(logger.Trace, "Hello world!");
    logger.SubSubHeading(logger.Trace, "Hello world!");
}
```

### Output

```
[x]: hello  
[x]:  world! 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ hello 
[x]: │ again 
[x]: │ world!! 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │ ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■■■■■■■■■■■■ ■■  ■■ ■■   ■■■■ ■■     ■■     ■ ■ 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │    ▄█    █▄       ▄████████  ▄█        ▄█        ▄██████▄          ▄█     █▄   ▄██████▄     ▄████████  ▄█       ████████▄   ▄███▄   
[x]: │   ███    ███     ███    ███ ███       ███       ███    ███        ███     ███ ███    ███   ███    ███ ███       ███   ▀███ ███████  
[x]: │   ███    ███     ███    █▀  ███       ███       ███    ███        ███     ███ ███    ███   ███    ███ ███       ███    ███ ███████  
[x]: │  ▄███▄▄▄▄███▄▄  ▄███▄▄▄     ███       ███       ███    ███        ███     ███ ███    ███  ▄███▄▄▄▄██▀ ███       ███    ███ ▀█████▀  
[x]: │ ▀▀███▀▀▀▀███▀  ▀▀███▀▀▀     ███       ███       ███    ███        ███     ███ ███    ███ ▀▀███▀▀▀▀▀   ███       ███    ███  ▀███▀   
[x]: │   ███    ███     ███    █▄  ███       ███       ███    ███        ███     ███ ███    ███ ▀███████████ ███       ███    ███   ███    
[x]: │   ███    ███     ███    ███ ███▌    ▄ ███▌    ▄ ███    ███        ███ ▄█▄ ███ ███    ███   ███    ███ ███▌    ▄ ███   ▄███          
[x]: │   ███    █▀      ██████████ █████▄▄██ █████▄▄██  ▀██████▀          ▀███▀███▀   ▀██████▀    ███    ███ █████▄▄██ ████████▀    ██▀    
[x]: │ ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■■■■■■■■■■■■ ■■  ■■ ■■   ■■■■ ■■     ■■     ■ ■ 
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │   ██   █       ▄█████  █        █        ██████        █     █   ██████     █████  █       ██████▄   ▄███▄   
[x]: │   ██   ██     ██   █  ██       ██       ██    ██      ██     ██ ██    ██   ██  ██ ██       ██   ▀██ ▀█████▀  
[x]: │  ▄██▄▄▄██▄▄  ▄██▄▄    ██       ██       ██    ██      ██     ██ ██    ██  ▄██▄▄█▀ ██       ██    ██  ▀███▀   
[x]: │ ▀▀██▀▀▀██▀  ▀▀██▀▀    ██       ██       ██    ██      ██     ██ ██    ██ ▀███████ ██       ██    ██   ███    
[x]: │   ██   ██     ██   █  ██▌    ▄ ██▌    ▄ ██    ██      ██ ▄█▄ ██ ██    ██   ██  ██ ██▌    ▄ ██   ▄██          
[x]: │   ██   ██     ███████ ████▄▄██ ████▄▄██  ██████        ███▀███   ██████    ██  ██ ████▄▄██ ██████▀    ██▀    
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
[x]: ┌─────────── ─ ───────────────────────── ─────────────────────────────────────────────────────────────────── ─────── ─    ─     ─ 
[x]: │  ██  █    ▄████   █      █      █████      █   █   █████    █████  █     █████▄  ▄██▄  
[x]: │  ██▄▄██▄  ██     ██     ██     ██   ██    ██   ██ ██   ██  ██  ██ ██     ██   ██ ▀██▀  
[x]: │ ▀██▀▀██  ▀██▀▀   ██   ▄ ██   ▄ ██   ██    ██▄█▄██ ██   ██ ▄██▄▄█▀ ██   ▄ ██   ██  ██   
[x]: │  ██  ██   ██████ ██▄▄██ ██▄▄██  █████      ██▀██   █████   ██  ██ ██▄▄██ █████▀   ▄▄   
[x]: └──────────────────── ───────────────────────────────  ─  ─          ─ ─ ─    ─   ─ 
```

# Notes

## Performance

Performance is severely impacted with the Trace logging level enabled.  To alleviate this, configure NLog to asynchronously by adding the following to the configuration:

```<targets async="true">```

## Customization

Customize the output of the logger by editing the strings in the ```Variables``` region to your liking.

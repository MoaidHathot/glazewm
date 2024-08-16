namespace GlazeWM.Infrastructure.Exceptions;

public class ExceptionHandlingOptions
{
  public required string ErrorLogPath { get; set; }
  public required Func<Exception, string> ErrorLogMessageDelegate { get; set; }
}

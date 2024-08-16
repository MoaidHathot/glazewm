namespace GlazeWM.Infrastructure.WindowsApi;

public class CurrentMediaChangedEventArgs
{
  public required string AlbumTitle { get; set; }
  public required string Artist { get; set; }
  public required string Title { get; set; }
}

using GlazeWM.Infrastructure.Bussing;
using SplitContainer = GlazeWM.Domain.Containers.SplitContainer;

namespace GlazeWM.Domain.Windows.Commands;

public class ManageWindowCommand : Command
{
  public IntPtr WindowHandle { get; }
  public SplitContainer TargetParent { get; }

  public ManageWindowCommand(IntPtr windowHandle, SplitContainer targetParent = null)
  {
    WindowHandle = windowHandle;
    TargetParent = targetParent;
  }
}

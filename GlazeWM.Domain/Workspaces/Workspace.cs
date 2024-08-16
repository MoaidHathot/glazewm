using GlazeWM.Domain.Common;
using GlazeWM.Domain.Common.Enums;
using GlazeWM.Domain.UserConfigs;
using GlazeWM.Infrastructure;
using GlazeWM.Infrastructure.Utils;
using GlazeWM.Infrastructure.WindowsApi;

using SplitContainer = GlazeWM.Domain.Containers.SplitContainer;
using Monitor = GlazeWM.Domain.Monitors.Monitor;

namespace GlazeWM.Domain.Workspaces;

public sealed class Workspace : SplitContainer
{
  /// <inheritdoc />
  public override ContainerType Type { get; } = ContainerType.Workspace;

  public string Name { get; set; }

  private readonly UserConfigService _userConfigService =
      ServiceLocator.GetRequiredService<UserConfigService>();

  private readonly WorkspaceService _workspaceService =
      ServiceLocator.GetRequiredService<WorkspaceService>();

  //todo - moaid - null
  public string DisplayName => _userConfigService.GetWorkspaceConfigByName(Name)!.DisplayName ?? Name;

  //todo - moaid - null
  public bool KeepAlive => _userConfigService.GetWorkspaceConfigByName(Name)!.KeepAlive;

  /// <summary>
  /// Get height of bar after it's been automatically adjusted by DPI scaling.
  /// </summary>
  private int _logicalBarHeight
  {
    get
    {
      var monitor = (Monitor)Parent;
      var barHeight = UnitsHelper.TrimUnits(_userConfigService.GetBarConfigForMonitor(monitor).Height);
      return Convert.ToInt32(barHeight * monitor.ScaleFactor);
    }
  }

  private int _yOffset
  {
    get
    {
      var monitor = (Monitor)Parent;
      var barPosition = _userConfigService.GetBarConfigForMonitor(monitor).Position;
      return barPosition == BarPosition.Top ? _logicalBarHeight : 0;
    }
  }

  private RectDelta _outerGaps => CommandParsingService.ShorthandToRectDelta(_userConfigService.GapsConfig.OuterGap);

  private BarConfig barForMonitor => _userConfigService.GetBarConfigForMonitor((Monitor)Parent);
  private int floatBarOffsetY => UnitsHelper.TrimUnits(barForMonitor.OffsetY);

  public override int Height
  {
    get
    {
      if (!_userConfigService.GetBarConfigForMonitor((Monitor)Parent).Enabled)
      {
        return Parent.Height - _outerGaps.Top - _outerGaps.Bottom;
      }

      return Parent.Height - _outerGaps.Top - _outerGaps.Bottom - (barForMonitor.Position == BarPosition.Top ? floatBarOffsetY : -floatBarOffsetY) - _logicalBarHeight;
    }
  }

  public override int Width => Parent.Width - _outerGaps.Left - _outerGaps.Right;
  public override int X => Parent.X + _outerGaps.Left;
  public override int Y
  {
    get
    {
      if (!_userConfigService.GetBarConfigForMonitor((Monitor)Parent).Enabled || barForMonitor.Position == BarPosition.Bottom)
      {
        return Parent.Y + _outerGaps.Top;
      }

      return Parent.Y + _outerGaps.Top + _yOffset + floatBarOffsetY;
    }
  }

  /// <summary>
  /// Whether the workspace itself or a descendant container has focus.
  /// </summary>
  public bool HasFocus => _workspaceService.GetFocusedWorkspace() == this;

  /// <summary>
  /// Whether the workspace is currently displayed by the parent monitor.
  /// </summary>
  public bool IsDisplayed => ((Monitor)Parent).DisplayedWorkspace == this;

  public Workspace(string name, TilingDirection tilingDirection)
  {
    Name = name;
    TilingDirection = tilingDirection;
  }
}

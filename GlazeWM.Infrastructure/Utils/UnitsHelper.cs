using System.Globalization;
using System.Text.RegularExpressions;

namespace GlazeWM.Infrastructure.Utils;

public static partial class UnitsHelper
{
  /// <summary>
  /// Returns the number part of amount with units as signed integer
  /// </summary>
  public static int TrimUnits(string amountWithUnits)
  {
    var unitsRegex = CreateRegex();
    var amount = unitsRegex.Replace(amountWithUnits, "").Trim();
    return Convert.ToInt32(amount, CultureInfo.InvariantCulture);
  }
  /// <summary>
  /// Returns the unit part of amount with units as a string
  /// </summary>
  public static string GetUnits(string amountWithUnits)
  {
    var unitsRegex = CreateRegex();
    var match = unitsRegex.Match(amountWithUnits);
    return match.Value;
  }

  [GeneratedRegex("(%|ppt|px)")]
  private static partial Regex CreateRegex();
}

using System.Drawing;

static class ColorHelpers
{
  public static Color ComplimentaryColor(Color color){
    return Color.FromArgb(color.A, InvertByte(color.R),InvertByte(color.G), InvertByte(color.B));
  }

  //this is probably useless
  public static Color HalfColor(Color color){
    return Color.FromArgb(color.A, SumWithOverFlow(127, color.R), SumWithOverFlow(127, color.G),  SumWithOverFlow(127, color.B));
  }

  private static byte InvertByte(byte num){
    return (byte) (byte.MaxValue ^ num);
  }
  private static byte SumWithOverFlow(byte num1, byte num2){
    return (byte)((num1 + num2) % 255);
  }
}
namespace AsyncApostle.Helpers;

public interface IAsyncReplacer
{
   void ReplaceToAsync(IMethod method);
}
namespace AsyncApostle.Helpers;

public interface IAsyncReplacer
{
   #region methods

   void ReplaceToAsync(IMethod method);

   #endregion
}
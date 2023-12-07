extern alias mscorlib;
using StreamingContext = mscorlib::System.Runtime.Serialization.StreamingContext;

namespace System.Diagnostics;

/// <inheritdoc/>
/// <summary>
///    Exception thrown when the program executes an instruction that was thought to be unreachable.
/// </summary>
[Serializable]
public class UnreachableException : Exception
{
   #region constructors

   /// <inheritdoc/>
   /// <summary>
   ///    Initializes a new instance of the <see cref="UnreachableException"/> class with the default
   ///    error
   ///    message.
   /// </summary>
   public UnreachableException() : base("Missing parameter does not have a default value.") { }

   /// <inheritdoc/>
   /// <summary>
   ///    Initializes a new instance of the <see cref="UnreachableException"/>
   ///    class with a specified error message.
   /// </summary>
   /// <param name="message">The error message that explains the reason for the exception.</param>
   public UnreachableException(string? message) : base(message) { }

   /// <inheritdoc/>
   /// <summary>
   ///    Initializes a new instance of the <see cref="UnreachableException"/>
   ///    class with a specified error message and a reference to the inner exception that is the cause of
   ///    this exception.
   /// </summary>
   /// <param name="message">The error message that explains the reason for the exception.</param>
   /// <param name="innerException">The exception that is the cause of the current exception.</param>
   public UnreachableException(string? message, Exception? innerException) : base(message, innerException) { }

   /// <inheritdoc/>
   protected UnreachableException(SerializationInfo info, StreamingContext context) : base(info, context) { }

   #endregion
}
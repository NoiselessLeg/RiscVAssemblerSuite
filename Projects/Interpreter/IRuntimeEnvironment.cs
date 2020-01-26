namespace Assembler.Interpreter
{
   /// <summary>
   /// Defines a class that acts as an environment harness for the interpreter.
   /// </summary>
   public interface IRuntimeEnvironment
   {

      /// <summary>
      /// Requests that the environment implementation pauses current execution.
      /// </summary>
      void Break();

      /// <summary>
      /// Requests that the environment implementation terminates the application runtime.
      /// </summary>
      void Terminate();
   }
}

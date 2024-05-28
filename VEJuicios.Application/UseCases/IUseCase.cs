using System;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases
{
    /// <summary>
    ///     Use Case.
    /// </summary>
    /// <typeparam name="TUseCaseInput">Any Input Message.</typeparam>
    public interface IUseCase<in TUseCaseInput>
    {
        /// <summary>
        ///     Executes the Use Case.
        /// </summary>
        /// <param name="input">Input Message.</param>
        /// <returns>Task.</returns>
        Task Execute(TUseCaseInput input);

    }
}

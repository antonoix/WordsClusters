using System;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action OnPointerClicked;
        event Action OnPointerUnclicked;
    }
}
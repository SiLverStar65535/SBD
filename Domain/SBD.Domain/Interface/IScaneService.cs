using System.Collections.Generic;
using System.Reflection.Emit;
using SBD.Domain.Models;

namespace SBD.Domain.Interface;

public interface IScaneService
{
    LuggageSize GetLuggageSize( );
    int GetLuggageWieght( );
}
using DeWaste.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DeWaste.Services
{
    class UniqueSuggestionIDComparer : IEqualityComparer<Suggestion>
    {
        public bool Equals(Suggestion x, Suggestion y)
        {
            return x.id == y.id;
        }

        public int GetHashCode(Suggestion obj)
        {
            return obj.id.GetHashCode();
        }
    }
}

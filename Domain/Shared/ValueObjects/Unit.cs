using Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record Unit
    {
        public static Unit Kilograms => new("kg");
        public static Unit Pieces => new("pc");
        public static Unit Meters => new("m");
        public static Unit SquareMeters => new("m2");

        public string Value { get; }
        public Unit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidUnitException();
            }

            Value = value;
        }

        public static implicit operator Unit(string unit) => new(unit);
        public static implicit operator string(Unit unit) => unit.Value;
    }
}

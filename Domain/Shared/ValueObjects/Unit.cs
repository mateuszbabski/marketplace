using Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record ProductUnit
    {
        public static ProductUnit Kilograms => new("kg");
        public static ProductUnit Pieces => new("pc");
        public static ProductUnit Meters => new("m");
        public static ProductUnit SquareMeters => new("m2");

        public string Value { get; }
        public ProductUnit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidProductUnitException();
            }

            Value = value;
        }

        public static implicit operator ProductUnit(string unit) => new(unit);
        public static implicit operator string(ProductUnit unit) => unit.Value;
    }
}

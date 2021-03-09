using System;
using System.Collections.Generic;
using System.Text;

namespace EPE.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string ValueToString(this decimal value) =>
            $"$ {value.ToString("N2")}";
    }
}

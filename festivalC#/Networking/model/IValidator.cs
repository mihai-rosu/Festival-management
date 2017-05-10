﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalc.model
{
    public interface IValidator<T>
    {
        void validate(T elem);
    }

    public class ValidationException : ApplicationException
    {
        public ValidationException(string message)
        : base(message) { }
    }
}

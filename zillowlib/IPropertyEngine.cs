﻿using System;
using zillowlib.Schema;

namespace zillowlib
{
    public interface IPropertyEngine
    {
        Property SearchProperty(string address , string cityStateZip);

        decimal GetZestimate(uint propertyId);
    }
}
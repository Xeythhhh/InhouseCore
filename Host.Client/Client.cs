﻿using System.Reflection;

namespace Host.Client;
/// <summary> Class to reference the Client <see cref="System.Reflection.Assembly"/> </summary>
public static class Client
{
    /// <returns> A Reference to the Client <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(Client).Assembly;
}

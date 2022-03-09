using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Functions
{
    public static class DbFunctionsExtensions
    {
        public static byte[] EncryptByPassphrase(this DbFunctions _, string password, string value)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static byte[] DecryptByPassphrase(this DbFunctions _, string password, byte[] value)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static byte[] DecryptByKey(this DbFunctions _, byte[] value)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string ConvertToVarChar(this DbFunctions _, byte[] value)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string ConvertToVarChar(this DbFunctions _, byte[] value, int len)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string ConvertToNVarChar(this DbFunctions _, byte[] value)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }

        public static string ConvertToNVarChar(this DbFunctions _, byte[] value, int len)
        {
            throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
        }
    }
}
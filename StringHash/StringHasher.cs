﻿using System.Security.Cryptography;

namespace StringHash;

public static class StringHasher
{
    public static string Hash(this string entity)
    {
        byte[] salt;
        byte[] buffer2;
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        using (Rfc2898DeriveBytes bytes = new(entity, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedString(this string hashedString, string entity)
    {
        byte[] buffer4;
        if (hashedString == null)
        {
            return false;
        }
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        byte[] src = Convert.FromBase64String(hashedString);
        if (src.Length != 0x31 || src[0] != 0)
        {
            return false;
        }
        byte[] dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        byte[] buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new(entity, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20);
        }
        return ByteArraysEqual(buffer3, buffer4);
    }

    private static bool ByteArraysEqual(byte[] buffer3, byte[] buffer4)
    {
        if (buffer3.Length != buffer4.Length) return false;
        for (int i = 0; i < buffer3.Length; i++)
        {
            if (buffer3[i] != buffer4[i]) return false;
        }
        return true;
    }
}
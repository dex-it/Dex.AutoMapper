using System;
using System.Reflection;

namespace AutoMapper.Extensions.ExpressionMapping.Structures;

internal class DeclaringMemberKey : IEquatable<DeclaringMemberKey>
{
    public DeclaringMemberKey(MemberInfo declaringMemberInfo, string declaringMemberFullName)
    {
        DeclaringMemberInfo = declaringMemberInfo;
        DeclaringMemberFullName = declaringMemberFullName;
    }

    public MemberInfo DeclaringMemberInfo { get; set; }
    public string DeclaringMemberFullName { get; set; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        DeclaringMemberKey key = obj as DeclaringMemberKey;
        if (key == null) return false;

        return Equals(key);
    }

    public bool Equals(DeclaringMemberKey other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return DeclaringMemberInfo.Equals(other.DeclaringMemberInfo)
               && DeclaringMemberFullName == other.DeclaringMemberFullName;
    }

    public override int GetHashCode() => DeclaringMemberInfo.GetHashCode();

    public override string ToString() => DeclaringMemberFullName;
}
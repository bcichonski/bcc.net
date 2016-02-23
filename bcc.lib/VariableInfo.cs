using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public enum VariableType
    {
        @Int32,
        @Char
    }

    public class TypeDescriptor
    {
        public VariableType PrimitiveType { get; private set; }
        public TypeDescriptor(VariableType type)
        {
            this.PrimitiveType = type;
        }
        public TypeDescriptor(TypeDescriptor tdesc)
        {
            this.PrimitiveType = tdesc.PrimitiveType;
        }

        public static implicit operator TypeDescriptor(VariableType type)
        {
            return new TypeDescriptor(type);
        }

        public static implicit operator VariableType(TypeDescriptor td)
        {
            return td.PrimitiveType;
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableType)
            {
                var v = (VariableType)obj;
                return v == this.PrimitiveType;
            }
            else
                if (obj is TypeDescriptor && !(obj is ArrayTypeDescriptor))
            {
                var v = (TypeDescriptor)obj;
                return v.PrimitiveType == this.PrimitiveType;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.PrimitiveType.GetHashCode();
        }
    }

    public class ArrayTypeDescriptor : TypeDescriptor
    {
        public int Size { get; private set; }
        public ArrayTypeDescriptor(VariableType type, int size) : base(type)
        {
            this.Size = size;
        }
        public ArrayTypeDescriptor(TypeDescriptor tdesc, int size) : base(tdesc)
        {
            this.Size = size;
        }
        public ArrayTypeDescriptor(ArrayTypeDescriptor arr) : this(arr.PrimitiveType, arr.Size)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableType)
                return base.Equals(obj);
            else if (obj is ArrayTypeDescriptor)
            {
                var v = (ArrayTypeDescriptor)obj;
                return this.PrimitiveType == v.PrimitiveType && this.Size == v.Size;
            }
            else return false;
        }
    }

    public class VariableInfo
    {
        public string Name { get; set; }
        public int IlNo { get; set; }
        public VariableType Type { get; set; }
    }
}

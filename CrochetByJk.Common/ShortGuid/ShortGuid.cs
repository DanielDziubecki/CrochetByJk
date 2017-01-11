using System;

namespace CrochetByJk.Common.ShortGuid
{
    public struct ShortGuid
    {
        public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

        private Guid guid;
        private string value;

        public ShortGuid(string value)
        {
            this.value = value;
            guid = Decode(value);
        }

        public ShortGuid(Guid guid)
        {
            value = Encode(guid);
            this.guid = guid;
        }

        public Guid Guid
        {
            get { return guid; }
            set
            {
                if (value == guid) return;
                guid = value;
                this.value = Encode(value);
            }
        }
        public string Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                guid = Decode(value);
            }
        }
        public override string ToString()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ShortGuid)
                return guid.Equals(((ShortGuid) obj).guid);
            if (obj is Guid)
                return guid.Equals((Guid) obj);
            if (obj is string)
                return guid.Equals(((ShortGuid) obj).guid);
            return false;
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }

        public static string Encode(string value)
        {
            var guid = new Guid(value);
            return Encode(guid);
        }

        public static string Encode(Guid guid)
        {
            var encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");
            var buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }

        public static bool operator ==(ShortGuid x, ShortGuid y)
        {
            return x.guid == y.guid;
        }

        public static bool operator !=(ShortGuid x, ShortGuid y)
        {
            return !(x == y);
        }

        public static implicit operator string(ShortGuid shortGuid)
        {
            return shortGuid.value;
        }

        public static implicit operator Guid(ShortGuid shortGuid)
        {
            return shortGuid.guid;
        }

        public static implicit operator ShortGuid(string shortGuid)
        {
            return new ShortGuid(shortGuid);
        }

        public static implicit operator ShortGuid(Guid guid)
        {
            return new ShortGuid(guid);
        }
    }
}
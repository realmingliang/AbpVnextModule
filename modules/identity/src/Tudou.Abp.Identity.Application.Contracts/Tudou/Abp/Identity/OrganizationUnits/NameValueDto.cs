using System;
using Volo.Abp;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    [Serializable]
    public class NameValueDto : NameValueDto<Guid>
    {

        public NameValueDto()
        {

        }

        public NameValueDto(string name, Guid value)
            : base(name, value)
        {

        }

        public NameValueDto(NameValue<Guid> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }

    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        public NameValueDto()
        {

        }

        public NameValueDto(string name, T value)
            : base(name, value)
        {

        }

        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}
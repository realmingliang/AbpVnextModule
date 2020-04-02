using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp;
using Tudou.Abp.Identity.Localization;

namespace Tudou.Abp.Identity
{
    [Serializable]
    public class AbpIdentityResultException : BusinessException, ILocalizeErrorMessage
    {
        public IdentityResult IdentityResult { get; }

        public AbpIdentityResultException([NotNull] IdentityResult identityResult)
            : base(
                code: $"Identity.{identityResult.Errors.First().Code}",
                message: identityResult.Errors.Select(err => err.Description).JoinAsString(", "))
        {
            IdentityResult = Check.NotNull(identityResult, nameof(identityResult));
        }

        public AbpIdentityResultException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public virtual string LocalizeMessage(LocalizationContext context)
        {
            return IdentityResult.LocalizeErrors(context.LocalizerFactory.Create<IdentityResource>());
        }
    }
}

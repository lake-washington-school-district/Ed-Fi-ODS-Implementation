﻿#if NETFRAMEWORK
// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using Castle.MicroKernel.Registration;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Extensibility;
using EdFi.Ods.Features.OwnershipBasedAuthorization.Installers;

namespace EdFi.Ods.Features.OwnershipBasedAuthorization
{
    public class OwnershipBasedAuthorizationFeature : ConfigurationBasedFeatureBase
    {
        public OwnershipBasedAuthorizationFeature(IConfigValueProvider configValueProvider,
            IApiConfigurationProvider apiConfigurationProvider)
            : base(configValueProvider, apiConfigurationProvider) { }

        public override string FeatureName
        {
            get => OwnershipBasedAuthorizationConstants.FeatureName;
        }

        public override IWindsorInstaller Installer
        {
            get => new OwnershipBasedAuthorizationInstaller();
        }
    }
}
#endif

﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace G2H.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string tenantId;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.clientId = Environment.GetEnvironmentVariable("AzureClientId");
            this.clientSecret = Environment.GetEnvironmentVariable("AzureClientSecret");
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.azure = AuthenticateAzure();
        }

        private IAzure AuthenticateAzure()
        {
            AzureCredentials credentials =
                SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                    clientId: this.clientId,
                    clientSecret: this.clientSecret,
                    tenantId: this.tenantId,
                    environment: AzureEnvironment.AzureGlobalCloud);

            return Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();
        }
    }
}
